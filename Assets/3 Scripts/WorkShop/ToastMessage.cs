using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class ToastMessage : MonoBehaviour
{
    public TextMeshProUGUI toastText;
    public float displayDuration = 2.0f;

    private bool isDisplaying = false;
    Vector3 originScale = new Vector3();

    public static ToastMessage instance;

    private void Awake()
    {
        instance = this;
        gameObject.SetActive(false);

        originScale = transform.localScale;
    }

    public void ShowToast(string message)
    {
        ShowToast(message, 2.0f);
    }

    public void ShowToast(string message, float timer)
    {
        if (!isDisplaying)
        {
            gameObject.SetActive(true);
            toastText.text = message;
            isDisplaying = true;

            transform.DOScale(transform.localScale + new Vector3(0.05f, 0.05f, 0), 0.1f);
            StartCoroutine(ShowToastCoroutine());
        }
    }

    private IEnumerator ShowToastCoroutine()
    {
        yield return new WaitForSeconds(displayDuration);

        transform.localScale = originScale;
        gameObject.SetActive(false);
        isDisplaying = false;
    }
}
