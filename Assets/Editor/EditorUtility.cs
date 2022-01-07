using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class EditorUtility
{
    [MenuItem("Utility/AddTexturesToItems")]
    public static void FillTexturesIntoCorrectItems()
    {
        InventoryItem[] items = ItemHandler.GetAllItems();

        var textures = AssetDatabase.LoadAllAssetsAtPath("Assets/Textures/Items/Full sheet (16x16).png");
        Debug.Log("Loaded " + textures.Length + " textures");

        foreach (InventoryItem item in items)
        {
            if (!item.Image)
            {
                foreach(var texture in textures)
                {
                    if (texture.name.Length < 19)
                        continue;

                    int textureId = int.Parse(texture.name.Substring(19));
                    int itemId = int.Parse(item.name.Substring(5));
                    if (textureId == itemId)
                    {
                        item.Image = texture as Sprite;
                        UnityEditor.EditorUtility.SetDirty(item);
                        break;
                    }
                }
            }
        }
    }
}
