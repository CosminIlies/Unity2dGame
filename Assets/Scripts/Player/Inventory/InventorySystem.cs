using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    #region Singleton
    public static InventorySystem instance;
    private void Awake()
    {
        instance = this;
    }

    #endregion

    public List<InventorySlot> slots;

    public bool addItem(Item item, int count)
    {

        foreach(InventorySlot slot in slots)
        {
            if(slot.item == item){
                if(slot.count + count <= item.maxStaxkSize)
                {
                    slot.count += count;
                    slot.updateGui();
                    return true;
                }
            }
        }
        foreach(InventorySlot slot in slots)
        {
            if (slot.item == null)
            {
                slot.item = item;
                slot.count = count;
                slot.updateGui();
                return true;

            }
        }


        return false;
    }
    public bool isFull(){
        foreach(InventorySlot slot in slots)
        {
            if(slot.item == null){
                return false;
            }
        }
        return true;
    }
    public bool isFull(Item item){
        foreach(InventorySlot slot in slots)
        {
            if(slot.item == null){
                return false;
            }
            if(slot.item == item &&slot.count < item.maxStaxkSize){
                return false;
            }
        }
        return true;
    }
    public void swapItems(InventorySlot slot1, InventorySlot slot2)
    {
        Item auxitem = slot1.item;
        int auxcount = slot1.count;

        if(slot1.item == slot2.item && slot1.count < slot1.item.maxStaxkSize && slot2.count < slot2.item.maxStaxkSize)
        {
            if(slot1.count + slot2.count <= slot1.item.maxStaxkSize)
            {
                slot2.count = slot1.count + slot2.count;
                slot1.count = 0;
                slot1.item = null;
            }
            else if (slot1.count < slot1.item.maxStaxkSize)
            {
                int diff = slot2.item.maxStaxkSize - slot2.count;

                if (diff < slot1.count)
                {
                    slot2.count = slot1.item.maxStaxkSize;
                    slot1.count -= diff;
                }

            }

        }
        else
        {
            slot1.item = slot2.item;
            slot1.count = slot2.count;

            slot2.item = auxitem;
            slot2.count = auxcount;

        }



        slot1.updateGui();
        slot2.updateGui();
    }
    public void EquipItem(InventorySlot inventorySlot, EquipmentSlot equipmentSlot)
    {

        Equipment item = null;
        try
        {
            item = (Equipment)inventorySlot.item;
        }
        catch
        {
            Debug.Log("Cannot be equiped");
        }

        if (item != null)
        {
            
            if (item.type == equipmentSlot.type)
            {
                inventorySlot.item = equipmentSlot.item;
                equipmentSlot.item = item;
                inventorySlot.count = 1;
            }
        }


        inventorySlot.updateGui();
        equipmentSlot.updateGui();
        PlayerStats.instance.updateStats();
    }
    public void Unequip(InventorySlot inventorySlot, EquipmentSlot equipmentSlot)
    {
        if(inventorySlot.item != null)
            return;


        inventorySlot.item = equipmentSlot.item;
        equipmentSlot.item = null;
        inventorySlot.count = 1;


        inventorySlot.updateGui();
        equipmentSlot.updateGui();
        PlayerStats.instance.updateStats();
    }
    public void RemoveOne(InventorySlot inventorySlot){

        if(inventorySlot.item == null)
            return;

        inventorySlot.count -= 1;
        if(inventorySlot.count <= 0)
            inventorySlot.item = null;

        inventorySlot.updateGui();
    }
    public void Delete(InventorySlot inventorySlot)
    {
        if(inventorySlot.item == null)
            return;

        inventorySlot.item = null;
        inventorySlot.count = 1;

        inventorySlot.updateGui();
    }
    public void Delete(EquipmentSlot equipmentSlot)
    {
        if(equipmentSlot.item == null)
            return;

        equipmentSlot.item = null;

        equipmentSlot.updateGui();
    }
}

