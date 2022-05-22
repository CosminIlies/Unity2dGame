using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuySlot : MonoBehaviour
{
       
    public Image icon;
    public Text itemName;
    public Text priceText;

    private Item item;
    private float price;
    private Shop shop;



    public void setItem(Item item, Shop shop) {
        this.item = item;
        this.shop = shop;
        this.price = item.sellPrice + (int)Random.Range(item.sellPrice/2, item.sellPrice);

        icon.sprite = item.icon;
        itemName.text = item.name;
        priceText.text = price.ToString() + "$";
    }
    public Item getItem(){
        return item;
    }

    

    public void Buy(){

        if(InventorySystem.instance.isFull(item) || PlayerStats.instance.money < price){
            return;
        }

        PlayerStats.instance.money -= price;

        InventorySystem.instance.addItem(item, 1);
        PlayerStats.instance.updateUI();
        shop.updateUI();
        Destroy(this.gameObject);

    }
}
