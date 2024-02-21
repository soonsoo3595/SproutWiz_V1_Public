using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/Product")]
public class ProductData : ScriptableObject
{
    public string productID;    // ��ǰ �� ID
    public string id;           // ���� ������ ID ���� ����

    public Sprite image;
    public string Name;

    [TextArea(1, 3)]
    public string description;

    public int price;

    public ItemTier itemTier;
    public ItemType itemType;
    
}
