using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FarmInputMgr : MonoBehaviour
{
    private bool isControlling = false;

    public GameObject inputControl;

    public static FarmInputMgr instance;

    public bool IsControlling
    {
        get { return isControlling; }
        set
        {
            if(isControlling != value)
            {
                isControlling = value;
                
                if(isControlling)
                {
                    inputControl.SetActive(true);
                }
                else
                {
                    inputControl.SetActive(false);
                }
            }
        }
    }

    void Awake()
    {
        instance = this;
    }

}
