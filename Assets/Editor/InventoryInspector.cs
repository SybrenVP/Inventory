using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Inventory))]
public class InventoryInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Inventory inventory = target as Inventory;

        if (GUILayout.Button("Add item"))
        {
            inventory.AddItemToInventoryInRandomPosition();
        }

        if (GUILayout.Button("Clear inventory"))
        {
            inventory.ClearInventory();
        }
    }
}
