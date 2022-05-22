using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeleteItem : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null)
        {
            InventorySlot slotInventory = eventData.pointerDrag.GetComponent<InventorySlot>();
            EquipmentSlot slotEquipment = eventData.pointerDrag.GetComponent<EquipmentSlot>();
            if (slotEquipment != null)
            {
                InventorySystem.instance.Delete(slotEquipment);
            }
            else if (slotInventory != null)
            {
                InventorySystem.instance.Delete(slotInventory);
            }
        }
    }

}
