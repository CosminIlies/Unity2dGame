using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectGUI : MonoBehaviour
{
    public Effect effect;
    public Image slot;
    public Image slotCoolDown;
    //public Text nameText;
    //public Text Description
    //public ToolTip;
    public Text duration;


    public void setEffect(Effect effect){
        this.effect = effect;
        slot.sprite = effect.asset.effectIconSprite;
        slotCoolDown.fillMethod = Image.FillMethod.Radial360;
        slotCoolDown.color = new Color(0f, 0f, 0f, 0.25f);
    }


    void Update()
    {
        duration.text = formatCooldownTime(effect.asset.duration - effect.timer.getTime(),1).ToString();
        slotCoolDown.fillAmount = effect.timer.getTime() / effect.asset.duration;
    }



    float formatCooldownTime(float time, int numbersOfDecimals){
        float timeFormated = (int)time;

        time -= timeFormated;
        for (int i = 1; i <= numbersOfDecimals; i++)
        {
            time *= 10;
            timeFormated += (float)((int)time / Mathf.Pow(10,i) % 10);
            time -= (int) time;
        }


        return timeFormated;
    }

}
