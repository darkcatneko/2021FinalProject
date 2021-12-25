using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CabbageInteract : MonoBehaviour
{
    [SerializeField] GameObject fertilizeVfx_Spawnpoint;
    [SerializeField] GameObject Fertilize2D_VFX;
    [SerializeField] GameObject Harvest2D_VFX;
    public GameObject vfx;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //this.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
            //this.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", new Color(0.1f, 0.1f, 0.1f, 0));            
            Debug.Log(other.name + "is in");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            InRange();
            if (other.GetComponentInParent<PlayerMovement>().movement.z <= 0 && other.GetComponentInParent<PlayerMovement>().playerState == PlayerState.FreeMove)//�T�{�i�H�����L�ʧ@
            {
                if (Input.GetKeyDown(KeyCode.F) && this.gameObject.GetComponent<PlantPerform>().GetPlantState() == 3)//�Ħ�
                {
                    Debug.Log("get a mature cabbage!");
                    other.GetComponentInParent<PlayerMovement>().IM_Planting();

                    StartCoroutine(Delay.DelayToInvokeDo(() =>
                    {
                        GameObject[] emptyplace;
                        emptyplace = GameObject.FindGameObjectsWithTag("Emptyfarm");
                        for (int i = 0; i < emptyplace.Length; i++)
                        {
                            if (emptyplace[i].GetComponent<EmptyFarmSpace>().thisfarmspace.FarmID == this.GetComponent<PlantPerform>().This_Plant.plantspaceID)//���plantperform��class�̪�id
                            {
                                emptyplace[i].GetComponent<Collider>().enabled = true;
                                emptyplace[i].GetComponent<EmptyFarmSpace>().thisfarmspace.PlantWhich = WhichPlant.EmptySpace;
                            }
                        }
                        GameObject vfx = Instantiate(Harvest2D_VFX, this.transform.position + new Vector3(0, 0.11f, 0.1f), Quaternion.Euler(45f, 0, 0));
                        Destroy(vfx, 1f);

                    }
                    , 1.5f));
                    Destroy(this.gameObject);
                }
                if (Input.GetKeyDown(KeyCode.Y) && this.gameObject.GetComponent<PlantPerform>().GetPlantState() != 3 && this.GetComponent<PlantPerform>().This_Plant.Is_fertilize == false)
                {
                    Fertilize(other);
                }
            }
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            this.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
            this.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", new Color(0,0,0,0));
            
            Debug.Log(other.name + "is out");
        }
    }

    public void Fertilize(Collider other)
    {
        this.GetComponent<PlantPerform>().This_Plant.Is_fertilize = true;
        other.GetComponentInParent<PlayerMovement>().IM_Fertilizing();
        StartCoroutine(Delay.DelayToInvokeDo(() => 
        {
            GameObject _vfx = Instantiate(Fertilize2D_VFX, transform.position + new Vector3(0, 0.3f, 0.2f), Quaternion.Euler(45, 0, 0));
            Destroy(_vfx, 1.05f);
            StartCoroutine(Delay.DelayToInvokeDo(() =>
            {
                vfx = Instantiate(Resources.Load("Partical/PS_fertilize") as GameObject, fertilizeVfx_Spawnpoint.transform.position, Quaternion.identity);
            }
            , 0.8f));
        }, 1.4f));
        
        
    }

    public void Defertilize()
    {
        Destroy(vfx);
    }

    void InRange()
    {
        this.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
        this.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", new Color(0.1f, 0.1f, 0.1f, 0));
    }
}
