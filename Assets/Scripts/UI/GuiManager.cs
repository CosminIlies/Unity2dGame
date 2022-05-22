using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiManager : MonoBehaviour
{
    #region Singleton
    public static GuiManager instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion
    
    [SerializeField] private menu[] menus;
    [SerializeField] private GameObject inventorymenu;

    public void setActiveMenu(GuiMenus toOpenMenu)
    {
        foreach (menu thisMenu in menus)
        {
            

            if (thisMenu.menuType == toOpenMenu)
            {
                thisMenu.menuObj.SetActive(true);
                if(thisMenu.openWithInventory){
                    inventorymenu.SetActive(true);
                }else{
                    inventorymenu.SetActive(false);
                }

            }
            else
            {
                thisMenu.menuObj.SetActive(false);
                HoverDetails.instance.gameObject.GetComponent<CanvasGroup>().alpha = 0;
            }
        }

    }
    public bool getActiveMenu(GuiMenus menu)
    {
        foreach (menu thisMenu in menus)
        {
            if (thisMenu.menuType == menu)
            {
                 return thisMenu.menuObj.activeInHierarchy;
            }

        }

        return false;
    }

    public void closeMenus()
    {
        inventorymenu.SetActive(false);
        foreach (menu thisMenu in menus)
        {
            thisMenu.menuObj.SetActive(false);
        }
        HoverDetails.instance.gameObject.GetComponent<CanvasGroup>().alpha = 0;
    }

    [System.Serializable]
    private class menu
    {
        [SerializeField] public GameObject menuObj;
        [SerializeField] public GuiMenus menuType;
        [SerializeField] public bool openWithInventory;
    }

}


public enum GuiMenus
{
    Equipment,
    PauseMenu,
    BlackSmith,
    Shop
}
