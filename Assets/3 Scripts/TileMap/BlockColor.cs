using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockColor : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    //ScrollBlock scrollBlock;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        //scrollBlock = GetComponentInParent<ScrollBlock>();
    }

    private void OnEnable()
    {
        /*
        if (scrollBlock.element == Element.Fire)
        {
            spriteRenderer.color = Color.red;
        }
        else if(scrollBlock.element == Element.Water)
        {
            spriteRenderer.color = Color.blue;
        }
        else if (scrollBlock.element == Element.Grass)
        {
            spriteRenderer.color = Color.green;
        }

        Color color = spriteRenderer.color;

        color.a = 0.3f;
        spriteRenderer.color = color;
        */
    }
}
