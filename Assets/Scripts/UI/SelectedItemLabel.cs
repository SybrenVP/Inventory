using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class SelectedItemLabel : MonoBehaviour
{
    [SerializeField] protected GridCursor _cursor;
    [SerializeField] protected Inventory _inventory;

    protected TMP_Text _textField = null;

    protected void Awake()
    {
        _textField = GetComponent<TMP_Text>();
        _cursor.SelectedItemChanged += ChangeLabelToItem;       
    }

    public void ChangeLabelToItem(int id)
    {
        InventoryItem item = _inventory.GetItemInSpace(id);
        _textField.text = item ? item.ItemName : "Empty";
    }
    
}
