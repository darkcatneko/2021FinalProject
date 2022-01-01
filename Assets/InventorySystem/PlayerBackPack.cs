using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBackPack : MonoBehaviour
{
    public InventoryObject inventory;
    public void AddItemInBackPack(ItemObject item, int _amount)
    {
        
        inventory.AddItem(new TrueItem(item), _amount);
        ItemBarDisplay.instance.OnLoad();
    }
    private void OnApplicationQuit()
    {
        inventory.Container.Clear();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            inventory.Save();
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            inventory.Load();
        }
    }
}
