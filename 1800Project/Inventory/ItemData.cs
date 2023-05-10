using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObjects/ItemData" , order = -2)]
public class ItemData : ScriptableObject
{
    [SerializeField] Sprite _itemImage;
    [SerializeField] string _itemName;
    [SerializeField] string _itemInfo;
    [SerializeField] int _itemCount;

    public Sprite ItemImage
    {
        get => _itemImage;
    }
    public string ItemName
    {
        get => _itemName;
    }
    public string ItemInfo
    {
        get => _itemInfo;
    }
    public int ItemCount
    {
        get => _itemCount;
        set => _itemCount = value;
    }
}
