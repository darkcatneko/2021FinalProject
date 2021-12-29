using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemBarDisplay : MonoBehaviour
{
    public InventoryObject inventory;
    public int X_START;
    public int Y_START;
    public int X_SPACE_BETWEEN_ITEM;
    public int NUMBER_OF_COLUMN;
    public int Y_SAPCE_BETWEEN_ITEM;

    int MainToolNum = 0;
    public InventorySlot items;

    Dictionary<InventorySlot, GameObject> itemsDisplayed = new Dictionary<InventorySlot, GameObject>();
    public static ItemBarDisplay instance;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        CreateDisplay();        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDisplay();
        if (Input.GetKeyDown(KeyCode.Q))
        {
            MainToolNum--;
            MainToolNum = Mathf.Clamp(MainToolNum, 0, 5);
            if (inventory.Container[MainToolNum] != null)
            {
                items = inventory.Container[MainToolNum];
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            MainToolNum++;
            MainToolNum = Mathf.Clamp(MainToolNum, 0, 5);
            if (inventory.Container[MainToolNum]!=null)
            {
                items = inventory.Container[MainToolNum];
            }
        }
    }
    public void UpdateDisplay()
    {
        for (int i = 0; i < Mathf.Clamp(inventory.Container.Count,0,NUMBER_OF_COLUMN); i++)
        {
            if (itemsDisplayed.ContainsKey(inventory.Container[i]))
            {
                itemsDisplayed[inventory.Container[i]].GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
            }
            else
            {
                var obj = Instantiate(inventory.Container[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
                obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
                obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
                itemsDisplayed.Add(inventory.Container[i], obj);
            }
        }
    }
    public void CreateDisplay()
    {
        for (int i = 0; i < Mathf.Clamp(inventory.Container.Count, 0, NUMBER_OF_COLUMN); i++)
        {
            var obj = Instantiate(inventory.Container[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
        }
    }
    public Vector3 GetPosition(int i)
    {
        return new Vector3(X_START + (X_SPACE_BETWEEN_ITEM * (i % NUMBER_OF_COLUMN)), Y_START + ((-Y_SAPCE_BETWEEN_ITEM) * (i / NUMBER_OF_COLUMN)));
    }
    public void OnLoad()
    {
        items = inventory.Container[0];
    }
}
