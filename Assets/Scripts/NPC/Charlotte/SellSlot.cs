using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SellSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null)
        {
            InventorySlot slotInventory = eventData.pointerDrag.GetComponent<InventorySlot>();
            if (slotInventory != null)
            {
                if(Input.GetKey(KeyCode.LeftShift)){
                    PlayerStats.instance.money += slotInventory.item.sellPrice * slotInventory.count;
                    InventorySystem.instance.Delete(slotInventory);
                }else{
                    PlayerStats.instance.money += slotInventory.item.sellPrice;
                    InventorySystem.instance.RemoveOne(slotInventory);
                }
                PlayerStats.instance.updateUI();
            }
        }
    }

    
}
