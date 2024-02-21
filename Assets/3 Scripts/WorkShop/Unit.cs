using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WorkShop
{
    public class Unit : MonoBehaviour
    {
        Color enableColor;

        public void Toggle(bool enable)
        {
            Image image = GetComponent<Image>();

            if (enable)
            {
                image.color = enableColor;
            }
            else
            {
                image.color = Color.white;
            }
        }

        public void SetColor(Element element)
        {
            switch (element)
            {
                case Element.Fire:
                    enableColor = Color.red;
                    break;
                case Element.Grass:
                    enableColor = Color.green;
                    break;
                case Element.Water:
                    enableColor = Color.blue;
                    break;
                default:
                    enableColor = Color.white;
                    break;
            }
        }
    }
}
