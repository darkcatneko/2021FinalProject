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
            if (Input.GetKeyDown(KeyCode.F) && this.gameObject.GetComponent<PlantPerform>().GetPlantState() == 3)
            {
                Debug.Log("get a mature cabbage!");
                GameObject[] emptyplace;
                emptyplace = GameObject.FindGameObjectsWithTag("emptyfarm");
                for (int i = 0; i < emptyplace.Length; i++)
                {
                    if (emptyplace[i].GetComponent<EmptyFarmSpace>().thisfarmspace.FarmID==i)//抓取plantperform的class裡的id
                    {

                    }
                }
                Destroy(this.gameObject);
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
