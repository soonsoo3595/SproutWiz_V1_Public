using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.RuleTile.TilingRuleOutput;

namespace WorkShop
{
    public class Result : MonoBehaviour
    {
        [SerializeField] Button closeButton;
        ResultPanel[] panels;

        Vector3 originScale = new Vector3();
        Vector3 aniStartScale;

        private void Awake()
        {
            panels = GetComponentsInChildren<ResultPanel>();
            closeButton.onClick.AddListener(() => Close());

            originScale = transform.localScale;
            aniStartScale = originScale - new Vector3(0.5f, 0.5f, 0f);
        }

        private void OnEnable()
        {
            var scrolls = Manager.instance.craftingScrollData;
            int enNum = scrolls.Count;

            int count = 0;
            foreach (ResultPanel panel in panels)
            {
                if (count < enNum)
                {
                    panel.gameObject.SetActive(true);
                    panel.SetScrollData(scrolls[count]);
                }
                else
                {
                    panel.gameObject.SetActive(false);
                }
                count++;
            }

            scrolls.Clear();

            transform.localScale = aniStartScale;
            transform.DOScale(originScale, 1.0f);
        }

        private void OnDisable()
        {
            transform.localScale = originScale;
        }

        private void Close()
        {
            UIController.instance.MoveToMainSelect();
        }
    }
}
