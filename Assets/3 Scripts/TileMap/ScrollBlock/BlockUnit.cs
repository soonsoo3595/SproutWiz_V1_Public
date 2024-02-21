using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockUnit : MonoBehaviour
{
    public SpriteRenderer sprite;
    SpriteRenderer outLine;
    public bool isAtive;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();

        Transform child = transform.Find("OutLine");
        outLine = child.GetComponent<SpriteRenderer>();

        if (sprite.enabled)
        {
            outLine.enabled = false;
            isAtive = true;
        }
        else
        {
            outLine.enabled = true;
            isAtive = false;
        }
    }
    
    public bool GetState()
    {
        if (sprite.enabled)
        {
            isAtive = true;
        }
        else
        {
            isAtive = false;
        }

        return isAtive;
    }

    public void SetSize(bool enable)
    {
        if (sprite.enabled)
        {
            outLine.enabled = false;
        }
        else
        {
            outLine.enabled = enable;
        }
    }
}
