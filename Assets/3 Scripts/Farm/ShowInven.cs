using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

// 현재 안쓰는 스크립트

public class ShowInven : MonoBehaviour
{
    private Button btn;
    private bool isHide = false;
    private Vector2 startPosition;

    public GameObject inventory;

    void Awake()
    {
        btn = GetComponent<Button>();
    }

    void Start()
    {
        btn.onClick.AddListener(ClickBtn);
        startPosition = inventory.transform.localPosition;
    }

    public void ClickBtn()
    {
        if(isHide)
        {
            inventory.transform.DOLocalMoveX(startPosition.x, 1f);
            isHide = false;
        }
        else
        {
            inventory.transform.DOLocalMoveX(startPosition.x - 600, 1f);
            isHide = true;
        }
    }
}
