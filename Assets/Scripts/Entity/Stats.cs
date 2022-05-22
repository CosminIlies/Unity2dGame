using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stats
{

    public float maxHealth;
    public float baseMovementSpeed;
    public float baseAttackSpeed;
    public float baseDmg;

    [HideInInspector]
    public float health;
    [HideInInspector]
    public float movementSpeed;
    [HideInInspector]
    public float attackSpeed;
    [HideInInspector]
    public float dmg;

    List<Effect> currentEffects = new List<Effect>();
    [HideInInspector]
    public List<Effect> auxList = new List<Effect>();
    [HideInInspector]
    public bool needToUpdate = false;

    public void init()
    {
        resetStats();
    }

    public void resetStats()
    {
        health = maxHealth;
        movementSpeed = baseMovementSpeed;
        attackSpeed = baseAttackSpeed;
        dmg = baseDmg;
    }

    public void updateEffects()
    {
        if(needToUpdate)
            removeOrAddEffect();
            
        for (int i = 0; i < currentEffects.Count; i++)
        {
            currentEffects[i].UpdateEffect(this);
        }
    }

    public void addEffect(Effect effect)
    {
        foreach(Effect effectInList in auxList){
            if(effectInList.asset == effect.asset){
                effectInList.timer.reset();
                needToUpdate = true;
                return;
            }
        }
        auxList.Add(effect);
        needToUpdate = true;

    }

    public void endEffect(Effect effect)
    {
        auxList.Remove(effect);
        needToUpdate = true;
    }

    void removeOrAddEffect()
    {
        currentEffects = auxList;
        needToUpdate = false;
    }
}
