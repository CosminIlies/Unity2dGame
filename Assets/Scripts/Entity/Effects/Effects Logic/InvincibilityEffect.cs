using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibilityEffect : Effect
{
    public InvincibilityEffect(EffectAsset asset) : base(asset) { }
    float health = -1;

    public override void UpdateEffect(Stats stats)
    {
        base.UpdateEffect(stats);
        if (health == -1)
        {
            health = stats.health;
        }

        stats.health = health;
        

    }
}
