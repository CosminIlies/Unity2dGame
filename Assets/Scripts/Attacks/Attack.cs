using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="new Attack", menuName ="Attacks/new Attack")]
public class Attack : ScriptableObject
{
    public List<Proj> projectiles;
}

[System.Serializable]
public class Proj
{
    public string affectTag;
    public GameObject obj;
    public Vector3 statingPosition;
    public Vector3 statingRotation;
    public float timeDelay;
    public float speed;
    public float dmg;
    public float lifeTime;
}
