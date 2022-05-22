using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recall : Spell
{
    public GameObject pentagramPrefab;
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
            PlayerManager.instance = null;
            SavingSystem.instance.Save();
            Application.LoadLevel(1);
        }
    }


    public override bool activate()
    {

        if (this.useSpell())
        {
            timer.isStopped = false;
            instanceOfSpell = Instantiate(pentagramPrefab, point.position, point.rotation, point).transform;
            timer.reset();
            PlayerManager manager = PlayerManager.instance;
            manager.setState(PlayerStates.Recalling);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void interupt(){
        if(instanceOfSpell != null)
            Destroy(instanceOfSpell.gameObject);
        timer.isStopped = true;
        timer.reset();
    }
}
