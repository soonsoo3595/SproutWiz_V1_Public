using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WorkShop
{
    public class Manager : MonoBehaviour
    {
        [SerializeField] public TestData.UserData userData;
        [SerializeField] public TestData.PrefebDB PrefebDB;

        public static Manager instance;
        public TestData.CraftingItmeDB craftingItmeDB;

        public int selectedTier { get; set; }
        public int productionCount { get; set; }

        public List<TestData.CraftingScrollData> craftingScrollData;
        [HideInInspector] public Element selectedElement;


        [SerializeField] Sprite[] fireScroll;
        [SerializeField] Sprite[] WaterScroll;
        [SerializeField] Sprite[] GrassScroll;

        [SerializeField] Sprite[] tierImages;

        private void Awake()
        {
            if (instance == null) instance = this;

            selectedTier = 1;
            productionCount = 1;

            craftingItmeDB = new TestData.CraftingItmeDB();
            craftingItmeDB.MakeDB();

            craftingScrollData = new List<TestData.CraftingScrollData>();
        }

        public int GetUserCircle()
        {
            return userData.circle;
        }

        public List<bool> GetScroll(int tear, int level, int num)
        {
            Scroll scroll = null;

            if (level == 1)
            {
                scroll = Instantiate(PrefebDB.scrollPrefebDB[tear - 1].tear_Level1_Scrolls[num].GetComponent<Scroll>());
            }
            else if(level == 2)
            {
                scroll = Instantiate(PrefebDB.scrollPrefebDB[tear - 1].tear_Level2_Scrolls[num].GetComponent<Scroll>());
            }
            else if(level == 3)
            {
                scroll = Instantiate(PrefebDB.scrollPrefebDB[tear - 1].tear_Level3_Scrolls[num].GetComponent<Scroll>());
            }
            else
            {
                Debug.Log($"Error:잘못된 Level 선택, {level}레벨 선택됨.");
            }

            List<bool> test = scroll.GetTest();
            Destroy(scroll.gameObject);

            return test;
        }

        public GameObject GetBlockObject(int tear, int level, int num)
        {
            GameObject gameObject = null;

            if (level == 1)
            {
                gameObject = PrefebDB.scrollPrefebDB[tear - 1].tear_Level1_Scrolls[num];
            }
            else if (level == 2)
            {
                gameObject = PrefebDB.scrollPrefebDB[tear - 1].tear_Level2_Scrolls[num];
            }
            else if (level == 3)
            {
                gameObject = PrefebDB.scrollPrefebDB[tear - 1].tear_Level3_Scrolls[num];
            }
            else
            {
                Debug.Log($"Error:잘못된 Level 선택, {level}레벨 선택됨.");
            }

            return gameObject;
        }

        public int GetPrefebLength(int tear, int level)
        {
            int length = 0;

            if (level == 1)
            {
                length = PrefebDB.scrollPrefebDB[tear - 1].tear_Level1_Scrolls.Length;
            }
            else if (level == 2)
            {
                length = PrefebDB.scrollPrefebDB[tear - 1].tear_Level2_Scrolls.Length;
            }
            else if (level == 3)
            {
                length = PrefebDB.scrollPrefebDB[tear - 1].tear_Level3_Scrolls.Length;
            }

            return length;
        }

        public Sprite GetScrollImage(Element element, int tier)
        {
            switch (element)
            {
                case Element.Fire:
                    return fireScroll[tier - 1];
                case Element.Water:
                    return WaterScroll[tier - 1];
                case Element.Grass:
                    return GrassScroll[tier - 1];

            }

            return null;
        }

        public Sprite GetTierImage(int tier)
        {
            return tierImages[tier - 1];
        }
    }
}

   
