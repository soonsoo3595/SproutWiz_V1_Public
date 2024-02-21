using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TopUI : MonoBehaviour
{
    [HideInInspector] public GameObject backBtn;
    [HideInInspector] public GameObject layout;
    [HideInInspector] public GameObject gold;
    [HideInInspector] public GameObject spell;
    [HideInInspector] public GameObject proficiency;

    [HideInInspector] public Text goldTxt;
    [HideInInspector] public Text spellTxt;

    void Awake()
    {
        backBtn = transform.GetChild(0).gameObject;
        layout = transform.GetChild(2).gameObject;
        gold = layout.transform.GetChild(0).gameObject;
        spell = layout.transform.GetChild(1).gameObject;
        proficiency = layout.transform.GetChild(2).gameObject;

        goldTxt = gold.transform.GetChild(0).GetComponent<Text>();
        spellTxt = spell.transform.GetChild(0).GetComponent<Text>();
    }

    void Start()
    {
        string curScene = GameMgr.Instance.curScene;
        if (curScene == "Farm")
        {
            gameObject.SetActive(true);

            backBtn.SetActive(false);

            gold.SetActive(false);
            spell.SetActive(false);
            proficiency.SetActive(true);

        }
        else if (curScene == "Town")
        {
            gameObject.SetActive(false);
        }
        else if(curScene == "Store")
        {
            gameObject.SetActive(true);

            gold.SetActive(true);
            spell.SetActive(true);
            proficiency.SetActive(false);
        }
        else if(curScene == "Workshop")
        {
            gameObject.SetActive(true);

            gold.SetActive(false);
            spell.SetActive(true);
            proficiency.SetActive(false);
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void UpdateUI()
    {
        UpdateGoldText();
        UpdateSpellText();
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UpdateUI();
    }

    public void UpdateGoldText()
    {
        goldTxt.text = string.Format("{0:N0}", Director.userVariable.gold);
    }

    public void UpdateSpellText()
    {
        spellTxt.text = string.Format("{0:N0}", Director.userVariable.spell);
    }
}
