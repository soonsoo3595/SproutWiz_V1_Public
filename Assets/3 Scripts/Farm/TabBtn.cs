using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabBtn : MonoBehaviour
{
    private Button btn;

    public ItemType itemType;

    void Awake()
    {
        btn = GetComponent<Button>();
    }

    void Start()
    {
        btn.onClick.AddListener(Click);
    }

    public void Click()
    {
        GameMgr.Instance.soundEffect.PlayOneShotSoundEffect("paper_s");
        TabMgr.instance.ClickBtn(itemType);
    }
}
