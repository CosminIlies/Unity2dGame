using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


[System.Serializable]
public abstract class Spell: MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public SpellAsset asset;
    public SpellUI UI;

    [HideInInspector]
    public float formattedTime;



    private Timer timer;

    public virtual void Start()
    {
        timer = new Timer(asset.cooldown);
        UI.spellIcon.sprite = asset.spellIconSprite;
    }

    public virtual void Update()
    {
        timer.updateTimer();
        if (timer.isFinished)
        {
            UI.spellCoodown.color = new Color(0, 0, 0, 0f);
            UI.cooldownText.text = "";
        }
        else
        {
            formattedTime = formatCooldownTime(asset.cooldown - timer.getTime(),1);
            UI.spellCoodown.color = new Color(0f, 0f, 0f, 0.25f);
            UI.cooldownText.text = formattedTime.ToString();
            UI.spellCoodown.fillMethod = Image.FillMethod.Radial360;
            UI.spellCoodown.fillAmount = timer.getTime() / asset.cooldown;
        }

    }

    public abstract bool activate();

    public bool useSpell()
    {
        if (timer.isFinished)
        {

            timer.reset();
            return true;
        }
        else
        {
            return false;
        }
    }

    float formatCooldownTime(float time, int numbersOfDecimals)
    {
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



    public void OnPointerEnter(PointerEventData eventData)
    {
        GameObject obj = HoverDetails.instance.gameObject;
        obj.GetComponent<CanvasGroup>().alpha = 1;
        obj.transform.position = eventData.position;
        HoverDetails.instance.setSpell(this);

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HoverDetails.instance.gameObject.GetComponent<CanvasGroup>().alpha = 0;
    }
}

[System.Serializable]
public class SpellUI
{
    public Image spellIcon;
    public Image spellCoodown;
    public Text cooldownText;
}
