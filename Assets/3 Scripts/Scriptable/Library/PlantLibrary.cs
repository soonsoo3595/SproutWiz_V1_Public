using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/Library/Plants")]
public class PlantLibrary : ScriptableObject
{
    public List<PlantItem> DB = null;

    public PlantItem Get(string sID) => DB.FirstOrDefault(_ => _.seed.id.Equals(sID));
    public PlantItem Get(int pID) => DB.FirstOrDefault(_ => _.id.Equals(pID));

    public int CountPlant() => DB.Count();

    
}
