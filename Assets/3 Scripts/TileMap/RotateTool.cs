using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class RotateTool : MonoBehaviour
{
    [SerializeField] Button RotateLeftBotton;
    [SerializeField] Button ApplyBotton;
    [SerializeField] Button CancleBotton;

    Vector2 localPos;

    private void Start()
    {
        RotateLeftBotton.onClick.AddListener(() => Rotate(90));
        ApplyBotton.onClick.AddListener(Apply);
        CancleBotton.onClick.AddListener(Cancle);

        localPos = transform.localPosition;
        GridManager.instance.rotateTool = this;
    }

    private void Rotate(int degree)
    {
        GameMgr.Instance.soundEffect.PlayOneShotSoundEffect("rotate");

        GridManager.instance.RotatePreView(degree);
    }

    private void Apply()
    {
        GameMgr.Instance.soundEffect.PlayOneShotSoundEffect("check");

        GridManager.instance.ApplyPreView();
        gameObject.transform.DOLocalMoveY(localPos.y, 1f);
    }

    private void Cancle()
    {
        GameMgr.Instance.soundEffect.PlayOneShotSoundEffect("cancel");

        GridManager.instance.CancelPreView();
        gameObject.transform.DOLocalMoveY(localPos.y, 1f);
    }

    public void Raise()
    {
        gameObject.transform.DOLocalMoveY(localPos.y + 150f, 1f);
    }
}
