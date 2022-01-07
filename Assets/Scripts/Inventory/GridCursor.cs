using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class GridCursor : MonoBehaviour
{
    public delegate void ItemChange(int id);

    [SerializeField] protected Inventory _inventory = null;
    [SerializeField] protected GridLayoutExtended _grid = null;
    [SerializeField] protected InputManager _inputManager = null;

    public ItemChange SelectedItemChanged;

    protected Vector2 _cellSize = Vector2.zero;
    protected RectTransform _rectTransform = null;

    protected Vector2 _savedAnchorMin = Vector2.zero;
    protected Vector2 _savedAnchorMax = Vector2.zero;

    [HideInInspector] public int SelectedItem = 0;

    protected InventoryItem _holdingItem = null;
    protected int _originalIdOfHoldingItem = -1;

    protected void Start()
    {
        if (_grid)
        {
            _cellSize = _grid.CellSize;
            _rectTransform = GetComponent<RectTransform>();

            _inputManager.X_Axis += HorizontalMove;
            _inputManager.Y_Axis += VerticalMove;

            SelectedItem = 0;
            StartCoroutine(DelayedStart()); //Needs to be delayed as the grid layout needs a frame to initialize properly
        }
    }

    protected IEnumerator DelayedStart()
    {
        yield return null;

        GoToItemId(SelectedItem);
    }

    protected void VerticalMove(float value)
    {
        if (value < 0)
            SelectedItem = KeepSelectionInRange(SelectedItem + _grid.Columns);
        else
            SelectedItem = KeepSelectionInRange(SelectedItem - _grid.Columns);

        GoToItemId(SelectedItem);
    }

    protected void HorizontalMove(float value)
    {
        if (value < 0)
            SelectedItem = KeepSelectionInRange(SelectedItem - 1);
        else
            SelectedItem = KeepSelectionInRange(SelectedItem + 1);

        GoToItemId(SelectedItem);
    }

    public void GoToItemId(int id)
    {
        Vector2 selectedItemPos = _grid.transform.GetChild(id).GetComponent<RectTransform>().anchoredPosition;

        _rectTransform.anchoredPosition = selectedItemPos;
        SelectedItem = id;

        SelectedItemChanged?.Invoke(SelectedItem);
    }

    public int KeepSelectionInRange(int newId)
    {
        int result = SelectedItem;

        if (newId >= 0 && newId < _grid.Columns * _grid.Rows)
        {
            result = newId;
        }

        Debug.Log("Selected item: " + result);
        return result;
    }

    public void Clear()
    {
        if (transform.childCount > 0)
            _inventory?.ClearItemFromId(_originalIdOfHoldingItem);
        else
        {
            _inventory?.ClearInventory();
            _inventory?.FillInventory();
        }
    }

    public void PickUp()
    {
        if (transform.childCount > 0)
        {
            _inventory.PlaceItemAt(SelectedItem, _originalIdOfHoldingItem, _holdingItem);
            _originalIdOfHoldingItem = -1;
            SelectedItemChanged?.Invoke(SelectedItem);
        }
        else
        {
            _holdingItem = _inventory?.PickUpItemAt(SelectedItem, transform);
            _originalIdOfHoldingItem = SelectedItem;
        }
    }
}
