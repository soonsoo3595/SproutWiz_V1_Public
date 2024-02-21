using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyItem : MonoBehaviour
{
    PlantItem plant;
    int count = 1;

    int totalCost = 0;

    public string id;
    public Text resultTxt;
    public Text retentionTxt;

    public InputField countTxt;

    public Button buyBtn;
    public StoreMgr storeMgr;

    // Start is called before the first frame update
    void Start()
    {
        buyBtn.onClick.AddListener(Buy);
    }

    void OnEnable()
    {
        
    }

    void OnDisable()
    {
        Init();
    }

    public void Init()
    {
        UpdateText("1");
    }

    public void SetID(string id)
    {
        this.id = id;

        plant = GameMgr.Plants.Get(id);

        UpdateText(count.ToString());
    }

    public void UpdateText(string text)
    {
        count = int.Parse(text);
        int cost = 100;

        totalCost = cost * count;

        countTxt.text = count.ToString();
        retentionTxt.text = $"���� {Director.userVariable.itemRetention.Get(id)}�� ������";

        if(totalCost > Director.userVariable.gold)
        {
            resultTxt.text = "�ݾ��� �����մϴ�";
            buyBtn.interactable = false;
        }
        else
        {
            resultTxt.text = $"{count}���� �����մϴ�. �� " + string.Format("{0:N0}G", totalCost) + "�Դϴ�";
            buyBtn.interactable = true;
        }
    }

    public void Plus()
    {
        GameMgr.Instance.soundEffect.PlayOneShotSoundEffect("plus");

        count++;
        UpdateText(count.ToString());
    }

    public void Minus()
    {
        if (count <= 1)
            return;

        GameMgr.Instance.soundEffect.PlayOneShotSoundEffect("minus");

        count--;
        UpdateText(count.ToString());
    }

    public void Buy()
    {
        if(id[0] == 'M')
        {
            Director.userVariable.itemRetention.Push(id, count);

            Director.userVariable.gold -= totalCost;
            storeMgr.UpdateUI();

            Init();
            return;
        }

        if (UtilityTools.AddItemToInventory(plant.seed, count))
        {
            Director.userVariable.gold -= totalCost;
            storeMgr.UpdateUI();

            Init();
        }
        else
        {
            Debug.Log("�κ��丮�� �� �� ������ �� �����ϴ�");
            return;
        }
    }
}
