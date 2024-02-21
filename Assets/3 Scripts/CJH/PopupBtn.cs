using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupBtn : MonoBehaviour
{
    [SerializeField] ButtonType type;

    public GameObject popup;
    public GameObject back;

    Button btn;

    void Awake()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(Click);
    }

    public void Click()
    {
        if (type == ButtonType.Popup)
        {
            popup.SetActive(true);
            back.SetActive(true);

            BackMgr.instance.Push(this);
        }
        else if (type == ButtonType.Back)
        {
            back.SetActive(false);
            popup.SetActive(false);

            if (BackMgr.instance.st.Count > 0)
            {
                BackMgr.instance.st.Pop();
            }
        }
    }

    public void BackClick()
    {
        popup.SetActive(false);
    }
}
