using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToBed : MonoBehaviour
{
    bool Incollider;
    Collider player;    
    public InventoryObject playerInventory;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Incollider = true;
            player = other;
            playerInventory = player.GetComponentInParent<PlayerBackPack>().inventory;
        }
    }
    private void Update()
    {
        if (Incollider == true)
        {
            if (Input.GetKeyDown(KeyCode.U))
            {
                InGameTime.instance.TimeForBed();//stop the clock 
                player.GetComponentInParent<PlayerMovement>().IM_Sleeping();
                playerInventory.Save();
                //play the animation
                //change the UI
                // push the wake up button
                // change stat
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Incollider = false;
            player = null;
            playerInventory = null;
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
            item.GetComponent<CabbageInteract>().Defertilize();
        }
    }
}
