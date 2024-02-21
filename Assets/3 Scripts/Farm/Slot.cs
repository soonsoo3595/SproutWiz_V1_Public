using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SlotItem
{
    public ItemData item;
    public int itemCount;

    public SlotItem()
    {
        item = null;
        itemCount = 0;
    }

    public void Empty()
    {
        item = null;
        itemCount = 0;
    }

    public bool IsEmpty()
    {
        if (item == null)
            return true;
        else
            return false;
    }

    public bool CanAdd(ItemData item, int count)
    {
        if(this.item.id != item.id)
        {
            return false;
        }

        if(itemCount + count > Director.maxItemCount)
        {
            return false;
        }

        return true;
    }

    public void ChangeCount(int count)
    {
        itemCount += count;

        if(itemCount <= 0)
            Empty();
    }
}

public class Slot : MonoBehaviour
{
    [SerializeField] private GameObject countImage;
    private Text countText;
    private Button btn;
    private InventoryMgr inventoryMgr;

    [HideInInspector] public SlotItem slotItem;
    [HideInInspector] public Image image;

    void Awake()
    {
        image = GetComponent<Image>();
        btn = GetComponent<Button>();
        countText = countImage.GetComponentInChildren<Text>();
        slotItem = new SlotItem();
    }

    void Start()
    {
        inventoryMgr = InventoryMgr.instance;
    }

    void Update()
    {
        if(slotItem.item != null)
        {
            btn.interactable = true;
        }
        else
        {
            btn.interactable = false;
        }
    }

    private void SetColor(float alpha)
    {
        Color color = image.color;
        color.a = alpha;
        image.color = color;
    }

    public void ActiveButton(bool flag)
    {
        btn.interactable = flag;
    }

    public void AddItem(ItemData item, int count = 1)
    {
        slotItem.item = item;
        slotItem.itemCount = count;
        btn.interactable = true;

        image.sprite = item.image;
        // SetScrollImage(InventoryMgr.instance.isShowBlock);

        if (slotItem.item.itemType != ItemType.Scroll)
        {
            countImage.SetActive(true);
            countText.text = slotItem.itemCount.ToString();
        }
        else
        {
            countImage.SetActive(false);
        }
        SetColor(1);
    }

    public void SetSlotCount(int count)
    {
        slotItem.itemCount += count;
        countText.text = slotItem.itemCount.ToString();

        if (slotItem.itemCount <= 0)
            ClearSlot();
    }

    public void ClearSlot()
    {
        slotItem.Empty();
        image.sprite = null;
        btn.interactable = false;
        SetColor(0);

        countImage.SetActive(false);
    }

    public void ChangeSlot()
    {
        Slot dragSlot = DragSlot.instance.slot;
        ItemData tempItem = slotItem.item;
        int tempItemCount = slotItem.itemCount;

        if (dragSlot == this)
            return;
        else if (inventoryMgr.isShowBlock)
            return;


        if(tempItem != null)
        {
            if (tempItem.id == dragSlot.slotItem.item.id)
            {
                if (tempItemCount + dragSlot.slotItem.itemCount > Director.maxItemCount)
                {
                    int remainder = Director.maxItemCount - slotItem.itemCount;
                    SetSlotCount(remainder);
                    dragSlot.SetSlotCount(-remainder);
                }
                else
                {
                    SetSlotCount(dragSlot.slotItem.itemCount);
                }
            }
            else
            {
                AddItem(dragSlot.slotItem.item, dragSlot.slotItem.itemCount);
                dragSlot.AddItem(tempItem, tempItemCount);
            }
        }
        else
        {
            AddItem(dragSlot.slotItem.item, dragSlot.slotItem.itemCount);
            dragSlot.ClearSlot();
        }

        inventoryMgr.SaveSlot(inventoryMgr.currentTab);

    }
    /* 블록 이미지 개선 
    public void SetScrollImage(bool flag)
    {
        if(flag)
        {
            string fileName = slotItem.item.block.name; // $"{item.scrollBlockPrefab.name}.png";
            string filePath = Application.dataPath + "/Resources/ScrollBlock/" + fileName;

            if(BlockImage.instance.blockDictionary.dict.ContainsKey(fileName))
            {
                image.sprite = BlockImage.instance.blockDictionary.dict[fileName];
            }
            else
            {
                BlockImage.instance.Generate(slotItem.item.block);
                SetScrollImage(flag);
            }
        }
        else
        {
            image.sprite = slotItem.item.image;
        }

    }
    */
    public void UseItem()
    {
        SetSlotCount(-1);
        inventoryMgr.SaveSlot(inventoryMgr.currentTab);
    }
}
