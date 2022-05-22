using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="new SpellAsstet", menuName ="Spell/new spell asstet")]
public class SpellAsset : ScriptableObject
{
    public new string name;
    public string description;
    public float manaNeeded;
    public float cooldown;
    public Sprite spellIconSprite;

}
