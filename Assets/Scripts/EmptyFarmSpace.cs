using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class EmptyFarm
{
    public int FarmID;
    public WhichPlant PlantWhich;

    public EmptyFarm(int id, WhichPlant plant)
    {
        FarmID = id;
        PlantWhich = plant;
    }
}

public class EmptyFarmSpace : MonoBehaviour
{
    public GameObject[] plantPrefab;  
    public GameObject testVFX;
   
    public PlantIdentity PlantSaveFile;
    bool InEmptyFarmRange;
    Collider other;

    private void Start()
    {
        FarmReload();
    }
    private void Update()
    {
        if (InEmptyFarmRange == true)
        {
            if (other.gameObject.CompareTag("Player") && ItemBarDisplay.instance.items.item != null)
            {
                if (Input.GetKeyDown(KeyCode.O) && PlantSaveFile.which == WhichPlant.EmptySpace && other.GetComponentInParent<PlayerMovement>().movement.z <= 0 && ItemBarDisplay.instance.items.item.type==ItemType.Seed&&ItemBarDisplay.instance.items!=null)//plant a cabbage
                {
                    switch(ItemBarDisplay.instance.items.item.Id)
                    {
                        case 1:
                            InstantiatePlant(WhichPlant.cabbage,0,PlantState.seed,false);
                            break;
                        case 3:
                            InstantiatePlant(WhichPlant.tomato,1, PlantState.seed,false);
                            break;
                        case 5:
                            InstantiatePlant(WhichPlant.corn,2, PlantState.seed,false);
                            break;
                            
                    }

                    
                }
            }
        }
    }

    private void OnTriggerEnter(Collider others)
    {
        if (others.gameObject.CompareTag("Player"))
        {
            InEmptyFarmRange = true;
            other = others;
        }
    }    
    private void OnTriggerExit(Collider others)
    {
        if (others.gameObject.CompareTag("Player"))
        {
            InEmptyFarmRange = false;
            other = null;           
        }
    }

    public void PlantIdentityUpdate(PlantIdentity pid)
    {
        PlantSaveFile = pid;
    }

    //funtion(id) 根據植物id生成植物
    public void ReloadPlantInstantiate(WhichPlant _whichplant, int PlantNum, PlantState state, bool isfertilize)
    {
            PlantSaveFile.which = _whichplant;
            GameObject planted_cabbage = Instantiate(plantPrefab[PlantNum], this.transform.position, Quaternion.Euler(-45f, 180f, 0));
            planted_cabbage.GetComponent<PlantPerform>().SetPlantIdentity(state, _whichplant, PlantSaveFile.plantspaceID, isfertilize);
            planted_cabbage.GetComponent<PlantPerform>().PlantUpdate();
            PlantSaveFile = planted_cabbage.GetComponent<PlantPerform>().This_Plant;
            this.gameObject.GetComponent<Collider>().enabled = false;
            if (isfertilize == true)
            {
                planted_cabbage.GetComponent<CabbageInteract>().FertilizePartical();
            }        
    }
    public void InstantiatePlant(WhichPlant _whichplant, int PlantNum, PlantState state, bool isfertilize)
    {
        other.GetComponentInParent<PlayerMovement>().IM_Planting();
        PlantSaveFile.which = _whichplant;
        StartCoroutine(Delay.DelayToInvokeDo(() =>
        {
            GameObject planted_cabbage = Instantiate(plantPrefab[PlantNum], this.transform.position, Quaternion.Euler(-45f, 180f, 0));
            planted_cabbage.GetComponent<PlantPerform>().SetPlantIdentity(state, _whichplant, PlantSaveFile.plantspaceID, isfertilize);
            PlantSaveFile = planted_cabbage.GetComponent<PlantPerform>().This_Plant;
            GameObject vfx = Instantiate(testVFX, this.transform.position + new Vector3(0, 0.11f, 0.1f), Quaternion.Euler(45f, 0, 0));
            Destroy(vfx, 1f);
            this.gameObject.GetComponent<Collider>().enabled = false;
            if (isfertilize == true)
            {
                planted_cabbage.GetComponent<CabbageInteract>().FertilizePartical();
            }
        }
        , 1.5f));
    }   
    public void FarmReload()
    {
        if (this.PlantSaveFile.which != WhichPlant.EmptySpace)
        {
            switch(this.PlantSaveFile.which)
            {
                case WhichPlant.cabbage:
                    ReloadPlantInstantiate(WhichPlant.cabbage, 0, this.PlantSaveFile.plantState, this.PlantSaveFile.Is_fertilize);
                    return;
                case WhichPlant.corn:
                    ReloadPlantInstantiate(WhichPlant.corn, 2, this.PlantSaveFile.plantState, this.PlantSaveFile.Is_fertilize);
                    return;
                case WhichPlant.tomato:
                    ReloadPlantInstantiate(WhichPlant.tomato, 1, this.PlantSaveFile.plantState, this.PlantSaveFile.Is_fertilize);
                    return;
            }
        }
    }
}
