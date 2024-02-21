using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class InventoryMgr : MonoBehaviour
{
    [SerializeField] private Transform slotParent;
    [SerializeField] private ScrollRect scrollRect;

    [SerializeField] public GameObject TempPreview;

    [Space]
    private List<Slot> inventory;
    private List<SlotItem> seedInventory;
    private List<SlotItem> scrollInventory;
    private List<SlotItem> harvestInventory;
    private List<SlotItem> tempInventory;
    private RectTransform inventoryRect;

    public static InventoryMgr instance;
    [HideInInspector] public bool isShowBlock;
    public ItemType currentTab;
    

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        inventory = slotParent.GetComponentsInChildren<Slot>().ToList();
        seedInventory = Director.userVariable.seedInven; 
        scrollInventory = Director.userVariable.scrollInven; 
        harvestInventory = Director.userVariable.harvestInven; 
        inventoryRect = GetComponent<RectTransform>();
    }

    public void LoadSlot(ItemType itemType)
    {
        switch(itemType)
        {
            case ItemType.Seed:
                tempInventory = seedInventory;
                break;
            case ItemType.Scroll:
                tempInventory = scrollInventory;
                break;
            case ItemType.Harvest:
                tempInventory = harvestInventory;
                break;
        }
    }

    public void RefreshSlot(ItemType itemType)
    {
        // 인벤토리에 변경사항이 있으면 호출되어 인벤토리를 Refresh함
        if(currentTab != itemType)
        {
            return;
        }

        UpdateSlot();

        //
        TempPreview.SetActive(false);
    }

    public void AcquireItem(ItemData item, int count = 1)
    {
        // 전역함수인 AddItemToInventory에서 아이템 추가
        UtilityTools.AddItemToInventory(item, count);
        RefreshSlot(item.itemType);
    }

    public void UpdateSlot()
    {
        LoadSlot(currentTab);

        for (int i = 0; i < Director.inventorySize; i++)
        {
            if (tempInventory[i].item != null)
            {
                inventory[i].AddItem(tempInventory[i].item, tempInventory[i].itemCount);
            }
            else
            {
                inventory[i].ClearSlot();
            }
        }

        //
        TempPreview.SetActive(false);
    }

    public void SaveSlot(ItemType itemType)
    {
        LoadSlot(itemType);

        for(int i = 0; i < Director.inventorySize; i++)
        {
            if (inventory[i].slotItem.item != null)
            {
                tempInventory[i].item = inventory[i].slotItem.item;
                tempInventory[i].itemCount = inventory[i].slotItem.itemCount;
            }
            else
            {
                tempInventory[i].Empty();
            }
        }
    }

    public float OutOfInventotyRect()
    {
        Vector3[] corners = new Vector3[4];
        inventoryRect.GetWorldCorners(corners);

        Vector3 bottomRight = corners[3];

        return bottomRight.x;
    }
    
    public void SortSlot(int type)
    {
        LoadSlot(currentTab);

        #region ���� Ÿ��
        IOrderedEnumerable<SlotItem> sortedResult = type switch
        {
            0 => tempInventory.OrderBy(obj => obj.item == null).ThenBy(obj => obj.item != null ? obj.item.GetSeedGrade() : 0),
            1 => tempInventory.OrderBy(obj => obj.item == null).ThenByDescending(obj => obj.item != null ? obj.item.GetSeedGrade() : 0),
            2 => tempInventory.OrderBy(obj => obj.item == null).ThenBy(obj => obj.item != null ? obj.itemCount : 0),
            3 => tempInventory.OrderBy(obj => obj.item == null).ThenByDescending(obj => obj.item != null ? obj.itemCount : 0),
            4 => tempInventory.OrderBy(obj => obj.item == null).ThenBy(obj => obj.item != null ? (obj.item as ScrollItem).tier : 0),
            5 => tempInventory.OrderBy(obj => obj.item == null).ThenByDescending(obj => obj.item != null ? (obj.item as ScrollItem).tier : 0),
            6 => tempInventory.OrderBy(obj => obj.item == null).ThenBy(obj => obj.item != null ? (obj.item as ScrollItem).grade : 0),
            7 => tempInventory.OrderBy(obj => obj.item == null).ThenByDescending(obj => obj.item != null ? (obj.item as ScrollItem).grade : 0),
            8 => tempInventory.OrderBy(obj => obj.item == null).ThenBy(obj => obj.item != null ? (obj.item as ScrollItem).element : 0),
            9 => tempInventory.OrderBy(obj => obj.item == null).ThenByDescending(obj => obj.item != null ? (obj.item as ScrollItem).element : 0),
            _ => null,
        };
        tempInventory = sortedResult.ToList();

        #endregion

        for (int i = 0; i < Director.inventorySize; i++)
        {
            if (tempInventory[i].item != null)
            {
                inventory[i].AddItem(tempInventory[i].item, tempInventory[i].itemCount);
            }
            else
            {
                inventory[i].ClearSlot();
            }    
        }

        SaveSlot(currentTab);
    }

    /*

    public void ToggleClick()
    {
        if (Director.isPreview)
            return;

        if (!Director.isDrag)
        {
            isShowBlock = !isShowBlock;
            // ChangeImage();
        }
    }

    public void ChangeImage()
    {
        for(int i = 0; i < slotSize; i++)
        {
            if (inventory[i].slotItem.item != null)
                inventory[i].SetScrollImage(isShowBlock);
        }
    }
    */

    public void TempToggle()
    {
        if (TempPreview.activeSelf)
        {
            TempPreview.SetActive(false);
        }
        else
        {
            TempPreview.SetActive(true);
        }
    }
}
