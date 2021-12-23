using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CabbageInteract : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log(other.name + "is in");
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {            
            if (Input.GetKeyDown(KeyCode.F) && this.gameObject.GetComponent<PlantPerform>().GetPlantState() == 3)//採收
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
            if (Input.GetKeyDown(KeyCode.Y)&& this.gameObject.GetComponent<PlantPerform>().GetPlantState() != 3)
            {
                this.GetComponent<PlantPerform>().This_Plant.Is_fertilize = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log(other.name + "is out");
        }
    }
}
