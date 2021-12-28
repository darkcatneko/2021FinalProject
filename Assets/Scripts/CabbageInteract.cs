using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CabbageInteract : MonoBehaviour
{
    [SerializeField] GameObject fertilizeVfx_Spawnpoint;
    [SerializeField] GameObject Fertilize2D_VFX;
    [SerializeField] GameObject Harvest2D_VFX;
    public GameObject vfx;

    [SerializeField] ItemObject The_seed_it_gen;

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
                            
                            GetSameEmptyFarm().GetComponent<Collider>().enabled = true;
                            GetSameEmptyFarm().GetComponent<EmptyFarmSpace>().thisfarmspace.PlantWhich = WhichPlant.EmptySpace;
                            GetSameEmptyFarm().GetComponent<EmptyFarmSpace>().PlantSaveFile = new PlantIdentity(PlantState.seed,WhichPlant.EmptySpace, this.GetComponent<PlantPerform>().This_Plant.plantspaceID, false);
                            GameObject vfx = Instantiate(Harvest2D_VFX, this.transform.position + new Vector3(0, 0.11f, 0.1f), Quaternion.Euler(45f, 0, 0));
                            Destroy(vfx, 1f);
                            other.gameObject.GetComponentInParent<PlayerBackPack>().AddItemInBackPack(this.gameObject.GetComponent<Item>().item, 1);//進包包(完熟植物)
                            other.gameObject.GetComponentInParent<PlayerBackPack>().AddItemInBackPack(The_seed_it_gen, Random.Range(1, 4));//種子回收
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
        GetSameEmptyFarm().GetComponent<EmptyFarmSpace>().PlantIdentityUpdate(this.GetComponent<PlantPerform>().This_Plant);
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
    public GameObject GetSameEmptyFarm()
    {
        GameObject correctfarm = null;
        GameObject[] emptyplace;
        emptyplace = GameObject.FindGameObjectsWithTag("Emptyfarm");
        for (int i = 0; i < emptyplace.Length; i++)
        {
            if (emptyplace[i].GetComponent<EmptyFarmSpace>().thisfarmspace.FarmID == this.GetComponent<PlantPerform>().This_Plant.plantspaceID)//抓取plantperform的class裡的id
            {
                correctfarm = emptyplace[i];                               
            }
        }
        return correctfarm ;
    }
}
