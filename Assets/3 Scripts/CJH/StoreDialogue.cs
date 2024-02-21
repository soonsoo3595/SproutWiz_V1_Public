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
        ownerTxt.text = "자네 왔나";
    }

    public void Init()
    {
        ownerTxt.text = "천천히 둘러봐";

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
            $"{product.Name}을 구매하겠나? 가격은 개당 {product.price}골드네!", $"{product.Name}을 몇 개 구매할 생각인가?", $"{count}개를 구매하면 총 {totalCost}G 일세!", $"돈은 잘 받았네. 여기 {product.Name}을 받게나", $"자네, 골드가 부족한 것 같은데?"     
        };

        UpdateBuyDialogue(count.ToString());

    }

    public void AllocateDailyDialogue()
    {
        continueBtn.gameObject.SetActive(true);

        dialogue = new string[]
        {
            "자네 혹시 수확물을 어디서 팔아야 하는 지 찾고 있나?", "아카데미 공사 때문에 임시로 자네의 수확물을 여기서 사들이고 있는데 관심 있나?", "싱싱한 것으로 부탁한다네"
        };
    }

    public void AllocateBuySpellDialogue()
    {
        continueBtn.gameObject.SetActive(true);
        cancelBtn.gameObject.SetActive(true);

        dialogue = new string[]
        {
            "신규 오픈 기념으로 마력 회복 포션을 싼 값에 팔고 있다네", "한 번 마시면 최대 마력까지 회복시켜주지 구매하겠나??", "마력이 끝까지 채워졌다네"
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
                    Debug.Log("판매 시작");
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

        dialogue[2] = $"{count}개를 구매하면 총 {totalCost}G 일세!";
    }

    public bool CheckGold()
    {
        if (Director.userVariable.gold >= totalCost)
            return true;
        else
            return false;
    }
}
