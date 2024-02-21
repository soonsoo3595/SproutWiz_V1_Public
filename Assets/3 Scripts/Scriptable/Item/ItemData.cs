using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/Item")]
public class ItemData : ScriptableObject
{
    // ������ ����
    [Header("Common")]
    public string id;
    public Sprite image;
    public string itemName;
    public ItemType itemType;
    [TextArea(1, 3)]
    public string description;

    public int GetSeedGrade() => int.Parse(id[1].ToString());
}