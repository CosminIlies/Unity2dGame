using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsDataBase : MonoBehaviour
{

    public static ItemsDataBase instance;
    private void Awake() {
        instance = this;
    }
    public List<Item> items;

    public int GetItemId(Item item){
        for(int i = 0; i < items.Count; i++){
            if(item == items[i]){
                return i;
            }
        }
        return -1;
    }
}
