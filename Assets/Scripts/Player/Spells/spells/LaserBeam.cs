using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : Spell
{

    public GameObject laserBeamPrefab;
    public Transform point;
    public float time;

    Timer timer;
    Transform instanceOfSpell;

    public override void Start()
    {
        base.Start();
        timer = new Timer(time);
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
            instanceOfSpell = Instantiate(laserBeamPrefab, point.position, point.rotation, point).transform;
            timer.reset();
            return true;
        }
        else
        {
            return false;
        }
    }


}
