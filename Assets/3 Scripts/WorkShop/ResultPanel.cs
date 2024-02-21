using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace WorkShop
{
    public class ResultPanel : MonoBehaviour
    {
        [SerializeField] GameObject[] Tear;
        [SerializeField] TextMeshProUGUI countText;
        
        public int level, num, count;
        int tier;
        Element element;

        private void SelectedShowTier()
        {
          
            for (int i = 0; i<3; i++)
            {
                if(tier-1 == i)
                {
                    Tear[i].SetActive(true);
                    Tear[i].GetComponent<ScrollPanel>().printShape(tier, level, num, element);
                }
                else
                {
                    Tear[i].SetActive(false);
                }
            }
        }

        public void SetScrollData(TestData.CraftingScrollData scrollData)
        {
            tier = Manager.instance.selectedTier;

            this.level = scrollData.level;
            this.num = scrollData.number;
            this.count = scrollData.count;
            this.element = scrollData.element;

            countText.text = $"x{count}";

            SelectedShowTier();
        }

        public void FarmTest(List<bool> vs, int tier, Element element)
        {
            for (int i = 0; i < 3; i++)
            {
                if (tier - 1 == i)
                {
                    Tear[i].SetActive(true);
                    Tear[i].GetComponent<ScrollPanel>().printFarm(vs, tier, element);
                }
                else
                {
                    Tear[i].SetActive(false);
                }
            }
        }
    }
}
