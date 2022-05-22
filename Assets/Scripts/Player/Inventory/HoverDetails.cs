using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoverDetails : MonoBehaviour
{
    #region Singleton

    public static HoverDetails instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion


    [SerializeField] private new Text name;
    [SerializeField] private Text details;

    private Item hoverItem;
    private int count;




    public void setItem(Item hoverItem, int count)
    {
        this.hoverItem = hoverItem;
        this.count = count;
        Equipment item = null;
        details.text = "";
        try
        {
            item = (Equipment)hoverItem;
            name.text = item.name;
            if(count > 1)
            {
                name.text += " x" + count;
            }

            int onThisLine = 0;
            foreach(char character in item.description)
            {
                details.text += character;
                onThisLine++;
                if(onThisLine >= 25 && character == ' ')
                {
                    onThisLine = 0;
                    details.text += "\n";
                }
            }

            details.text += "\n" + "\n" + 
                            "Max Health : " + item.maxHelth+"\n"+
                            "Movement Speed : " + item.movementSpeed+"\n"+
                            "Attack Speed : " + item.attackSpeed+"\n"+
                            "Damage : " + item.damage+"\n";

            details.text += "\n" + "\n" + "Sell price : " + item.sellPrice+" each";
        }
        catch
        {
            name.text = hoverItem.name;

            if (count > 1)
            {
                name.text += " x" + count;
            }


            int onThisLine = 0;
            foreach (char character in hoverItem.description)
            {
                details.text += character;
                onThisLine++;
                if (onThisLine >= 25 && character == ' ')
                {
                    onThisLine = 0;
                    details.text += "\n";
                }
            }

            details.text += "\n" + "\n" + "Sell price : " + hoverItem.sellPrice + " each";
        }
    }

    public void setSpell(Spell hoverSpell){
        SpellAsset spell = hoverSpell.asset;

        details.text = "";
        name.text = spell.name + " " + hoverSpell.formattedTime;

        int onThisLine = 0;
        foreach(char character in spell.description)
        {
            details.text += character;
            onThisLine++;
            if(onThisLine >= 25 && character == ' ')
            {
                onThisLine = 0;
                details.text += "\n";
            }
        }

        details.text += "\n" + "\n" + 
                            "Mana needed : " + spell.manaNeeded+"\n"+
                            "Cooldown : " + spell.cooldown+"\n";

    }
}
