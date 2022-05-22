using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{

    public GameObject slotsParent;
    public GameObject prefab;
    private List<BuySlot> slots = new List<BuySlot>();
    public List<Item> items = new List<Item>();
    public int offerts;

    private void Start() {
        for(int i = 0; i < offerts; i++){
            slots.Add(Instantiate(prefab, slotsParent.transform).GetComponent<BuySlot>());
            
            int offert = Random.Range(0, items.Count);

            slots[i].setItem(items[offert], this);
            items.Remove(items[offert]);

        }
        updateUI();
    }

    public void updateUI(){

        slotsParent.GetComponent<RectTransform>().sizeDelta = new Vector2(0,slots.Count*110);
    }

}
