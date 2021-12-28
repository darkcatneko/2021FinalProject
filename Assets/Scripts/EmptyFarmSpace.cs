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
    public EmptyFarm thisfarmspace;
    public PlantIdentity PlantSaveFile;
    bool InEmptyFarmRange;
    Collider other;

    private void Start()
    {

    }
    private void Update()
    {
        if (InEmptyFarmRange == true)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (Input.GetKeyDown(KeyCode.O) && thisfarmspace.PlantWhich == WhichPlant.EmptySpace && other.GetComponentInParent<PlayerMovement>().movement.z <= 0)//plant a cabbage
                {
                    other.GetComponentInParent<PlayerMovement>().IM_Planting();
                    thisfarmspace.PlantWhich = WhichPlant.cabbage;
                    StartCoroutine(Delay.DelayToInvokeDo(() =>
                    {
                        GameObject planted_cabbage = Instantiate(plantPrefab[0], this.transform.position, Quaternion.Euler(-45f, 180f, 0));
                        planted_cabbage.GetComponent<PlantPerform>().SetPlantIdentity(PlantState.seed, WhichPlant.cabbage, thisfarmspace.FarmID, false);
                        PlantSaveFile = planted_cabbage.GetComponent<PlantPerform>().This_Plant;
                        GameObject vfx = Instantiate(testVFX, this.transform.position + new Vector3(0, 0.11f, 0.1f), Quaternion.Euler(45f, 0, 0));
                        Destroy(vfx, 1f);
                        this.gameObject.GetComponent<Collider>().enabled = false;

                    }
                    , 1.5f));
                }
                if (Input.GetKeyDown(KeyCode.P) && thisfarmspace.PlantWhich == WhichPlant.EmptySpace && other.GetComponentInParent<PlayerMovement>().movement.z <= 0)//plant a cabbage
                {
                    other.GetComponentInParent<PlayerMovement>().IM_Planting();
                    thisfarmspace.PlantWhich = WhichPlant.tomato;
                    StartCoroutine(Delay.DelayToInvokeDo(() =>
                    {
                        GameObject planted_cabbage = Instantiate(plantPrefab[1], this.transform.position, Quaternion.Euler(-45f, 180f, 0));
                        planted_cabbage.GetComponent<PlantPerform>().SetPlantIdentity(PlantState.seed, WhichPlant.tomato, thisfarmspace.FarmID, false);
                        PlantSaveFile = planted_cabbage.GetComponent<PlantPerform>().This_Plant;
                        GameObject vfx = Instantiate(testVFX, this.transform.position + new Vector3(0, 0.11f, 0.1f), Quaternion.Euler(45f, 0, 0));
                        Destroy(vfx, 1f);
                        this.gameObject.GetComponent<Collider>().enabled = false;
                    }
                    , 1.5f));
                }
                if (Input.GetKeyDown(KeyCode.T) && thisfarmspace.PlantWhich == WhichPlant.EmptySpace && other.GetComponentInParent<PlayerMovement>().movement.z <= 0)//plant a cabbage
                {
                    other.GetComponentInParent<PlayerMovement>().IM_Planting();
                    thisfarmspace.PlantWhich = WhichPlant.corn;
                    StartCoroutine(Delay.DelayToInvokeDo(() =>
                    {
                        GameObject planted_cabbage = Instantiate(plantPrefab[2], this.transform.position, Quaternion.Euler(-45f, 180f, 0));
                        planted_cabbage.GetComponent<PlantPerform>().SetPlantIdentity(PlantState.seed, WhichPlant.corn, thisfarmspace.FarmID, false);
                        PlantSaveFile = planted_cabbage.GetComponent<PlantPerform>().This_Plant;
                        GameObject vfx = Instantiate(testVFX, this.transform.position + new Vector3(0, 0.11f, 0.1f), Quaternion.Euler(45f, 0, 0));
                        Destroy(vfx, 1f);
                        this.gameObject.GetComponent<Collider>().enabled = false;
                    }
                    , 1.5f));
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
    
    
}
