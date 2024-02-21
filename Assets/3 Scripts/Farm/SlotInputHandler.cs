using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// 인벤토리 슬롯 아이템 클릭 or 드래그 앤 드롭에 대한 처리

public class SlotInputHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    private ItemClickPopup popup;
    private Button btn;
    private Slot slot;
    private DragSlot dragSlot;
    private ScrollRect scrollRect;

    void Awake()
    {
        btn = GetComponent<Button>();
        slot = GetComponent<Slot>();
        popup = transform.parent.parent.GetComponent<ItemClickPopup>();
        scrollRect = transform.parent.parent.parent.parent.GetComponent<ScrollRect>();
    }

    void Start()
    {
        btn.onClick.AddListener(Click);
        dragSlot = DragSlot.instance;
    }

    public void Click()
    {
        if (slot.slotItem.item != null)
        {
            GameMgr.Instance.soundEffect.PlayOneShotSoundEffect("click");
            popup.SetPopup(slot.slotItem.item, transform.position);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        scrollRect.OnBeginDrag(eventData);

        btn.interactable = false;

        GameMgr.Instance.soundEffect.PlayOneShotSoundEffect("drag");

        ItemData item = slot.slotItem.item;

        if (item != null)
        {
            dragSlot.SetDragSlot(slot);

            if (item.itemType != ItemType.Harvest)
            {
                GridManager.instance.CreatPreView(item, slot);
            }

            dragSlot.transform.position = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        scrollRect.OnDrag(eventData);

        if (slot.slotItem.item != null)
        {
            Vector2 currentPos = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            dragSlot.transform.position = currentPos;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        scrollRect.OnEndDrag(eventData);

        GameMgr.Instance.soundEffect.StopSoundEffect();
        GameMgr.Instance.soundEffect.PlayOneShotSoundEffect("drop");

        GridManager.instance.DropScrollBlock();

        dragSlot.UnsetDragSlot();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (dragSlot.slot != null)
            slot.ChangeSlot();
    }

}
