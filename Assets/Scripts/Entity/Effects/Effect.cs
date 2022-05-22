using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Effect {
    public EffectAsset asset;

    public Timer timer;

    public Effect(EffectAsset asset)
    {
        this.asset = asset;
        timer = new Timer(asset.duration);
    }


    public virtual void UpdateEffect(Stats stats)
    {
        timer.updateTimer();

        if (timer.isFinished)
        { 
            stats.endEffect(this);
        }
    }


}
