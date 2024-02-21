using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 일반적인 버튼 클릭 시 나오는 소리
// 특수한 소리는 각 스크립트에서 처리하지만 많이 사용되는 버튼 클릭은 이 스크립트를 사용하겠다

public class BtnSFX : MonoBehaviour
{
    Button btn;

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
        GameMgr.Instance.soundEffect.PlayOneShotSoundEffect("click");
    }
}
