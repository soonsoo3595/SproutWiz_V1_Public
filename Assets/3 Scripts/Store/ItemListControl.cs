using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemListControl : MonoBehaviour
{
    public GameObject content;

    void OnEnable()
    {
        EmptyList();
    }

    public void EmptyList()
    {
        int childCount = content.transform.childCount;

        for(int i = 0; i < childCount; i++) 
        {
            Destroy(content.transform.GetChild(i).gameObject);
        }

    }
}
