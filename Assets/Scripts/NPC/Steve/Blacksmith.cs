using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blacksmith : MonoBehaviour
{

    public List<Ingredient> ingredinets = new List<Ingredient>(); 

    public GameObject slotsParent;
    public GameObject slotprefab;
    public List<BlacksmithSlot> slots = new List<BlacksmithSlot>();
    public List<Recipe> recipes;


    public int HaveEnoughtIngredient(Ingredient ingredinetNeeded){
        foreach(Ingredient ingredinet in ingredinets){
            if(ingredinet.item == ingredinetNeeded.item){
                if(ingredinet.count >= ingredinetNeeded.count){
                    return ingredinet.count / ingredinetNeeded.count;
                }else{
                    return -1;
                }
            }
        }

        return -1;
    }

    public void RemoveIngredients(Ingredient ingredientToRemove){
        foreach(Ingredient ingredinet in ingredinets){
            if(ingredinet.item == ingredientToRemove.item){
                ingredinet.count -= ingredientToRemove.count;
                if(ingredinet.count <= 0){
                    ingredinets.Remove(ingredinet);
                }
                updateRecipes();
                return;
            }
        }
    }

    public void AddIngredient(Item item, int count){

        foreach(Ingredient ingredient in ingredinets){
            if(ingredient.item == item){
                ingredient.count+= count;
                updateRecipes();
                return;
            }
        }

        ingredinets.Add(new Ingredient(item, count));
        updateRecipes();
    }

    public void updateRecipes(){
        foreach(Transform child in slotsParent.transform){
            Destroy(child.gameObject);
        }
        int nrOfChildren = 0;
        foreach(Recipe recipe in recipes){
            bool canCraft = true;
            int minimum = 100000;
            foreach(Ingredient ingredient in recipe.ingredients){
                if(HaveEnoughtIngredient(ingredient) == -1){
                    canCraft = false;
                }
                minimum = Math.Min(minimum, HaveEnoughtIngredient(ingredient));
            }
            Debug.Log(canCraft);
            if(canCraft){
                BlacksmithSlot slot = Instantiate(slotprefab, slotsParent.transform).GetComponent<BlacksmithSlot>();
                slot.setRecipe(recipe, minimum, this);
                slots.Add(slot);
                nrOfChildren++;
            }

        } 


        slotsParent.GetComponent<RectTransform>().sizeDelta = new Vector2(0,nrOfChildren*110);

    }


    

}

