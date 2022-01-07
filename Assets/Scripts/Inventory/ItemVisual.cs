using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ItemVisual : MonoBehaviour
{
    [HideInInspector] public string ItemName = "";

    [SerializeField] protected Image ItemImageRenderer = null;
    
    public void Initialize(string name, Sprite image)
    {
        if (ItemImageRenderer == null)
            ItemImageRenderer = GetComponent<Image>();

        ItemName = name;
        ItemImageRenderer.sprite = image;

        gameObject.name = "Item_" + name;
    }
}
