using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseDmg : MonoBehaviour
{
    [SerializeField] private float attackPulse;
    [SerializeField] private bool applyToPlayer;

    [SerializeField] private float dmg;

    private EnemyManager manager;
    private Timer timer;
    private bool touchTarget;
    private PlayerManager pManager;

    List<EnemyManager> enemiesInCollision = new List<EnemyManager>();
    List<EnemyManager> auxlist = new List<EnemyManager>();
    private void Awake()
    {
        manager = GetComponent<EnemyManager>();
        timer = new Timer(attackPulse);
        pManager = FindObjectOfType<PlayerManager>();

    }
    private void Update()
    {
        timer.updateTimer();
        if (touchTarget && timer.isFinished)
        {
            if (applyToPlayer)
            {
                pManager.getHit(dmg);
            }
            else
            {
                swapList();
                for (int i = 0; i < enemiesInCollision.Count; i++)
                {
                    if (enemiesInCollision[i] != null)
                        enemiesInCollision[i].getHit(dmg); 
                }

            }
            timer.reset();
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && applyToPlayer)
        {
            dmg = manager.enemyStats.stats.dmg;
            touchTarget = true;
        }
        if (collision.tag == "Enemy" && !applyToPlayer)
        {
            auxlist.Add(collision.GetComponent<EnemyManager>());
            touchTarget = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" && applyToPlayer)
        {
            touchTarget = false;
        }
        if (collision.tag == "Enemy" && !applyToPlayer)
        {
            touchTarget = false;
            

        }
    }

    void swapList()
    {
        enemiesInCollision = auxlist;
    }



}
