using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StoreDialogue : MonoBehaviour
{
    bool isWaitingForInput = false;
    bool userResponse = false;

    int count = 1;
    int totalCost = 0;

    public StoreMgr storeMgr;
    public Text ownerTxt;
    string[] dialogue;

    public InputField countTxt;
    public Button plusBtn;
    public Button minusBtn;
    public Button continueBtn;
    public Button cancelBtn;
    public Button ownerBtn;

    void Awake()
    {
        continueBtn.onClick.AddListener(OnYesButtonClicked);
        cancelBtn.onClick.AddListener(OnNoButtonClicked);
        ownerBtn.onClick.AddListener(ClickOwner);
    }

    void Start()
    {
        ownerTxt.text = "�ڳ� �Գ�";
    }

    public void Init()
    {
        ownerTxt.text = "õõ�� �ѷ���";

        count = 1;
        countTxt.text = count.ToString();

        countTxt.gameObject.SetActive(false);
        continueBtn.gameObject.SetActive(false);
        cancelBtn.gameObject.SetActive(false);

        storeMgr.SetToolTip(false);
    }


    public void BuyProccess()
    {
        if(storeMgr.product.id == "M05")
        {
            AllocateBuySpellDialogue();
            StartCoroutine(ShowBuySpellDialogue());
        }
        else
        {
            AllocateBuyDialogue();
            StartCoroutine(ShowBuyDialogue());
        }
        
    }

    public void ClickOwner()
    {
        storeMgr.SetToolTip(false);

        AllocateDailyDialogue();
        StartCoroutine(ShowDailyDialogue());
    }

    public void AllocateBuyDialogue()
    {
        ProductData product = storeMgr.product;

        continueBtn.gameObject.SetActive(true);
        cancelBtn.gameObject.SetActive(true);

        dialogue = new string[]
        {
            $"{product.Name}�� �����ϰڳ�? ������ ���� {product.price}����!", $"{product.Name}�� �� �� ������ �����ΰ�?", $"{count}���� �����ϸ� �� {totalCost}G �ϼ�!", $"���� �� �޾ҳ�. ���� {product.Name}�� �ްԳ�", $"�ڳ�, ��尡 ������ �� ������?"     
        };

        UpdateBuyDialogue(count.ToString());

    }

    public void AllocateDailyDialogue()
    {
        continueBtn.gameObject.SetActive(true);

        dialogue = new string[]
        {
            "�ڳ� Ȥ�� ��Ȯ���� ��� �Ⱦƾ� �ϴ� �� ã�� �ֳ�?", "��ī���� ���� ������ �ӽ÷� �ڳ��� ��Ȯ���� ���⼭ ����̰� �ִµ� ���� �ֳ�?", "�̽��� ������ ��Ź�Ѵٳ�"
        };
    }

    public void AllocateBuySpellDialogue()
    {
        continueBtn.gameObject.SetActive(true);
        cancelBtn.gameObject.SetActive(true);

        dialogue = new string[]
        {
            "�ű� ���� ������� ���� ȸ�� ������ �� ���� �Ȱ� �ִٳ�", "�� �� ���ø� �ִ� ���±��� ȸ���������� �����ϰڳ�??", "������ ������ ä�����ٳ�"
        };
    }

    private IEnumerator ShowBuyDialogue()
    {
        for(int i = 0; i < dialogue.Length; i++)
        {
            ownerTxt.text = dialogue[i];

            if(i == 1)
                countTxt.gameObject.SetActive(true);
            else
                countTxt.gameObject.SetActive(false);

            isWaitingForInput = true;
            while (isWaitingForInput)
            {
                yield return null;
            }

            if(userResponse)
            {
                if(i == 2)
                {
                    if(CheckGold())
                    {
                        storeMgr.Purchase(count);
                        countTxt.gameObject.SetActive(false);
                    }
                    else
                    {
                        i++;
                    }
                        cancelBtn.gameObject.SetActive(false);
                }

                if (i == 3)
                    break;
            }
            else
            {
                break;
            }
        }

        Init();
    }

    private IEnumerator ShowDailyDialogue()
    {
        for (int i = 0; i < dialogue.Length; i++)
        {
            ownerTxt.text = dialogue[i];

            if (i == 0)
                cancelBtn.gameObject.SetActive(false);
            else
                cancelBtn.gameObject.SetActive(true);

            isWaitingForInput = true;
            while (isWaitingForInput)
            {
                yield return null;
            }

            if (userResponse)
            {
                if(i == 1)
                {
                    Debug.Log("�Ǹ� ����");
                    storeMgr.OpenItemList(1);
                }
            }
            else
            {
                Init();
                storeMgr.CloseItemList();

                yield break;
            }
        }

        storeMgr.CloseItemList();
        Init();
    }

    private IEnumerator ShowBuySpellDialogue()
    {
        for (int i = 0; i < dialogue.Length; i++)
        {
            ownerTxt.text = dialogue[i];

            if (i == 0 || i == 2)
                cancelBtn.gameObject.SetActive(false);
            else
                cancelBtn.gameObject.SetActive(true);

            isWaitingForInput = true;
            while (isWaitingForInput)
            {
                yield return null;
            }

            if (userResponse)
            {
                if (i == 1)
                {
                    if(Director.userVariable.gold >= 5)
                    {
                        storeMgr.ChargeSpell();
                    }
                }
            }
            else
            {
                Init();
                yield break;
            }
        }

        Init();
    }

    public void ShowYesNoBtn(bool flag)
    {
        if(flag)
        {
            continueBtn.gameObject.SetActive(true);
            cancelBtn.gameObject.SetActive(true);
        }
        else
        {
            continueBtn.gameObject.SetActive(false);
            cancelBtn.gameObject.SetActive(false);
        }
    }

    public void ShowCountTxt(bool flag)
    {
        if (flag)
        {
            countTxt.gameObject.SetActive(true);
        }
        else
        {
            countTxt.gameObject.SetActive(false);
        }
    }

    public void OnYesButtonClicked()
    {
        isWaitingForInput = false;
        userResponse = true;
    }

    public void OnNoButtonClicked()
    {
        isWaitingForInput = false;
        userResponse = false;
    }

    public void Plus()
    {
        count++;
        GameMgr.Instance.soundEffect.PlayOneShotSoundEffect("plus");

        UpdateBuyDialogue(count.ToString());
    }

    public void Minus()
    {
        if (count <= 1)
            return;

        GameMgr.Instance.soundEffect.PlayOneShotSoundEffect("minus");

        count--;
        UpdateBuyDialogue(count.ToString());
    }

    public void UpdateBuyDialogue(string text)
    {
        count = int.Parse(text);
        int price = storeMgr.product.price;

        totalCost = price * count;

        countTxt.text = count.ToString();

        dialogue[2] = $"{count}���� �����ϸ� �� {totalCost}G �ϼ�!";
    }

    public bool CheckGold()
    {
        if (Director.userVariable.gold >= totalCost)
            return true;
        else
            return false;
    }
}
