using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcStartDialogue : NpcInteract
{

    public GameObject dialogueBox;
    public Dialogue dialogue;
    public float timePerChar = 0.5f;


    private Text name;
    private Text dialogueBody;
    private int index = -1;
    private Timer timer;
    private int charIdx = 0;

    private void Start() {
        name = dialogueBox.transform.GetChild(0).GetChild(0).GetComponent<Text>();
        dialogueBody = dialogueBox.transform.GetChild(1).GetChild(0).GetComponent<Text>();
        timer = new Timer(timePerChar);
        timer.isStopped = true;
    }


    private void Update() {
        timer.updateTimer();
        if(isStarted){

            if(Input.GetMouseButtonDown(0)){
                nextDialogue();
                return;
            }

            if(timer.isFinished && charIdx < dialogue.dialogues[index].Length){
                timer.reset();
                dialogueBody.text += dialogue.dialogues[index][charIdx];
                charIdx++;
            }

        }
    }




    private void nextDialogue(){
        index++;
        dialogueBody.text = "";
        charIdx = 0;

        if(index >= dialogue.dialogues.Length){
            FinishInteraction(true);
            return;
        }

    }


    public override void Interact(NpcManager manager)
    {   
        if(PlayerManager.instance.getState() != PlayerStates.Idle){
            Debug.Log("Other state");
            return;
        }


        base.Interact(manager);
        dialogueBox.SetActive(true);
        name.text = manager.name;
        timer.isStopped = false;
        PlayerManager.instance.setState(PlayerStates.IsChating);
    }

    public override void FinishInteraction(bool success)
    {
        PlayerManager.instance.setState(PlayerStates.Idle);
        base.FinishInteraction(success);
        Debug.Log("Finish interaction");
        dialogueBox.SetActive(false);
        index = -1;
        charIdx = 0;
        timer.isStopped = true;
        
    }
}
