using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/Product")]
public class ProductData : ScriptableObject
{
    public string productID;    // 상품 상 ID
    public string id;           // 실제 아이템 ID 매핑 위함

    public Sprite image;
    public string Name;

    [TextArea(1, 3)]
    public string description;

    public int price;

    public ItemTier itemTier;
    public ItemType itemType;
    
}
