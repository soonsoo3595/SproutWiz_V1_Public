using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragSlot : MonoBehaviour
{
    private Image slotImage;

    public static DragSlot instance;
    public ScrollRect scrollRect;
    [HideInInspector] public Slot slot;

    void Awake()
    {
        instance = this;

        slotImage = GetComponent<Image>();
    }
    private void SetColor(float alpha)
    {
        Color color = slotImage.color;
        color.a = alpha;
        slotImage.color = color;
    }

    public void SetDragSlot(Slot slot)
    {
        this.slot = slot;
        slotImage.sprite = slot.image.sprite;
        SetColor(0.6f);
        scrollRect.enabled = false;
    }

    public void UnsetDragSlot()
    {
        slot = null;
        SetColor(0);
        scrollRect.enabled = true;
    }

}
