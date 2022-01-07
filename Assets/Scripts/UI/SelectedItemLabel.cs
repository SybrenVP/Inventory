using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class SelectedItemLabel : MonoBehaviour
{
    [SerializeField] protected GridCursor _cursor;

    protected TMP_Text _textField = null;

    protected void Awake()
    {
        _textField = GetComponent<TMP_Text>();
        _cursor.SelectedItemChanged += ChangeLabelToItem;       
    }

    public void ChangeLabelToItem(InventoryItem item)
    {
        _textField.text = item ? item.ItemName : "Empty";
    }
    
}
