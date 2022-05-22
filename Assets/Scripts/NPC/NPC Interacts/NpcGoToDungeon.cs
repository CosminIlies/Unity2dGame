using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcGoToDungeon : NpcInteract
{
    public override void Interact(NpcManager manager)
    {
        if(PlayerManager.instance.getState() != PlayerStates.Idle){
            Debug.Log("Other state");
            return;
        }
        base.Interact(manager);
        SavingSystem.instance.Save();
        Application.LoadLevel(2);
    }

    public override void FinishInteraction(bool success)
    {
        base.FinishInteraction(success);
    }
}
