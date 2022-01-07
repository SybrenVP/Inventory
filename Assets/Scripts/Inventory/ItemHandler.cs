using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHandler
{
    public const string Path = "Items/";

    protected static InventoryItem[] _items = null;

    public static InventoryItem GetRandomItem()
    {
        if (_items == null)
        {
            _items = Resources.LoadAll<InventoryItem>(Path);
            Debug.Log("Loaded " + _items.Length + " items from the resources folder: " + Path);
        }

        //Get random item from this list
        int randomId = Random.Range(0, _items.Length);

        return _items[randomId];
    }

    public static InventoryItem[] GetAllItems()
    {
        if (_items == null)
        {
            _items = Resources.LoadAll<InventoryItem>(Path);
            Debug.Log("Loaded " + _items.Length + " items from the resources folder: " + Path);
        }

        return _items;
    }
}
