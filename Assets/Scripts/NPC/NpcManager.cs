using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcManager : MonoBehaviour
{
    public new string name;
    public GameObject interactObj;
    public NpcInteract interact;

    bool canInteract = false;

    private void Start() {
        interactObj.SetActive(false);
    }


    private void Update() {
        if(Vector3.Distance( PlayerManager.instance.gameObject.transform.position, transform.position) > 5){
            if(canInteract){
                interactObj.SetActive(false);
                canInteract = false;
            }

            if(interact.isStarted){
                interact.FinishInteraction(false);
            }
        } 
    }

    private void OnMouseDown() {
        if(canInteract){
            interact.Interact(this);
        }
    }

    private void OnMouseEnter() {
        if(Vector3.Distance( PlayerManager.instance.gameObject.transform.position, transform.position) < 5){
            interactObj.SetActive(true);
            canInteract = true;
        } 
    }
    private void OnMouseExit() {
        interactObj.SetActive(false);
        canInteract = false;
    }
    
}
