using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class SmeltSlot : MonoBehaviour, IDropHandler
{
    public Blacksmith blacksmith;
    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null)
        {
            InventorySlot slotInventory = eventData.pointerDrag.GetComponent<InventorySlot>();
            if (slotInventory != null && slotInventory.item.smelt.Length > 0)
            {
                if(Input.GetKey(KeyCode.LeftShift)){
                    foreach(Ingredient ingredient in slotInventory.item.smelt){
                        blacksmith.AddIngredient(ingredient.item, ingredient.count * slotInventory.count);
                    }
                    InventorySystem.instance.Delete(slotInventory);
                }else{
                    foreach(Ingredient ingredient in slotInventory.item.smelt){
                        blacksmith.AddIngredient(ingredient.item, ingredient.count);
                    }
                    InventorySystem.instance.RemoveOne(slotInventory);
                }
            }
        }
    }

    
}
