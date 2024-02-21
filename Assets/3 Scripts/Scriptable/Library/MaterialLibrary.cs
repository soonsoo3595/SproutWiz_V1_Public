using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/Library/Materials")]
public class MaterialLibrary : ScriptableObject
{
    public List<MaterialItem> DB = null;

    public MaterialItem Get(string mID) => DB.FirstOrDefault(_ => _.id.Equals(mID));

    public int CountMaterial() => DB.Count();
}
