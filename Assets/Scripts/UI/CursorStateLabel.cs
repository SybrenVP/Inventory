using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class CursorStateLabel : MonoBehaviour
{
    [SerializeField] private string _emptyCursorLabel = "";
    [SerializeField] private string _holdingItemLabel = "";
    [SerializeField] private string _hoveringItemLabel = "";

    [SerializeField] protected GridCursor _cursor = null;
    protected TMP_Text _label = null;

    protected void Start()
    {
        if (_cursor)
            _cursor.StateChanged += ChangeLabel;

        _label = GetComponent<TMP_Text>();
    }

    protected void ChangeLabel(CursorState state)
    {
        switch (state)
        {
            case CursorState.Empty:
                _label.text = _emptyCursorLabel;
                break;

            case CursorState.HoldingItem:
                _label.text = _holdingItemLabel;
                break;

            case CursorState.HoveringItem:
                _label.text = _hoveringItemLabel;
                break;
        }
    }
}
