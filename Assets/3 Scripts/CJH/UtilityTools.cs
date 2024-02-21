using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ��� ������ ����� �� �ִ� �Լ� ����
public static class UtilityTools
{
    public static bool AddItemToInventory(ItemData item, int count)
    {
        #region Allocate
        List<SlotItem> list = new();
        int maxItemCount = Director.maxItemCount;

        if (item.itemType == ItemType.Seed)
        {
            list = Director.userVariable.seedInven;
        }
        else if(item.itemType == ItemType.Scroll)
        {
            list = Director.userVariable.scrollInven;
        }
        else if (item.itemType == ItemType.Harvest)
        {
            list = Director.userVariable.harvestInven;
        }
        #endregion

        if (IsInventoryFull(item, count, list))
        {
            return false;
        }

        bool flag = true;

        if (ItemType.Scroll != item.itemType)
        {
            for (int i = 0; i < list.Count; i++)
            {
                SlotItem tempSlot = list[i];

                if (tempSlot.item != null)
                {
                    if (tempSlot.item.id == item.id)
                    {
                        if (count + tempSlot.itemCount > maxItemCount)
                        {
                            int remainder = maxItemCount - tempSlot.itemCount;
                            Director.userVariable.itemRetention.Push(item.id, remainder);
                            // 23.7.15�߰�
                            tempSlot.itemCount = maxItemCount;

                            count -= (remainder);
                        }
                        else
                        {
                            tempSlot.itemCount += count;
                            Director.userVariable.itemRetention.Push(item.id, count);

                            flag = false;
                            break;
                        }
                    }
                }
            }
        }

        if (flag)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].item == null)
                {
                    list[i].item = item;
                    if(count > maxItemCount)
                    {
                        list[i].itemCount = maxItemCount;
                        count -= maxItemCount;
                        Director.userVariable.itemRetention.Push(item.id, maxItemCount);
                        AddItemToInventory(item, count);
                    }
                    else
                    {
                        list[i].itemCount = count;
                        Director.userVariable.itemRetention.Push(item.id, count);  
                    }
                    break;
                }
            }
        }


        return true;
    }

    public static bool IsInventoryFull(ItemData item, int count, List<SlotItem> list)
    {

        // �κ��丮 �� �� ������ ������ �ٷ� false ����
        for(int i = 0; i < list.Count; i++)
        {
            if(list[i].IsEmpty())
            {
                return false;
            }
        }

        // �������� ��ũ���� ��� �� ������ ������ �߰��� �� ����
        if(item.itemType == ItemType.Scroll)
        {
            return true;
        }

        // �� ������ ���µ� ���� �������� count��ŭ ���� ������ �ִٸ�
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].CanAdd(item, count))
            {
                return false;
            }
        }

        return true;
    }

    public static int GetEmptySlotCount(ItemType itemType)
    {
        int result = 0;

        #region Allocate
        List<SlotItem> list = new();
        int maxItemCount = Director.maxItemCount;

        if (itemType == ItemType.Seed)
        {
            list = Director.userVariable.seedInven;
        }
        else if (itemType == ItemType.Scroll)
        {
            list = Director.userVariable.scrollInven;
        }
        else if (itemType == ItemType.Harvest)
        {
            list = Director.userVariable.harvestInven;
        }
        #endregion

        for(int i = 0; i < list.Count; i++)
        {
            if (list[i].IsEmpty())
            {
                result++;
            }
        }

        Debug.Log(result);

        return result;
    }

    public static void ItemCountChange(string id, int count)
    {
        List<SlotItem> list = new();
        switch (id[0])
        {
            case 'S':
                list = Director.userVariable.seedInven;
                break;
            case 'H':
                list = Director.userVariable.harvestInven;
                break;
        }

        for(int i = 0; i < list.Count; i++)
        {
            if (list[i].item == null)
                continue;

            if (list[i].item.id == id)
            {
                if (list[i].itemCount > count)
                {
                    list[i].ChangeCount(count);
                    Director.userVariable.itemRetention.Push(id, count);
                    return;
                }
                else
                {
                    list[i].ChangeCount(list[i].itemCount);
                    Director.userVariable.itemRetention.Push(id, list[i].itemCount);

                    count -= list[i].itemCount;
                }
            }

            if (count <= 0)
                break;
        }
    }

    public static void FadeIn(Image image)
    {
        Color color = new Color(0, 0, 0, 0);
        image.color = color;
        image.DOFade(1, 1f);
    }

    public static void FadeOut(Image image)
    {
        Color color = new Color(0, 0, 0, 1);
        image.color = color;
        image.DOFade(0, 1f);
    }

}
