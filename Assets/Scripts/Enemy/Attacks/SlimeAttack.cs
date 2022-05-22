using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAttack : MonoBehaviour, IEnemyAttack
{
    public float startSpeed;
    public float frictionPower;


    private Transform player;
    private Vector2 dir;
    private float currentSpeed;
    private Rigidbody2D rb;

    private void Awake()
    {
        player = FindObjectOfType<PlayerManager>().transform;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (currentSpeed > 0)
        {
            currentSpeed -= frictionPower;
            rb.velocity = new Vector2(dir.x, dir.y) * currentSpeed * currentSpeed;
        }
    }


    public void attack()
    {
        dir = player.position -  transform.position;
        dir = dir.normalized;
        currentSpeed = startSpeed;
    }



}
