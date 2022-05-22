using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SlashAttack : MonoBehaviour, IEnemyAttack
{
    public GameObject slashObj;
    public Transform slashPoint;


    public void attack()
    {
        Instantiate(slashObj, slashPoint.position, slashPoint.rotation);
    }
}
