using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class PlayerStats
{

    #region Singleton
    public static PlayerStats instance;

    #endregion


    public Stats stats;

    public EquipmentSlot book;
    public EquipmentSlot earring;
    public EquipmentSlot necklace;
    public EquipmentSlot ring;
    public float money;

    public Text maxHealthText;
    public Text movementSpeedText;
    public Text attsckSpeedText;
    public Text damageText;
    public Text moneyText;
    
    public EffectGUIManager effectGuiManager;


    public void init()
    {
        instance = this;
        updateStats();
        stats.init();
    }

    public void update(){
        if(stats.needToUpdate){
            effectGuiManager.UpdateGuis(stats);
        }


        stats.updateEffects();
    }

    public void updateStats()
    {
        stats.resetStats();
        if (book.item != null)
            addStats(book.item);

        if (earring.item != null)
            addStats(earring.item);

        if (necklace.item != null)
            addStats(necklace.item);

        if (ring.item != null)
            addStats(ring.item);

        updateUI();
    }


    void addStats(Equipment equipment)
    {
        stats.health += equipment.maxHelth;
        stats.movementSpeed += equipment.movementSpeed;
        stats.attackSpeed += equipment.attackSpeed;
        stats.dmg += equipment.damage;
    }
    public void updateUI()
    {
        moneyText.text = money.ToString();
        maxHealthText.text = "Max Healt: " + stats.health;
        movementSpeedText.text = "Movement Speed: " + stats.movementSpeed;
        attsckSpeedText.text = "Attack Speed: " + stats.attackSpeed;
        damageText.text = "Damage: " + stats.dmg;
    }

}
