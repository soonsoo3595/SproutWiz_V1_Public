using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TabMgr : MonoBehaviour
{
    public static TabMgr instance;
    public TabBtn[] TabBtns;
    public Scrollbar tabScroll;
    public GameObject scrollToggle;

    [Header("Tab Image")]
    public Image tabImage;
    public Sprite[] tabSprites;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        Init();
    }

    public void Init()
    {
        ClickBtn(ItemType.Seed);
    }

    public void ClickBtn(ItemType itemType)
    {
        InventoryMgr.instance.currentTab = itemType;

        switch (itemType)
        {
            case ItemType.Seed:
                scrollToggle.SetActive(false);
                InventoryMgr.instance.isShowBlock = false;
                break;
            case ItemType.Scroll:
                scrollToggle.SetActive(true);
                break;
            case ItemType.Harvest:
                scrollToggle.SetActive(false);
                InventoryMgr.instance.isShowBlock = false;
                break;  
        }

        ChangeTabImage(itemType);

        // 스크롤 맨 위로 올려줌
        tabScroll.value = 1f;

        InventoryMgr.instance.RefreshSlot(itemType);
    }

    /* 리소스 적용으로 필요 없어짐
    public void SetBtnAlpha(ItemType itemType)
    {
        for (int i = 0; i < TabBtns.Length; i++)
        {
            if (TabBtns[i].itemType == itemType)
            {
                Color newColor = TabBtns[i].GetComponent<Image>().color;
                newColor.a = 1.0f;
                TabBtns[i].GetComponent<Image>().color = newColor;
            }
            else
            {
                Color newColor = TabBtns[i].GetComponent<Image>().color;
                newColor.a = 0.3f;
                TabBtns[i].GetComponent<Image>().color = newColor;
            }
        }
    }
    */

    public void ChangeTabImage(ItemType itemType)
    {
        switch(itemType)
        {
            case ItemType.Seed:
                tabImage.sprite = tabSprites[0];
                break;
            case ItemType.Scroll:
                tabImage.sprite = tabSprites[1];
                break;
            case ItemType.Harvest:
                tabImage.sprite = tabSprites[2];
                break;
        }
    }
}
