using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemClickPopup : MonoBehaviour
{
    public Transform popup;
    public Text itemName;
    public Text itemDesc;
    public GameObject text1;
    public GameObject text2;
    public GameObject element;

    public Sprite[] elementSprites;

    public GameObject scrollShape;

    public void SetPopup(ItemData item, Vector3 position)
    {
        popup.position = position;
        itemName.text = item.itemName;
        itemDesc.text = item.description;

        switch (item.itemType)
        {
            case ItemType.Seed:
                SeedPopup(item.id);
                break;
            case ItemType.Scroll:
                ScrollItem scroll = (ScrollItem)item;
                ScrollPopup(scroll);
                break;
            case ItemType.Harvest:
                HarvestPopup(item.id);
                break;
        }
    }

    public void SeedPopup(string id)
    {
        PlantItem plant = GameMgr.Plants.Get(id);

        element.SetActive(false);
        text1.SetActive(true);
        text2.SetActive(true);

        text1.GetComponent<Text>().text = $"�ܰ� : {plant.grade}";
        text2.GetComponent<Text>().text = $"�ִ� ���� ����Ʈ : {plant.GetMaxGrowPoint()}";
    }

    public void ScrollPopup(ScrollItem scroll)
    {
        element.SetActive(true);
        text1.SetActive(true);
        text2.SetActive(true);

        text1.GetComponent<Text>().text = $"Ƽ�� : {scroll.tier}";
        text2.GetComponent<Text>().text = $"�ܰ� : {scroll.grade}";

        Image image = element.GetComponent<Image>();

        if (scroll.element == Element.Fire) image.sprite = elementSprites[0];
        else if (scroll.element == Element.Water) image.sprite = elementSprites[1];
        else if (scroll.element == Element.Grass) image.sprite = elementSprites[2];

        scrollShape.GetComponent<WorkShop.ResultPanel>().FarmTest(scroll.shape, scroll.tier, scroll.element);

    }

    public void HarvestPopup(string id)
    {
        int pID = int.Parse(id[2..]);
        PlantItem plant = GameMgr.Plants.Get(pID);

        element.SetActive(false);
        text1.SetActive(true);
        text2.SetActive(true);

        text1.GetComponent<Text>().text = $"�ܰ� : {plant.grade}";
        text2.GetComponent<Text>().text = $"���� �ü� : {plant.harvestCost}G";
    }
}
