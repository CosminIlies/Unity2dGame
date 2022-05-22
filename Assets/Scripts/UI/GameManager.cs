using UnityEngine;

public class GameManager : MonoBehaviour
{

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {

            if (!GuiManager.instance.getActiveMenu(GuiMenus.Equipment))
            {
                GuiManager.instance.setActiveMenu(GuiMenus.Equipment);
                PlayerManager.instance.setState(PlayerStates.InMenu);
            }
            else
            {
                GuiManager.instance.closeMenus();
                PlayerManager.instance.setState(PlayerStates.Idle);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GuiManager.instance.closeMenus();
            PlayerManager.instance.setState(PlayerStates.Idle);
        }
    }
}
