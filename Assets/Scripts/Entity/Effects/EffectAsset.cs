using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EffectAsset", menuName = "Effects/new EffectAsset")]
public class EffectAsset : ScriptableObject {
    

    public new string name;
    public string description;
    public float duration;
    public Sprite effectIconSprite;
}
