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

    public WhichPlant which = WhichPlant.EmptySpace;

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
            if (Input.GetKeyDown(KeyCode.O))//plant a cabbage
            {
                       Instantiate(plantPrefab[0], this.transform.position, Quaternion.Euler(45f, 180f, 0));   
                       GameObject vfx = Instantiate(testVFX, this.transform.position, Quaternion.Euler(45f, 0, 0));
                       Destroy(vfx, 1f);
                       this.gameObject.GetComponent<Collider>().enabled = false;
                       thisfarmspace.PlantWhich = WhichPlant.cabbage;
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
