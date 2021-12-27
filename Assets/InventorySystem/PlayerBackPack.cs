using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBackPack : MonoBehaviour
{
    public InventoryObject inventory;
    public void AddItemInBackPack(ItemObject item, int _amount)
    {
        inventory.AddItem(item, _amount);
    }
    private void OnApplicationQuit()
    {
        inventory.Container.Clear();
    }
}
