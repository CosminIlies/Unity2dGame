using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Item item;
    public int count;
    public Image icon;
    public Text countText;


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
            if (count > 1)
            {
                countText.text = count.ToString();
            }
            else
            {
                countText.text = "";
            }
            
        }
        else
        {
            icon.sprite = null;
            icon.color = new Color(0,0,0,0);
            countText.text = "";
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null)
        {
            InventorySlot slotInventory = eventData.pointerDrag.GetComponent<InventorySlot>();
            EquipmentSlot slotEquipment = eventData.pointerDrag.GetComponent<EquipmentSlot>();
            if (slotEquipment != null)
            {
                if (slotEquipment.item != null)
                    InventorySystem.instance.Unequip(this, slotEquipment);
            }
            else if (slotInventory != null)
            {
                if (slotInventory.item != null)
                    InventorySystem.instance.swapItems(slotInventory, this);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (this.item != null)
        {
            GameObject obj = HoverDetails.instance.gameObject;
            obj.GetComponent<CanvasGroup>().alpha = 1;
            obj.transform.position = eventData.position;
            HoverDetails.instance.setItem(this.item,count);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (this.item != null)
            HoverDetails.instance.gameObject.GetComponent<CanvasGroup>().alpha = 0;
    }
}

