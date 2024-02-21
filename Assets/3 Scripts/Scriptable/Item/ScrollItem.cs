using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/Item/Scroll")]
public class ScrollItem : ItemData
{
    public int tier;
    public int grade;
    public GameObject block;
    public Element element;

    public List<bool> shape;
}
