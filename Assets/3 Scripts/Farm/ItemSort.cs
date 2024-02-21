using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSort : MonoBehaviour
{
    Button sortBtn;
    InventoryMgr inventoryMgr;

    public GameObject[] popup;
    public Transform popupPos;

    public Toggle seedGradeToggle;
    public Toggle seedCountToggle;
    public Toggle scrollTierToggle;
    public Toggle scrollGradeToggle;
    public Toggle scrollElementToggle;


    void Awake()
    {
        sortBtn = GetComponent<Button>();    
    }

    void Start()
    {
        inventoryMgr = InventoryMgr.instance;
        sortBtn.onClick.AddListener(OpenPopup);

        seedGradeToggle.onValueChanged.AddListener(SeedGradeSort);
        seedCountToggle.onValueChanged.AddListener(SeedCountSort);
        scrollTierToggle.onValueChanged.AddListener(ScrollTierSort);
        scrollGradeToggle.onValueChanged.AddListener(ScrollGradeSort);
        scrollElementToggle.onValueChanged.AddListener(ScrollElementSort);
    }

    public void OpenPopup()
    {
        GameMgr.Instance.soundEffect.PlayOneShotSoundEffect("sort");

        SetPopupTransform();

        switch (inventoryMgr.currentTab)
        {
            case ItemType.Seed:
                popup[0].gameObject.SetActive(true);
                popup[1].gameObject.SetActive(false);
                popup[2].gameObject.SetActive(false);
                break;
            case ItemType.Scroll:
                popup[0].gameObject.SetActive(false);
                popup[1].gameObject.SetActive(true);
                popup[2].gameObject.SetActive(false);
                break;
            case ItemType.Harvest:
                popup[0].gameObject.SetActive(false);
                popup[1].gameObject.SetActive(false);
                popup[2].gameObject.SetActive(true);
                break;
        }
    }

    public void SetPopupTransform()
    {
        popup[0].transform.position = popupPos.position;
        popup[1].transform.position = popupPos.position;
        popup[2].transform.position = popupPos.position;
    }

    public void SeedGradeSort(bool isOn)
    {
        GameMgr.Instance.soundEffect.PlayOneShotSoundEffect("sort");

        if (isOn)
        {
            inventoryMgr.SortSlot(0);
        }
        else
        {
            inventoryMgr.SortSlot(1);
        }
    }

    public void SeedCountSort(bool isOn)
    {
        GameMgr.Instance.soundEffect.PlayOneShotSoundEffect("sort");

        if (isOn)
        {
            inventoryMgr.SortSlot(2);
        }
        else
        {
            inventoryMgr.SortSlot(3);
        }
    }

    public void ScrollTierSort(bool isOn)
    {
        GameMgr.Instance.soundEffect.PlayOneShotSoundEffect("sort");

        if (isOn)
        {
            inventoryMgr.SortSlot(4);
        }
        else
        {
            inventoryMgr.SortSlot(5);
        }
    }

    public void ScrollGradeSort(bool isOn)
    {
        GameMgr.Instance.soundEffect.PlayOneShotSoundEffect("sort");

        if (isOn)
        {
            inventoryMgr.SortSlot(6);
        }
        else
        {
            inventoryMgr.SortSlot(7);
        }
    }

    public void ScrollElementSort(bool isOn)
    {
        GameMgr.Instance.soundEffect.PlayOneShotSoundEffect("sort");

        if (isOn)
        {
            inventoryMgr.SortSlot(8);
        }
        else
        {
            inventoryMgr.SortSlot(9);
        }
    }

}
