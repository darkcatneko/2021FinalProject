using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Potion,
    Seed,
    Default
}
public abstract class ItemObject : ScriptableObject
{
    public Sprite sprite;
    public ItemType type;
    public int Item_ID;
    [TextArea(15,20)]
    public string description;
}
[System.Serializable]
public class TrueItem
{
    public string Name;
    public int Id;
    public ItemType type;
    public TrueItem(ItemObject _item)
    {
        Name = _item.name;
        type = _item.type;
        Id = _item.Item_ID;
    }
}
