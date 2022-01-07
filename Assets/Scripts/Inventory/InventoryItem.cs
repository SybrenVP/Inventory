using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New_InventoryItem", menuName = "Inventory/NewItem")]
public class InventoryItem : ScriptableObject
{
    public GameObject VisualPrefab = null;

    public string ItemName = "";
    public Sprite Image = null;

    protected ItemVisual _activeVisual = null;

    public void Initialize(Transform parent)
    {
        if (_activeVisual)
        {
            Debug.LogError("This Inventory Item instance already has an _activeVisual, you just tried to create a new one");
            return;
        }

        _activeVisual = Instantiate(VisualPrefab, parent).GetComponent<ItemVisual>();
        if (!_activeVisual)
        {
            Debug.LogError("Failed to initialize ItemVisual for item: " + ItemName + ", passed an object without itemvisual attached");
            return;
        }

        _activeVisual.Initialize(ItemName, Image);
    }

    public void AssignSpace(Transform parent)
    {
        _activeVisual.transform.SetParent(parent);
        _activeVisual.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    }

    public ItemVisual GetActiveVisual()
    {
        return _activeVisual;
    }
}
