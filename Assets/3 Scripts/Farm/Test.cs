using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    public ItemData itemData;
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
        InventoryMgr.instance.AcquireItem(itemData);
    }    

    
}
