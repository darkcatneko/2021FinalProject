using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CabbageInteract : MonoBehaviour
{
    [SerializeField] GameObject fertilizeVfx_Spawnpoint;
    [SerializeField] GameObject Fertilize2D_VFX;
    public GameObject vfx;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            this.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
            this.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", new Color(0.1f, 0.1f, 0.1f, 0));
            //DynamicGI.UpdateEnvironment();
            Debug.Log(other.name + "is in");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            
            if (Input.GetKeyDown(KeyCode.F) && this.gameObject.GetComponent<PlantPerform>().GetPlantState() == 3 && other.GetComponentInParent<PlayerMovement>().movement.z <= 0)//採收
            {
                Debug.Log("get a mature cabbage!");
                GameObject[] emptyplace;
                emptyplace = GameObject.FindGameObjectsWithTag("Emptyfarm");
                for (int i = 0; i < emptyplace.Length; i++)
                {
                    if (emptyplace[i].GetComponent<EmptyFarmSpace>().thisfarmspace.FarmID == this.GetComponent<PlantPerform>().This_Plant.plantspaceID)//抓取plantperform的class裡的id
                    {
                        emptyplace[i].GetComponent<Collider>().enabled = true;
                        emptyplace[i].GetComponent<EmptyFarmSpace>().thisfarmspace.PlantWhich = WhichPlant.EmptySpace;
                    }
                }
                Destroy(this.gameObject);
            }
            if (Input.GetKeyDown(KeyCode.Y)&& this.gameObject.GetComponent<PlantPerform>().GetPlantState() != 3 && this.GetComponent<PlantPerform>().This_Plant.Is_fertilize == false)
            {
                
                Fertilize();    
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            this.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
            this.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", new Color(0,0,0,0));
            //DynamicGI.UpdateEnvironment();
            Debug.Log(other.name + "is out");
        }
    }

    public void Fertilize()
    {
        this.GetComponent<PlantPerform>().This_Plant.Is_fertilize = true;
        GameObject _vfx = Instantiate(Fertilize2D_VFX, transform.position + new Vector3(0, 0.3f, 0.2f),Quaternion.Euler(45,0,0));
        Destroy(_vfx, 1.05f);
        StartCoroutine(Delay.DelayToInvokeDo(() =>
        {
            vfx = Instantiate(Resources.Load("Partical/PS_fertilize") as GameObject, fertilizeVfx_Spawnpoint.transform.position, Quaternion.identity);
        }
        , 0.8f));
        
    }

    public void Defertilize()
    {
        Destroy(vfx);
    }
}
