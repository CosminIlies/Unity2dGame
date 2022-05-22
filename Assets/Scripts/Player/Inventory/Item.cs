using UnityEngine;

[CreateAssetMenu(fileName ="new Item", menuName ="Items/new Item")]
public class Item : ScriptableObject
{
    public new string name;
    public string description;
    public Sprite icon;
    public int maxStaxkSize;
    public float sellPrice;
    public Ingredient[] smelt;


    public virtual void use(){  
        
    }

}
