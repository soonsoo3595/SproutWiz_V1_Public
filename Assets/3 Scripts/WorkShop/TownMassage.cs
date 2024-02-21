using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TownMassage : MonoBehaviour
{
    [SerializeField] Button academy;

    private void Awake()
    {
        academy.onClick.AddListener(() => ClickAcademy());
    }

    private void ClickAcademy()
    {
        ToastMessage.instance.ShowToast("공사중 입니다!");
    }
}
