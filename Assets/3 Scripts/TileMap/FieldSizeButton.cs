using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FieldSizeButton : MonoBehaviour
{
    Button btn;

    void Awake()
    {
        btn = GetComponent<Button>();
    }

    void Start()
    {
        btn.onClick.AddListener(Click);
    }

    public void Click()
    {
        GridManager.instance.ExpandGrid();
    }
}
