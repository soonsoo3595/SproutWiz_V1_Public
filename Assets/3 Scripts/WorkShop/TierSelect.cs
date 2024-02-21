using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

namespace WorkShop
{
    public class TierSelect : MonoBehaviour
    {
        [SerializeField] Button tier1Button;
        [SerializeField] Button tier2Button;
        [SerializeField] Button tier3Button;
        [SerializeField] Button closeButton;

        [SerializeField] TextMeshProUGUI message;

        Vector3 originScale = new Vector3();

        private void Awake()
        {
            tier1Button.onClick.AddListener(() => SelectTear(1));
            tier2Button.onClick.AddListener(() => SelectTear(2));
            tier3Button.onClick.AddListener(() => SelectTear(3));
            closeButton.onClick.AddListener(() => Close());

            originScale = transform.localScale;
        }

        private void OnEnable()
        {
            message.enabled = false;

            transform.DOScale(transform.localScale + new Vector3(0.05f, 0.05f, 0), 0.1f);
        }

        private void OnDisable()
        {
            transform.localScale = originScale;
        }

        private void SelectTear(int tier)
        {
            int userCircle = Manager.instance.GetUserCircle();

            if(tier == 2)
            {
                if(userCircle < 3)
                {
                    message.text = $"3단계 마력서클 달성시 해금됩니다";
                    message.enabled = true;
                    // TODO: 일정 시간 후 메세지 지우기?

                    return;
                }
            }
            else if(tier == 3)
            {
                if (userCircle < 5)
                {
                    message.text = $"5단계 마력서클 달성시 해금됩니다";
                    message.enabled = true;
                    return;
                }
            }

            Manager.instance.selectedTier = tier;
            UIController.instance.MoveToMakeScroll();
        }

        private void Close()
        {
            UIController.instance.MoveToMainSelect();
        }
    }
}
    
