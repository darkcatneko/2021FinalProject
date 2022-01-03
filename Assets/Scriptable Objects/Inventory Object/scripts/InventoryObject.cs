using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEditor;
[CreateAssetMenu(fileName = "New Inventory",menuName ="Inventory System/Inventory")]

public class InventoryObject : ScriptableObject,ISerializationCallbackReceiver
{
    public string savePath;
    private ItemDatabaseObject data;
    public GameTimeData TimeData;
    public List<PlantIdentity> emptyfarmData = new List<PlantIdentity>();
    public List<InventorySlot> Container = new List<InventorySlot>();

    private void OnEnable()
    {
#if UNITY_EDITOR
        data = (ItemDatabaseObject)AssetDatabase.LoadAssetAtPath("Assets/Resources/Database.asset", typeof(ItemDatabaseObject));
#else
        data = Resources.Load<ItemDatabaseObject>("Database");
#endif
    }
    public ItemObject GetItem(int i)
    {
       return data.GetItem[i];
    }
    public void EmptyFarmLoad()
    {
        GameObject[] EmptyFarms;
        EmptyFarms = GameObject.FindGameObjectsWithTag("Emptyfarm");
        for (int i = 0; i < EmptyFarms.Length; i++)
        {
            for (int j = 0; j < EmptyFarms.Length; j++)
            {
                if (EmptyFarms[j].GetComponent<EmptyFarmSpace>().PlantSaveFile.plantspaceID == i)
                {
                    EmptyFarms[j].GetComponent<EmptyFarmSpace>().PlantIdentityUpdate(emptyfarmData[i]);
                    EmptyFarms[j].GetComponent<EmptyFarmSpace>().FarmReload();                
                }
            }
        }
    }
    public void EmptyFarmListSave()
    {
        emptyfarmData = new List<PlantIdentity>();
        GameObject[] EmptyFarms;
        EmptyFarms = GameObject.FindGameObjectsWithTag("Emptyfarm");
        for (int i = 0; i < EmptyFarms.Length; i++)
        {
            for (int j = 0; j < EmptyFarms.Length; j++)
            {
                if (EmptyFarms[j].GetComponent<EmptyFarmSpace>().PlantSaveFile.plantspaceID == i)
                {
                emptyfarmData.Add(EmptyFarms[j].GetComponent<EmptyFarmSpace>().PlantSaveFile);
                }
            }
        }
    }



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
        EmptyFarmListSave();
        string saveData = JsonUtility.ToJson(this, true);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(string.Concat(Application.persistentDataPath, savePath));
        Debug.Log(string.Concat(Application.persistentDataPath, savePath));
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
        emptyfarmData = new List<PlantIdentity>();
        for (int i = 0; i < 6; i++)
        {
            emptyfarmData.Add(new PlantIdentity(PlantState.seed, WhichPlant.EmptySpace, i, false));
        }
        TimeData.GAMEDAY = 1;
        TimeData.ENERGYWASTE = 0;
        TimeData.PASSSEC = 420 * 60;
    }
    [ContextMenu("SaveEmpty")]
    public void SaveEmpty()
    {
        string saveData = JsonUtility.ToJson(this, true);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(string.Concat(Application.persistentDataPath, savePath));
        bf.Serialize(file, saveData);
        file.Close();
    }

    public void OnAfterDeserialize()
    {
        for (int i = 0; i < Container.Count; i++)
        {
            Container[i].item =new TrueItem(data.GetItem[Container[i].ID]);
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