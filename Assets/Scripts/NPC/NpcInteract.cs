using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcInteract : MonoBehaviour
{
    protected NpcManager manager;
    public bool isStarted = false;
    public NpcInteract doAfterIsFinished;

    public virtual void Interact(NpcManager manager){

        this.manager = manager;
        isStarted = true;
    }
    public virtual void FinishInteraction(bool success){
        if( doAfterIsFinished != null && success){
            doAfterIsFinished.Interact(manager);
        }
        isStarted = false;
    }
}
