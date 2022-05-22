using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicShield : Spell
{

    public GameObject shealdPrefab;
    public Transform point;


    public EffectAsset effectAsset;

    Timer timer;
    Transform instanceOfSpell;
    Stats stats;

    public override void Start()
    {
        base.Start();
        stats = PlayerStats.instance.stats;
        timer = new Timer(effectAsset.duration);
    }
    public override void Update()
    {
        base.Update();
        timer.updateTimer();

        if (timer.isFinished && instanceOfSpell != null)
        {
            Destroy(instanceOfSpell.gameObject);
        }
    }
    public override bool activate()
    { 
        if (this.useSpell())
        {
            instanceOfSpell = Instantiate(shealdPrefab, point.position, point.rotation, point).transform;
            InvincibilityEffect effect = new InvincibilityEffect(effectAsset);
            stats.addEffect(effect);

            timer.reset();
            return true;
        }
        else
        {
            return false;
        }

    }
}
