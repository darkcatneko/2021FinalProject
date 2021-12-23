using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CabbageInteract : MonoBehaviour
{
    public string PlantName;
    public int PlantID;
    private void OnTriggerEnter(Collider other)
    {       
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log(other.name + "is in");
            if (Input.GetKeyDown(KeyCode.F) && this.gameObject.GetComponent<PlantPerform>().GetPlantState() == 3)
            {
                Debug.Log("get a mature cabbage!");
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {            
            if (Input.GetKeyDown(KeyCode.F) && this.gameObject.GetComponent<PlantPerform>().GetPlantState() == 3)
            {
                Debug.Log("get a mature cabbage!");
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
