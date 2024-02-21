using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WorkShop
{
    public class ScrollPanel : MonoBehaviour
    {
        Unit[] units;

        public void printShape(int tier, int level, int num, Element element)
        {
            units = GetComponentsInChildren<Unit>();

            List<bool> test = Manager.instance.GetScroll(tier, level, num);

            int temp = 0;
            int count = 0;
            foreach (bool enable in test)
            {
                // 티어별로 불러오는 길이 바꾸던가 해야함.
                if (Manager.instance.selectedTier == 1)
                {
                    if (count == 3 || count == 7 || count >= 11)
                    {

                    }
                    else
                    {
                        units[temp].SetColor(element);
                        units[temp].Toggle(enable);
                        temp++;
                    }
                }
                else if (Manager.instance.selectedTier == 2)
                {
                    if (count < 12)
                    {
                        units[temp].SetColor(element);
                        units[temp].Toggle(enable);
                        temp++;
                    }
                }
                else
                {
                    units[temp].SetColor(element);
                    units[temp].Toggle(enable);
                    temp++;
                }

                count++;
            }
        }

        public void printFarm(List<bool> vs, int tier, Element element)
        {
            units = GetComponentsInChildren<Unit>();

            List<bool> test = vs;

            int temp = 0;
            int count = 0;
            foreach (bool enable in test)
            {
                // 티어별로 불러오는 길이 바꾸던가 해야함.
                if (tier == 1)
                {
                    if (count == 3 || count == 7 || count >= 11)
                    {

                    }
                    else
                    {
                        units[temp].SetColor(element);
                        units[temp].Toggle(enable);
                        temp++;
                    }
                }
                else if (tier == 2)
                {
                    if (count < 12)
                    {
                        units[temp].SetColor(element);
                        units[temp].Toggle(enable);
                        temp++;
                    }
                }
                else
                {
                    units[temp].SetColor(element);
                    units[temp].Toggle(enable);
                    temp++;
                }

                count++;
            }
        }
    }
}

