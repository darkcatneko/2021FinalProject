using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
[CreateAssetMenu(fileName = "New Inventory",menuName ="Inventory System/Inventory")]

public class InventoryObject : ScriptableObject,ISerializationCallbackReceiver
{
    public string savePath;
    public ItemDatabaseObject database;
    public GameTimeData TimeData;
    public List<EmptyFarm> emptyfarmData = new List<EmptyFarm>();
    public List<InventorySlot> Container = new List<InventorySlot>();


    public void AddItem(TrueItem _item,int _amount)
    {
        if (_item.buffs.Length>0)
        {
            Container.Add(new InventorySlot(_item.Id, _item, _amount));
            return;
        }
        for (int i = 0; i < Container.Count; i++)
        {
            if (Container[i].item.Id == _item.Id)
            {
                Container[i].AddAmount(_amount);
                return;               
            }
        }
        Container.Add(new InventorySlot(_item.Id,_item, _amount));        
    }
    [ContextMenu("Save")]
    public void Save()
    {
        string saveData = JsonUtility.ToJson(this, true);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(string.Concat(Application.persistentDataPath, savePath));
        bf.Serialize(file, saveData);
        file.Close();
    }
    [ContextMenu("Load")]
    public void Load()
    {
        if (File.Exists(string.Concat(Application.persistentDataPath, savePath)))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);
            JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(),this);
            ItemBarDisplay.instance.OnLoad();
            file.Close();
        }
    }
    [ContextMenu("Clear")]
    public void Clear()
    {
        Container = new List<InventorySlot>();
        TimeData.GAMEDAY = 1;
        TimeData.ENERGYWASTE = 0;
    }
    public void OnAfterDeserialize()
    {
        for (int i = 0; i < Container.Count; i++)
        {
            Container[i].item =new TrueItem(database.GetItem[Container[i].ID]);
        }
    }

    public void OnBeforeSerialize()
    {
        
    }
}
[System.Serializable]
public class InventorySlot
{
    public int ID;
    public TrueItem item;
    public int amount;
    public InventorySlot(int _id, TrueItem _item, int _amount)
    {
        ID = _id;
        item = _item;
        amount = _amount;
    }
    public void AddAmount(int value)
    {
        amount += value;
    }
}