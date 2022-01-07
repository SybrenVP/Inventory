using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CursorState
{
    Empty,
    HoldingItem,
    HoveringItem
}

[RequireComponent(typeof(RectTransform))]
public class GridCursor : MonoBehaviour
{
    public delegate void ItemChange(InventoryItem item);
    public delegate void CursorStateChange(CursorState newState);

    public ItemChange SelectedItemChanged;
    public CursorStateChange StateChanged;

    [HideInInspector] public int SelectedItem = 0;
    [HideInInspector] public CursorState State = CursorState.Empty;

    [SerializeField] protected Inventory _inventory = null;
    [SerializeField] protected GridLayoutExtended _grid = null;
    [SerializeField] protected InputManager _inputManager = null;

    protected RectTransform _rectTransform = null;


    protected InventoryItem _holdingItem = null;
    protected int _originalIdOfHoldingItem = -1;

    protected void Start()
    {
        if (_grid)
        {
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

        InventoryItem item = _inventory.GetItemInSpace(id);

        if (item && State == CursorState.Empty)
        {
            State = CursorState.HoveringItem;
            StateChanged?.Invoke(State);
        }
        else if (!item && State == CursorState.HoveringItem)
        {
            State = CursorState.Empty;
            StateChanged?.Invoke(State);
        }

        SelectedItemChanged?.Invoke(item);
    }

    public int KeepSelectionInRange(int newId)
    {
        int result = SelectedItem;

        if (newId >= 0 && newId < _grid.Columns * _grid.Rows)
        {
            result = newId;
        }
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

            SelectedItemChanged?.Invoke(_holdingItem);

            State = CursorState.HoveringItem;
            StateChanged?.Invoke(State);
        }
        else
        {
            _holdingItem = _inventory?.PickUpItemAt(SelectedItem, transform);
            _originalIdOfHoldingItem = SelectedItem;

            State = CursorState.HoldingItem;
            StateChanged?.Invoke(State);
        }
    }
}
