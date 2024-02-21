using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WorkShop
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] GameObject mainSelect;
        [SerializeField] GameObject TearSelct;
        [SerializeField] GameObject MakeScroll;
        [SerializeField] GameObject Result;

        public static UIController instance;

        void Start()
        {
            if(instance == null) instance = this;

            MoveToMainSelect();
        }

        public void MoveToMainSelect()
        {
            mainSelect.SetActive(true);
            TearSelct.SetActive(false);
            MakeScroll.SetActive(false);
            Result.SetActive(false);
        }

        public void MoveToTearSelect()
        {
            mainSelect.SetActive(false);
            TearSelct.SetActive(true);
            MakeScroll.SetActive(false);
            Result.SetActive(false);
        }

        public void MoveToMakeScroll()
        {
            mainSelect.SetActive(false);
            TearSelct.SetActive(false);
            MakeScroll.SetActive(true);
            Result.SetActive(false);
        }

        public void MoveToResult()
        {
            mainSelect.SetActive(false);
            TearSelct.SetActive(false);
            MakeScroll.SetActive(false);
            Result.SetActive(true);
        }
    }
}
