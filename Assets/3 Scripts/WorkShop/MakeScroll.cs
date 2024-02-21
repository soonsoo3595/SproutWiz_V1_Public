using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

namespace WorkShop
{
    public class MakeScroll : MonoBehaviour
    {
        [SerializeField] Button closeButton;
        [SerializeField] Button pluseButton;
        [SerializeField] Button minusButton;
        [SerializeField] Button MakeButton;
        [SerializeField] GameObject spell;
        [SerializeField] GameObject[] materials;
        [SerializeField] TextMeshProUGUI tierText;
        [SerializeField] TextMeshProUGUI skillLevelText;
        [SerializeField] TMP_InputField inputField;
        [SerializeField] float Level_1_percent, Level_2_percent;

        [SerializeField] GameObject ConfirmPanel;
        [SerializeField] Button ConfirmButton;
        [SerializeField] Button CancelButton;
        [SerializeField] TextMeshProUGUI MakeCountText;

        [SerializeField] GameObject tierImage;

        int tier;
        int multiply;
        bool enoughMaterial;
        TestData.CraftingItem craftingItem;

        TestData.CraftingItmeDB DB;

        Vector3 originScale = new Vector3();
        Vector3 ConfirmOriginScale = new Vector3();

        private void Awake()
        {
            closeButton.onClick.AddListener(() => Close());
            pluseButton.onClick.AddListener(() => PlusOrMinus(true));
            minusButton.onClick.AddListener(() => PlusOrMinus(false));
            MakeButton.onClick.AddListener(() => MakeItemButton(enoughMaterial));

            ConfirmButton.onClick.AddListener(() => Confirm(true));
            CancelButton.onClick.AddListener(() => Confirm(false));

            Level_2_percent += Level_1_percent;

            originScale = transform.localScale;
            ConfirmOriginScale = ConfirmPanel.transform.localScale;
        }

        private void OnEnable()
        {
            transform.DOScale(transform.localScale + new Vector3(0.05f, 0.05f, 0), 0.1f);

            Initialize();
            TearSelect();
            PrintUserLevel();
            MaterialsImageAndCount();

            tierImage.GetComponent<Image>().sprite = Manager.instance.GetTierImage(tier);
        }

        private void OnDisable()
        {
            transform.localScale = originScale;
        }

        private void Initialize()
        {
            DB = Manager.instance.craftingItmeDB;

            craftingItem = new TestData.CraftingItem();
            inputField.text = "1";
            multiply = 1;

            ConfirmPanel.SetActive(false);
            ConfirmPanel.transform.localScale = ConfirmOriginScale;
        }

        private void MakeItemButton(bool enough)
        {
            if(enough)
            {
                if (UtilityTools.GetEmptySlotCount(ItemType.Scroll) < multiply)
                {
                    ToastMessage.instance.ShowToast("스크롤 인벤토리가 부족합니다");
                    return;
                }

                PrintConfirmPanel();
            }
            else
            {
                ToastMessage.instance.ShowToast("재료가 부족합니다");
            }
        }

        private void PrintConfirmPanel()
        {
            ConfirmPanel.SetActive(true);
            ConfirmPanel.transform.DOScale(ConfirmPanel.transform.localScale + new Vector3(0.05f, 0.05f, 0), 0.1f);

            MakeCountText.text = $"{tier}티어 성장 스크롤 X{multiply}";
        }

        private void Confirm(bool IsConfirmed)
        {
            if (IsConfirmed)
            {
                GameMgr.Instance.soundEffect.PlayOneShotSoundEffect("makeScroll");

                RandomMake();
                useMaterial();

                Manager.instance.productionCount = multiply;
                UIController.instance.MoveToResult();
            }
            else
            {
                ConfirmPanel.SetActive(false);
                ConfirmPanel.transform.localScale = ConfirmOriginScale;
            }
        }

        private void useMaterial()
        {
            foreach (var key in craftingItem.requiredMaterials)
            {
                int totalRequired = key.Value * multiply;

                Director.userVariable.itemRetention.Push(key.Key, -totalRequired);
            }

            Director.userVariable.spell -= craftingItem.requiredSpell * multiply;
            TopUI topui = FindObjectOfType<TopUI>();
            topui.UpdateSpellText();
        }

        private void RandomMake()
        {
            SetRandomNum();
        }

        private void SetRandomNum()
        {
            int[][] LevelAndNum = new int[3][];
            LevelAndNum[0] = new int[9];
            LevelAndNum[1] = new int[9];
            LevelAndNum[2] = new int[9];

            for (int i = 0; i < multiply; i++)
            {
                int level = GetRandomLevel();
                int numLength = Manager.instance.GetPrefebLength(tier, level);
                int num = Random.Range(0, numLength - 1);

                LevelAndNum[level-1][num]++;
            }

            for (int level = 0; level < 3; level++)
            {
                for(int num = 0; num < 9; num++)
                {
                    if(LevelAndNum[level][num] != 0)
                    {
                        TestData.CraftingScrollData temp = new TestData.CraftingScrollData();
                        temp.tier = tier;
                        temp.level = level+1;
                        temp.number = num;
                        temp.count = LevelAndNum[level][num];
                        temp.element = GetRandomElement();
                        Manager.instance.craftingScrollData.Add(temp);

                        SentToInventory(temp);
                    }
                }
            }     
        }

        private void SentToInventory(TestData.CraftingScrollData scroll)
        {
            ScrollItem scrollItem = ScriptableObject.CreateInstance<ScrollItem>();
            scrollItem.element = scroll.element;
            scrollItem.itemType = ItemType.Scroll;
            scrollItem.image = Manager.instance.GetScrollImage(scroll.element, scroll.tier);
            scrollItem.tier = scroll.tier;
            scrollItem.grade = scroll.level;
            scrollItem.block = Manager.instance.GetBlockObject(scroll.tier, scroll.level, scroll.number);

            List<bool> test = Manager.instance.GetScroll(tier, scroll.level, scroll.number);

            scrollItem.shape = test;

            for (int i = 0; i < scroll.count; i++)
            {
                UtilityTools.AddItemToInventory(scrollItem, 1);
            }
        }

        private int GetRandomLevel()
        {
            float randomValue = Random.value; 

            if (randomValue < Level_1_percent) 
                return 1;
            else if (randomValue < Level_2_percent) 
                return 2;
            else
                return 3;
        }

        private Element GetRandomElement()
        {
            float randomValue = Random.value;

            if (randomValue < 0.33)
                return Element.Fire;
            else if (randomValue < 0.66)
                return Element.Grass;
            else
                return Element.Water;
        }

        private void MaterialsImageAndCount()
        {
            if (tier < 1) tier = 1;
            enoughMaterial = true;

            int totalSpellRequired = DB.items[tier - 1].requiredSpell * multiply;
            TextMeshProUGUI spellText = spell.GetComponentInChildren<TextMeshProUGUI>();
            spellText.color = Color.white;

            // 스펠 필요량 처리
            spellText.text = $"{Director.userVariable.spell} / {totalSpellRequired} ";

            if(totalSpellRequired > Director.userVariable.spell)
            {
                spellText.color = Color.red;
                enoughMaterial = false;
            }

            int count = 0;
            List<int> required = new List<int>();

            foreach (var key in DB.items[tier - 1].requiredMaterials)
            {
                required.Add(key.Value);

                int totalRequired = key.Value * multiply;
                int userHas = Director.userVariable.itemRetention.Get(key.Key);

                // 스크립터블로 만들어서 MaterialItem 받아오는 코드
                MaterialItem material = GameMgr.Materials.Get(key.Key);
                TextMeshProUGUI Text = materials[count].GetComponentInChildren<TextMeshProUGUI>();

                materials[count].GetComponent<Image>().sprite = material.image;
                Text.text = $"{totalRequired} / {userHas}";
                Text.color = Color.white;

                if (totalRequired > userHas)
                {
                    //TODO: 보유량보다 초과하거나 마이너스 일경우 처리
                    enoughMaterial = false;
                    Text.color = Color.red;
                }

                count++;
            }

            craftingItem = DB.items[tier - 1];
        }

        private void TearSelect()
        {
            tier = Manager.instance.selectedTier;

            materials[0].SetActive(true);
            materials[1].SetActive(false);
            materials[2].SetActive(false);

            tierText.text = $"{tier}티어";
        }

        private void PrintUserLevel()
        {
            // 서클레벨이랑 숙련도랑 다른 개념인듯 추후 수정 필요.
            int userCircle = Manager.instance.GetUserCircle();

            skillLevelText.text = $"숙련도 : {userCircle}";
        }

        public void ChangeValue()
        {
            int inputNum;
            int.TryParse(inputField.text, out inputNum);

            if (inputNum > 20)
            {
                inputNum = 20;
                inputField.text = inputNum.ToString();
                ToastMessage.instance.ShowToast("더 이상 추가할 수 없습니다.");
            }

            multiply = inputNum;
            MaterialsImageAndCount();
        }

        private void PlusOrMinus(bool isPlus)
        {
            int inputNum;
            int.TryParse(inputField.text, out inputNum);

            if (isPlus)
            {
                if (inputNum < 20)
                    inputNum++;
                else
                    ToastMessage.instance.ShowToast("더 이상 추가할 수 없습니다.");

                GameMgr.Instance.soundEffect.PlayOneShotSoundEffect("plus");
            }
            else
            {
                if (inputNum > 1)
                    inputNum--;

                GameMgr.Instance.soundEffect.PlayOneShotSoundEffect("minus");
            }

            multiply = inputNum;
            MaterialsImageAndCount();
            inputField.text = inputNum.ToString();
        }

        private void Close()
        {
            UIController.instance.MoveToTearSelect();
        }
    }
}
