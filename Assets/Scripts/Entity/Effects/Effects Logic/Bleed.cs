using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bleed : Effect
{
    public float dmg = 2;
    public float timePerActivation = 1;

    private Timer timer;

    public Bleed(EffectAsset asset) : base(asset) {
        timer = new Timer(timePerActivation);
    }

    public override void UpdateEffect(Stats stats)
    {
        base.UpdateEffect(stats);
        timer.updateTimer();
        if(timer.isFinished){
            stats.health -= dmg;
            timer.reset();
        }
        

    }
}
