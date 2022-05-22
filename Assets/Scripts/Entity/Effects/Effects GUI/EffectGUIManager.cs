using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectGUIManager : MonoBehaviour
{
    
    public GameObject effectGuiPrefab;
    public GameObject effectGuiParent;

    public void UpdateGuis(Stats stats)
    {


        Debug.Log("Update Effects");
        foreach (Transform child in effectGuiParent.transform)
        {
            Destroy(child.gameObject);
        }

        foreach(Effect effect in stats.auxList){
            if(effect != null){
                EffectGUI gui = Instantiate(effectGuiPrefab, effectGuiParent.transform).GetComponent<EffectGUI>();
                gui.setEffect(effect);
            }

        }
        
        
    }


}
