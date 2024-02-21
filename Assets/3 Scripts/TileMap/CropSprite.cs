using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class CropSprite : MonoBehaviour
{
    Vector3 originScale = new Vector3();

    private void Awake()
    {
        originScale = transform.localScale;
    }

    public void PlayAni()
    {
        transform.localScale = originScale - new Vector3(0.02f, 0.01f, 0);
        transform.DOScale(transform.localScale + new Vector3(0.02f, 0.02f, 0), 0.1f);
    }
}
