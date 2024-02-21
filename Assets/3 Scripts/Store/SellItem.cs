using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellItem : MonoBehaviour
{
    PlantItem plant;
    public int count = 1;

    int totalCost = 0;

    public string id;
    public Text resultTxt;
    public Text retentionTxt;

    public InputField countTxt;

    public Button sellBtn;
    public StoreMgr storeMgr;

    // Start is called before the first frame update
    void Start()
    {
        sellBtn.onClick.AddListener(Sell);
    }

    void OnEnable()
    {
    }

    void OnDisable()
    {
        storeMgr.itemListControl.EmptyList();
    }

    public void Init()
    {
        UpdateText("1");
    }

    public void SetID(string id)
    {
        this.id = id;

        if (id[0] == 'H')
        {
            int pID = int.Parse(id.Substring(2));
            plant = GameMgr.Plants.Get(pID);
        }
        else
            plant = GameMgr.Plants.Get(id);

        Init();
        UpdateText(count.ToString());
    }

    public void UpdateText(string text)
    {
        count = int.Parse(text);
        int cost = plant.harvestCost;

        totalCost = cost * count;

        countTxt.text = count.ToString();

        int retention = Director.userVariable.itemRetention.Get(id);
        retentionTxt.text = $"현재 {retention}개 보유중";

        if (count > retention)
        {
            resultTxt.text = "더 팔 수 없습니다";
            sellBtn.interactable = false;
        }
        else
        {
            resultTxt.text = $"{count}개를 판매합니다. 총 " + string.Format("{0:N0}G", totalCost) + "입니다";
            sellBtn.interactable = true;
        }
    }

    public void Plus()
    {
        count++;
        UpdateText(count.ToString());
    }

    public void Minus()
    {
        if (count <= 1)
            return;

        count--;
        UpdateText(count.ToString());
    }

    public void Sell()
    {
        Director.userVariable.gold += totalCost;
        storeMgr.UpdateUI();

        UtilityTools.ItemCountChange(id, -count);
        /*
        if (UtilityTools.AddItemToInventory(plant.seed, count))
        {
            Director.userVariable.gold -= totalCost;
            storeMgr.UpdateGold();

            Init();
        }
        else
        {
            Debug.Log("인벤토리가 꽉 차 구매할 수 없습니다");
            return;
        }
        */
    }
}
