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
        retentionTxt.text = $"���� {retention}�� ������";

        if (count > retention)
        {
            resultTxt.text = "�� �� �� �����ϴ�";
            sellBtn.interactable = false;
        }
        else
        {
            resultTxt.text = $"{count}���� �Ǹ��մϴ�. �� " + string.Format("{0:N0}G", totalCost) + "�Դϴ�";
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
            Debug.Log("�κ��丮�� �� �� ������ �� �����ϴ�");
            return;
        }
        */
    }
}
