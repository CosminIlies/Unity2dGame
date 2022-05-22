using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EquipmentSlot : MonoBehaviour, IDropHandler, IPointerEnterHandler,IPointerExitHandler
{
    public Equipment item;
    public Image icon;
    public EquipmentType type;


    private void Start()
    {
        updateGui();
    }
    public void updateGui()
    {
        if (item != null)
        {
            icon.sprite = item.icon;
            icon.color = Color.white;
        }
        else
        {
            icon.sprite = null;
            icon.color = new Color(0, 0, 0, 0);
        }
        if (item != null)
        {
            icon.sprite = item.icon;
            icon.color = Color.white;
        }
        else
        {
            icon.sprite = null;
            icon.color = new Color(0, 0, 0, 0);
        }
    }

    public void OnDrop(PointerEventData eventData)
    {  
        if (eventData.pointerDrag != null)
        {
            InventorySlot slot = eventData.pointerDrag.GetComponent<InventorySlot>();
            if (slot != null)
            {
                InventorySystem.instance.EquipItem(slot, this);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (this.item != null) { 
            GameObject obj = HoverDetails.instance.gameObject;
            obj.GetComponent<CanvasGroup>().alpha = 1;
            obj.transform.position = eventData.position;
            HoverDetails.instance.setItem(this.item, 1);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (this.item != null)
            HoverDetails.instance.gameObject.GetComponent<CanvasGroup>().alpha = 0;
    }
}
