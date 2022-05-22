using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName ="Recipes/ new Recipe", fileName = "new Recipe")]
public class Recipe : ScriptableObject
{
    public Item result;
    public Ingredient[] ingredients;

}

[System.Serializable]
public class Ingredient{
    public Item item;
    public int count;

    public Ingredient(Item item, int count){
        this.item = item;
        this.count = count;
    }
}
