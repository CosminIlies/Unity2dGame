using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{

    [SerializeField()] private EnemyStates state = EnemyStates.Idle;
    [SerializeField()] public EnemyStats enemyStats = new EnemyStats();
    [SerializeField()] private GameObject graphics;
    [SerializeField()] private GameObject itemHolder;
    [SerializeField()] private Slider healthSlider;



    private EnemyController controller;
    private Animator anim;
    private IEnemyAttack enemyAttack;
    private Timer timerAttack;
    private bool fliped;


    void Start()
    {
        controller = GetComponent<EnemyController>();
        
        controller.enabled = true;

        enemyAttack = GetComponent<IEnemyAttack>();
        anim = graphics.GetComponent<Animator>();

        enemyStats.init();

        healthSlider.maxValue = enemyStats.stats.health;
        healthSlider.value = enemyStats.stats.health;

        timerAttack = new Timer(enemyStats.stats.attackSpeed);
    }

    void Update()
    {

        timerAttack.updateTimer();
        switch (state)
        {
            case EnemyStates.Idle:
                if (controller.player != null)
                {
                    controller.canWalk = false;

                    if (Vector2.Distance(controller.player.transform.position, transform.position) < enemyStats.chaseRange)
                    {
                        state = EnemyStates.Walking;
                        controller.setMovementSpeed(enemyStats.stats.movementSpeed);
                    }
                }
                break;

            case EnemyStates.Walking:
                
                controller.canWalk = true;
                if (timerAttack.isFinished && Vector2.Distance(controller.player.transform.position, transform.position) < enemyStats.attackRange)
                {
                    controller.canWalk = false;
                    enemyAttack.attack();
                    anim.SetTrigger("attack");
                    timerAttack.reset();
                    state = EnemyStates.Attacking;
                }else if (Vector2.Distance(controller.player.transform.position, transform.position) > enemyStats.attackRange)
                    state = EnemyStates.Idle;

                if (transform.position.x < controller.player.transform.position.x)
                    fliped = false;
                else
                    fliped = true;


                if (fliped)
                    transform.eulerAngles =  Vector3.up * 180;
                else
                    transform.eulerAngles =  Vector3.up * 0;
                healthSlider.transform.eulerAngles = Vector3.up * 0;

                break;

            case EnemyStates.Attacking:
                if(timerAttack.isFinished)
                    state = EnemyStates.Idle;

                break;
        }


    }

    bool alreadydead = false;
    public void getHit(float dmg)
    {
        enemyStats.stats.health -= dmg;
        healthSlider.value = enemyStats.stats.health;

        if (enemyStats.stats.health <= 0 && !alreadydead )
            Death();
    }


    void Death()
    {
        alreadydead = true;
        PickUpItem item = Instantiate(itemHolder, transform.position, Quaternion.identity).GetComponent<PickUpItem>();
        item.spawnItem(enemyStats.drops.drop(), 1);
        Destroy(this.gameObject);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, enemyStats.chaseRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, enemyStats.attackRange);
    }
}

enum EnemyStates
{
    Idle,
    Walking,
    Attacking
};
