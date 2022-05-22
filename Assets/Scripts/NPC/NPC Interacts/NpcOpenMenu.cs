using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcOpenMenu : NpcInteract
{
    public GuiMenus menu;

   public override void Interact(NpcManager manager)
    {
        if(PlayerManager.instance.getState() != PlayerStates.Idle){
            Debug.Log("Other state");
            return;
        }


        base.Interact(manager);
        GuiManager.instance.setActiveMenu(menu);
        PlayerManager.instance.setState(PlayerStates.InMenu);
    }

    public override void FinishInteraction(bool success)
    {
        PlayerManager.instance.setState(PlayerStates.Idle);
        base.FinishInteraction(success);
        SavingSystem.instance.Save();
        GuiManager.instance.closeMenus();

    }
}
