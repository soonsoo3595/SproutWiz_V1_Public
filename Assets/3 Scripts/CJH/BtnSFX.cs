using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// �Ϲ����� ��ư Ŭ�� �� ������ �Ҹ�
// Ư���� �Ҹ��� �� ��ũ��Ʈ���� ó�������� ���� ���Ǵ� ��ư Ŭ���� �� ��ũ��Ʈ�� ����ϰڴ�

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
