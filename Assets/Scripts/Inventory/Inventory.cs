using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] protected int _amtRandomItems = 5;
    [SerializeField] protected Transform _itemHolder = null;

    protected InventoryItem[] _inventoryItems = null;

    protected void Start()
    {
        //Initialize _inventoryItems according to the amount of objects created 
        _inventoryItems = new InventoryItem[_itemHolder.childCount];

        FillInventory();
    }

    public void FillInventory()
    {
        for (int i = 0; i < _amtRandomItems; i++)
        {
            AddItemToInventoryInRandomPosition();
        }
    }

    public void AddItemToInventoryInRandomPosition()
    {
        //Request x amount random free spaces
        int spaceId = GetRandomFreeSpace();

        if (spaceId < 0)
        {
            return;
        }

        //Request x amount random items
        InventoryItem item = ItemHandler.GetRandomItem();
        InventoryItem itemInstance = Instantiate(item);

        itemInstance.Initialize(_itemHolder.GetChild(spaceId));

        _inventoryItems[spaceId] = itemInstance;
    }

    public int GetRandomFreeSpace()
    {
        List<int> emptySpaces = new List<int>();
        for (int i = 0; i < _inventoryItems.Length; i++)
        {
            if (_inventoryItems[i] == null)
                emptySpaces.Add(i);
        }
        if (emptySpaces.Count == 0)
        {
            Debug.LogWarning("No new empty space found!");
            return -1;
        }

        int result = UnityEngine.Random.Range(0, emptySpaces.Count);

        return emptySpaces[result];
    }

    public void ClearInventory()
    {
        //Get all ItemVisual objects and destroy them
        foreach (InventoryItem item in _inventoryItems)
        {
            if (item != null)
            {
                Destroy(item.GetActiveVisual().gameObject);
                Destroy(item);
            }
        }
    }

    public void ClearItemFromId(int id)
    {
        Destroy(_inventoryItems[id].GetActiveVisual().gameObject);
        Destroy(_inventoryItems[id]);
    }

    public InventoryItem PickUpItemAt(int id, Transform newParent)
    {
        _inventoryItems[id].GetActiveVisual().transform.SetParent(newParent);
        return _inventoryItems[id];
    }

    public void PlaceItemAt(int newId, int oldId, InventoryItem item)
    {
        if (_inventoryItems[newId] != null)
        {
            _inventoryItems[newId].AssignSpace(_itemHolder.GetChild(oldId));
            _inventoryItems[oldId] = _inventoryItems[newId];
        }

        item.AssignSpace(_itemHolder.GetChild(newId));
        _inventoryItems[newId] = item;
    }

    public InventoryItem GetItemInSpace(int spaceId)
    {
        return _inventoryItems[spaceId];
    }
}
