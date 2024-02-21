using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/Library/Product")]
public class ProductLibrary : ScriptableObject
{
    public List<ProductData> Tier1 = null;
    public List<ProductData> Tier2 = null;
    public List<ProductData> Tier3 = null;

    public ProductData Get(string id)
    {
        switch (id[0])
        {
            case '1':
                return Tier1.FirstOrDefault(_ => _.productID.Equals(id));
            case '2':
                return Tier2.FirstOrDefault(_ => _.productID.Equals(id));
            case '3':
                return Tier3.FirstOrDefault(_ => _.productID.Equals(id));
            default:
                return null;
        }
    }

    public int CountProduct(int tier)
    {
        switch(tier)
        {
            case 1:
                return Tier1.Count();
            case 2:
                return Tier2.Count();
            case 3:
                return Tier3.Count();
            default:
                return 0;
        }

    }    
}
