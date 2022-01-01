using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToBed : MonoBehaviour
{
    bool Incollider;
    Collider player;    
    public InventoryObject playerInventory;
    public GameObject CanvasLayer1;
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
                CanvasLayer1.SetActive(true);                
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
    void wakePart2()
    {
        CanvasLayer1.GetComponent<Animator>().SetBool("DayTime", true);

    }
    public void WakeUpButtonVer2()
    {
        Debug.Log("bruh");
        InGameTime.instance.WakeUpButton();
        wakePart2();
        StartCoroutine(Delay.DelayToInvokeDo(() => 
        {
            player.GetComponentInParent<PlayerMovement>().playerState = PlayerState.FreeMove;
            playerInventory.Load();
            CanvasLayer1.SetActive(false);
        }
        , 2.5f));
    }
}
