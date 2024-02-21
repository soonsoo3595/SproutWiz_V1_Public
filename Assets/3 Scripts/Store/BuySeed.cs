using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuySeed : MonoBehaviour
{
    int count = 1;

    int totalCost = 0;

    public string id;
    public Text resultTxt;

    public InputField countTxt;

    public Button buyBtn;
    public StoreMgr storeMgr;

    void Start()
    {
        buyBtn.onClick.AddListener(Buy);
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

        UpdateText(count.ToString());
    }

    public void UpdateText(string text)
    {
        count = int.Parse(text);
        int cost = 300;

        totalCost = cost * count;

        countTxt.text = count.ToString();

        if (totalCost > Director.userVariable.gold)
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

    public void Buy()
    {

        for(int i = 0; i < count; i++)
        {
            PlantItem plant = GameMgr.Plants.Get(RandomSeed());

            if (UtilityTools.AddItemToInventory(plant.seed, 1))
            {
                Director.userVariable.gold -= 300;
                storeMgr.UpdateUI();
                
            }
            else
            {
                Debug.Log("�κ��丮�� �� �� ������ �� �����ϴ�");
                return;
            }

            Debug.Log(plant.id);
        }

        Init();

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

    public int RandomSeed()
    {
        int rand = 0;
        
        rand = Random.Range(0, 6);

        return rand;
    }
}
