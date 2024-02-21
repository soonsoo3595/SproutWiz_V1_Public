using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WorkShop
{
    public class ElementSelect : MonoBehaviour
    {
        [SerializeField] Button WaterButton;
        [SerializeField] Button FireButton;
        [SerializeField] Button GrassButton;

        private void Awake()
        {
            WaterButton.onClick.AddListener(() => SelectElement(Element.Water));
            FireButton.onClick.AddListener(() => SelectElement(Element.Fire));
            GrassButton.onClick.AddListener(() => SelectElement(Element.Grass));
        }

        private void OnEnable()
        {
            SelectElement(Element.Water);
        }

        private void SelectElement(Element element)
        {
            if(element == Element.Water)
            {
                WaterButton.image.color = Color.blue;
                FireButton.image.color = Color.gray;
                GrassButton.image.color = Color.gray;
            }
            if(element == Element.Fire)
            {
                WaterButton.image.color = Color.gray;
                FireButton.image.color = Color.red;
                GrassButton.image.color = Color.gray;
            }
            if(element == Element.Grass)
            {
                WaterButton.image.color = Color.gray;
                FireButton.image.color = Color.gray;
                GrassButton.image.color = Color.green;
            }

            // MakeScroll 에디터에서 활성화상태로 실행시 nullRef오류 발생
            Manager.instance.selectedElement = element;
        }
    }
}
