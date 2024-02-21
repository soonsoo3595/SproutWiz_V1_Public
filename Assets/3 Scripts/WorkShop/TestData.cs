using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestData : MonoBehaviour
{
    [System.Serializable]
    public class UserData
    {
        public int circle;
    }

    public class Item
    {
        public string name;
        public int id;
    }

    public class CraftingItem : Item
    {
        public Dictionary<string, int> requiredMaterials;
        public int requiredSpell;
    }

    public class CraftingItmeDB
    {
        public List<CraftingItem> items = new List<CraftingItem>();

        public void MakeDB()
        {
            CraftingItem item1 = new CraftingItem();
            item1.name = "1티어 스크롤";
            item1.id = 1;

            item1.requiredSpell = 3;
            item1.requiredMaterials = new Dictionary<string, int> 
            {
                { "M00", 1 },
            }; 

            CraftingItem item2 = new CraftingItem();
            item2.name = "2티어 스크롤";
            item2.id = 2;

            item2.requiredSpell = 5;
            item2.requiredMaterials = new Dictionary<string, int> 
            {
                { "M00", 4 },
            };
                                                     
            CraftingItem item3 = new CraftingItem(); 
            item3.name = "3티어 스크롤";                  
            item3.id = 3;

            item3.requiredSpell = 7;
            item3.requiredMaterials = new Dictionary<string, int> 
            {
                { "M00", 6 },
            };

            items.Add(item1);
            items.Add(item2);
            items.Add(item3);
        }
    }

    public class CraftingScrollData
    {
        public int tier;
        public int level;
        public int number;
        public Element element;
        public int count;
    }

    [System.Serializable]
    public class PrefebDB
    {
        public List<ScrollPrefeb> scrollPrefebDB;
    }

    [System.Serializable]
    public class ScrollPrefeb
    {
        public GameObject[] tear_Level1_Scrolls;
        public GameObject[] tear_Level2_Scrolls;
        public GameObject[] tear_Level3_Scrolls;
    }

}
