using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class StoreMgr : MonoBehaviour
{
    [HideInInspector] public ProductData product;

    public Text goldTxt;

    public GameObject item;
    public GameObject sellItemprefab;

    public GameObject itemList;
    public GameObject itemContent;

    public GameObject toolTip;

    [Header("임시처리용")]
    public GameObject buyPopup;
    public GameObject sellPopup;
    public GameObject seedPopup;
    public GameObject back;

    public Button[] itmeType;
    public Text merchantText;

    public TopUI topUI;
    public BuyItem buyItem;
    public SellItem sellItem;
    public ItemListControl itemListControl;

    public StoreDialogue storeDialogue;

    void Start()
    {
        UpdateUI();
    }


    public void UpdateUI()
    {
        topUI.UpdateGoldText();
        topUI.UpdateSpellText();
    }

    public void SetProduct(string id)
    {
        GameMgr.Instance.soundEffect.PlayOneShotSoundEffect("drop");

        product = GameMgr.Products.Get(id);
        SetToolTip(true);
        storeDialogue.BuyProccess();
    }

    public void SetToolTip(bool flag)
    {
        toolTip.SetActive(flag);
        
        if(flag)
        {
            toolTip.transform.GetChild(0).GetComponent<Text>().text = product.Name;
            toolTip.transform.GetChild(1).GetComponent<Text>().text = product.description;
        }
    }

    public void Purchase(int count)
    {
        if (product.productID == "10")
        {
            PurchaseSeedBag(count);
        }
        else
        {
            Director.userVariable.itemRetention.Push(product.id, count);

            int totalCost = count * product.price;
            Director.userVariable.gold -= totalCost;
            UpdateUI();
        }

        GameMgr.Instance.soundEffect.PlayOneShotSoundEffect("cash");

    }

    public void PurchaseSeedBag(int count)
    {
        for (int i = 0; i < count; i++)
        {
            int rand = Random.Range(0, 6);
            PlantItem plant = GameMgr.Plants.Get(rand);

            if (UtilityTools.AddItemToInventory(plant.seed, 1))
            {
                Director.userVariable.gold -= product.price;
                UpdateUI();

            }
            else
            {
                Debug.Log("인벤토리가 꽉 차 구매할 수 없습니다");
                return;
            }

            Debug.Log(plant.id);
        }
    }
    /*
    public void Buy()
    {
        PlantItem plant = GameMgr.Plants.Get(0);
        SeedItem seed = plant.seed;

        if(Director.userVariable.gold < plant.seedCost)
        {
            Debug.Log("돈이 모자랍니다");
            return;
        }

        UtilityTools.AddItemToInventory(seed, 1);
        Director.userVariable.gold -= plant.seedCost;

        UpdateGold();
    }
    */

    public void OpenItemList(int type)
    {
        if(type == 0)
        {
            itemList.SetActive(true);
        }
        else
        {
            itmeType[0].gameObject.SetActive(true);

            itemList.SetActive(true);
        }
    }

    public void CloseItemList()
    {
        itemList.SetActive(false);
    }

    public void FillSeed()
    {
        itemListControl.EmptyList();

        GameObject prefab = Instantiate(item);

        prefab.name = "SeedBundle";
        prefab.transform.GetChild(1).GetComponent<Text>().text = "씨앗 주머니";
        // prefab.transform.GetChild(2).GetComponent<Text>().text = $"단계 : {plant.grade}";
        prefab.transform.GetChild(3).GetComponent<Text>().text = $"가격 : 300G";
        prefab.GetComponent<Button>().onClick.AddListener(() => buyItem.SetID(prefab.name));

        PopupBtn _popup = prefab.GetComponent<PopupBtn>();
        _popup.popup = seedPopup;
        _popup.back = back;

        prefab.transform.SetParent(itemContent.transform, false);
        /*
        int count = GameMgr.Plants.CountPlant();

        for(int i = 0; i < count; i++)
        {
            GameObject prefab = Instantiate(item);

            PlantItem plant = GameMgr.Plants.Get(i);

            Image image = prefab.transform.GetChild(0).GetComponent<Image>();
            image.sprite = plant.seed.image;

            prefab.name = plant.seed.id;
            prefab.transform.GetChild(1).GetComponent<Text>().text = plant.seed.itemName;
            prefab.transform.GetChild(2).GetComponent<Text>().text = $"단계 : {plant.grade}";
            prefab.transform.GetChild(3).GetComponent<Text>().text = $"가격 : {plant.seedCost}G";
            prefab.GetComponent<Button>().onClick.AddListener(() => buyItem.SetID(prefab.name));

            PopupBtn _popup = prefab.GetComponent<PopupBtn>();
            _popup.popup = buyPopup;
            _popup.back = back;

            prefab.transform.SetParent(itemContent.transform, false);
        }
        */
    }

    public void FillMaterial()
    {
        itemListControl.EmptyList();

        int count = GameMgr.Materials.CountMaterial();

        for (int i = 0; i < count; i++)
        {
            GameObject prefab = Instantiate(item);

            string mID = "M0" + i.ToString();
            MaterialItem material = GameMgr.Materials.Get(mID);

            Image image = prefab.transform.GetChild(0).GetComponent<Image>();
            image.sprite = material.image;

            prefab.name = material.id;
            prefab.transform.GetChild(1).GetComponent<Text>().text = material.itemName;
            // prefab.transform.GetChild(2).GetComponent<Text>().text = $"단계 : {material}";
            prefab.transform.GetChild(3).GetComponent<Text>().text = $"가격 : {material.cost}G";
            prefab.GetComponent<Button>().onClick.AddListener(() => buyItem.SetID(prefab.name));

            PopupBtn _popup = prefab.GetComponent<PopupBtn>();
            _popup.popup = buyPopup;
            _popup.back = back;

            prefab.transform.SetParent(itemContent.transform, false);
        }
    }

    public void FillHarvest()
    {
        itemListControl.EmptyList();

        var list = Director.userVariable.itemRetention;
        var keys = list.harvest.Keys;

        foreach (var key in keys) 
        {
            if (list.Get(key) == 0)
                continue;

            GameObject prefab = Instantiate(sellItemprefab);

            int pID = int.Parse(key.Substring(2));
            PlantItem plant = GameMgr.Plants.Get(pID);

            HarvestItem harvest = null;

            if (key[1] == 'F')
            {
                harvest = plant.fireHarvest;
            }
            else if (key[1] == 'G')
            {
                harvest = plant.grassHarvest;
            }
            else if (key[1] == 'W')
            {
                harvest = plant.waterHarvest;
            }

            Image image = prefab.transform.GetChild(0).GetComponent<Image>();
            image.sprite = harvest.image;

            prefab.name = harvest.id;
            prefab.transform.GetChild(1).GetComponent<Text>().text = harvest.itemName;
            prefab.transform.GetChild(2).GetComponent<Text>().text = $"가격 : {plant.harvestCost}G";
            prefab.GetComponent<Button>().onClick.AddListener(() => sellItem.SetID(prefab.name));

            PopupBtn _popup = prefab.GetComponent<PopupBtn>();
            _popup.popup = sellPopup;
            _popup.back = back;

            prefab.transform.SetParent(itemContent.transform, false);
        }
    }

    public void ChargeSpell()
    {
        Director.userVariable.gold -= 5;
        Director.userVariable.spell = Director.maxSpell;
        UpdateUI();
    }
}
