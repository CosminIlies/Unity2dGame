using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlacksmithSlot : MonoBehaviour
{
    
    public Image icon;
    public Text itemName;
    public Text multiplierText;

    private Recipe recipe;
    private Blacksmith blacksmith;
    private int multiplier;


    public void setRecipe(Recipe recipe, int multiplier, Blacksmith blacksmith) {
        this.recipe = recipe;
        this.blacksmith = blacksmith;
        this.multiplier = multiplier;

        icon.sprite = recipe.result.icon;
        itemName.text = recipe.result.name;
        multiplierText.text = multiplier.ToString();
    }
    public Recipe getRecipe(){
        return recipe;
    }

    

    public void Craft(){

        if(InventorySystem.instance.isFull(recipe.result)){
            return;
        }

        foreach(Ingredient ingredient in recipe.ingredients){
            if(blacksmith.HaveEnoughtIngredient(ingredient) == -1){
                return;
            }
        }

        foreach(Ingredient ingredient in recipe.ingredients){
            blacksmith.RemoveIngredients(ingredient);

        }

        InventorySystem.instance.addItem(recipe.result, 1);

    }
}
