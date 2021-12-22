using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToBed : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                AllPlantGrow();
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

    public void AllPlantGrow()
    {
        Debug.Log("growing time");
        GameObject[] Plants;
        Plants = GameObject.FindGameObjectsWithTag("Plant");
        foreach (var item in Plants)
        {
            item.GetComponent<PlantPerform>().TimePass();
        }
    }
}
