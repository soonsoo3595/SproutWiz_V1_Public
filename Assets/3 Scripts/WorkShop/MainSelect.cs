using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WorkShop
{
    public class MainSelect : MonoBehaviour
    {
        [SerializeField] Button growthBotton;
        [SerializeField] Button specialBotton;
        [SerializeField] Button toolBotton;
        [SerializeField] Button decomposBotton;

        private void Awake()
        {
            growthBotton.onClick.AddListener(() => Growth());
            toolBotton.onClick.AddListener(() => Tool());
            decomposBotton.onClick.AddListener(() => Decompos());
        }

        private void Growth()
        {
            GameMgr.Instance.soundEffect.PlayOneShotSoundEffect("paper_l");

            UIController.instance.MoveToTearSelect();
        }

        private void Tool()
        {
            ToastMessage.instance.ShowToast("준비중 입니다!");
        }

        private void Decompos()
        {
            ToastMessage.instance.ShowToast("준비중 입니다!");
        }
    }
}

