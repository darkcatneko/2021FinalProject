using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CabbageInteract : MonoBehaviour
{
    [SerializeField] GameObject fertilizeVfx_Spawnpoint;
    [SerializeField] GameObject Fertilize2D_VFX;
    [SerializeField] GameObject Harvest2D_VFX;
    public GameObject vfx;

    bool CanInteract;
    Collider other;
        
    private void Start()
    {

    }
    private void OnTriggerEnter(Collider others)
    {
        if (others.gameObject.CompareTag("Player"))
        {
            InRange();
            other = others;
            CanInteract = true;            
        }
    }
    private void Update()
    {
        if (CanInteract == true)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                InRange();
                if (other.GetComponentInParent<PlayerMovement>().movement.z <= 0 && other.GetComponentInParent<PlayerMovement>().playerState == PlayerState.FreeMove)//確認可以執行其他動作
                {
                    if (Input.GetKeyDown(KeyCode.F) && this.gameObject.GetComponent<PlantPerform>().GetPlantState() == 3)//採收
                    {
                        Debug.Log("get a mature cabbage!");
                        other.GetComponentInParent<PlayerMovement>().IM_Planting();

                        StartCoroutine(Delay.DelayToInvokeDo(() =>
                        {
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
                            GameObject vfx = Instantiate(Harvest2D_VFX, this.transform.position + new Vector3(0, 0.11f, 0.1f), Quaternion.Euler(45f, 0, 0));
                            Destroy(vfx, 1f);
                            Destroy(this.gameObject);
                        }
                        , 1.5f));

                    }
                    if (Input.GetKeyDown(KeyCode.Y) && this.gameObject.GetComponent<PlantPerform>().GetPlantState() != 3 && this.GetComponent<PlantPerform>().This_Plant.Is_fertilize == false)
                    {
                        Fertilize(other);
                    }
                }

            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        
    }
    private void OnTriggerExit(Collider others)
    {
        if (others.gameObject.CompareTag("Player"))
        {
            CanInteract = false;
            other = null;
            this.gameObject.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
            this.gameObject.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", new Color(0,0,0,0));            
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
       this.gameObject.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
       this.gameObject.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", new Color(0.1f, 0.1f, 0.1f, 0));
    }
}
