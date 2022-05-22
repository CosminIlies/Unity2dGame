using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugEffect : Effect
{
    public DebugEffect(EffectAsset asset) : base(asset) { }


    public override void UpdateEffect(Stats stats)
    {
        base.UpdateEffect(stats);
        Debug.Log(timer.getTime());

    }
}
