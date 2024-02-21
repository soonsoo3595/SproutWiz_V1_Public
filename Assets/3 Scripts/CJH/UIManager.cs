using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public Action updateTopUI;

    TopUI topUI;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
    }


}
