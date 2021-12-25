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
    public static bool InEmptyFarmRange;

    private void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            InEmptyFarmRange = true;
            
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.O) && thisfarmspace.PlantWhich == WhichPlant.EmptySpace && other.GetComponentInParent<PlayerMovement>().movement.z <= 0)//plant a cabbage
            {
                       other.GetComponentInParent<PlayerMovement>().IM_Planting();
                       thisfarmspace.PlantWhich = WhichPlant.cabbage;
                        StartCoroutine(Delay.DelayToInvokeDo(() =>
                        {
                           GameObject planted_cabbage =  Instantiate(plantPrefab[0], this.transform.position, Quaternion.Euler(45f, 180f, 0));
                           planted_cabbage.GetComponent<PlantPerform>().SetPlantIdentity(PlantState.seed, WhichPlant.cabbage, thisfarmspace.FarmID,false);
                           GameObject vfx = Instantiate(testVFX, this.transform.position+new Vector3(0,0.005f,0), Quaternion.Euler(45f, 0, 0));
                           Destroy(vfx, 1f);
                           this.gameObject.GetComponent<Collider>().enabled = false;
                           
                        }
                        , 1.5f));

            }            
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            InEmptyFarmRange = false;
            Debug.Log(other.name + "is out");
        }
    }
    //funtion(id) 根據植物id生成植物
    
    
}
