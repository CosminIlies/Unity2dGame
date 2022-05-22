using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    #region Singletone
    public static PlayerManager instance;
    private void Awake()
    {
        instance = this;
        playerStats.init();
    }

    #endregion

    public PlayerStats playerStats = new PlayerStats();
    [SerializeField()] private PlayerStates states = PlayerStates.Idle;
    [SerializeField()] private GameObject graphics;
    [SerializeField()] private GameObject attackPoint;
    [SerializeField()] private Slider healthslider;
    [SerializeField()] private Slider manaSlider;
    [SerializeField()] private Spell spell;
    [SerializeField()] private Spell spell2;

    [SerializeField()] private Spell recall;


    private Animator anim;
    private PlayerController controller;
    private PlayerAttackPoint attackPointScript;
    private SpriteRenderer spriteRenderer;
    private Camera cam;
    private Timer attackTimer;



    void Start()
    {
        
        controller = GetComponent<PlayerController>();
        controller.setMovementSpeed(playerStats.stats.movementSpeed);
        controller.enabled = true;

        spriteRenderer = graphics.GetComponent<SpriteRenderer>();
        anim = graphics.GetComponent<Animator>();

        attackPointScript = attackPoint.GetComponent<PlayerAttackPoint>();

        cam = Camera.main;
        attackTimer = new Timer(playerStats.stats.attackSpeed);
        updateGui();


    }

    void Update()
    {
        attackTimer.updateTimer();
        playerStats.update();
        updateGui();

        switch (states)
        {
            case PlayerStates.Idle:
                
                if (cam.ScreenToWorldPoint(Input.mousePosition).x < transform.position.x)
                {
                    spriteRenderer.flipX = true;
                }
                else if (cam.ScreenToWorldPoint(Input.mousePosition).x > transform.position.x)
                {
                    spriteRenderer.flipX = false;
                }


                if (Input.GetMouseButton(0) && attackTimer.isFinished)
                {
                    attackPointScript.attack();
                    attackTimer.reset();
                    setState( PlayerStates.Attacking);
                }

                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    spell.activate();
                }
                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    spell2.activate();
                }

                if(Input.GetKeyDown(KeyCode.B) && recall != null){
                    recall.activate();
                }

                if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f || Mathf.Abs(Input.GetAxis("Vertical")) > 0.1f)
                {
                    setState( PlayerStates.Walking);
                    anim.SetBool("isWalking", true);
                }

                break;

            case PlayerStates.Walking:
                controller.enabled = true;


                if (cam.ScreenToWorldPoint(Input.mousePosition).x < transform.position.x)
                {
                    spriteRenderer.flipX = true;
                }else if(cam.ScreenToWorldPoint(Input.mousePosition).x > transform.position.x)
                {
                    spriteRenderer.flipX = false;
                }


                if (Input.GetMouseButton(0) && attackTimer.isFinished)
                {
                    attackPointScript.attack();
                    attackTimer.reset();
                    setState(PlayerStates.Attacking);
                }

                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    spell.activate();
                }
                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    spell2.activate();
                }

                if (Mathf.Abs(Input.GetAxis("Horizontal")) < 0.1f && Mathf.Abs( Input.GetAxis("Vertical") ) < 0.1f)
                {
                    setState(PlayerStates.Idle);
                    anim.SetBool("isWalking", false);
                }
                break;

            case PlayerStates.Attacking:

                setState( PlayerStates.Idle);
                break;

            case PlayerStates.InMenu:
                Time.timeScale = 0;

                break;

            case PlayerStates.Recalling:
                Debug.Log("Recalling");
                break;
            case PlayerStates.IsChating:
                controller.enabled = true;
                break;
        }
    }

    public void getHit(float dmg)
    {
        playerStats.stats.health -= dmg;

        setState(PlayerStates.Idle);
        updateGui();
    }


    private void updateGui()
    {
        healthslider.maxValue = playerStats.stats.maxHealth;
        healthslider.value = playerStats.stats.health;

        manaSlider.maxValue = playerStats.stats.attackSpeed;
        manaSlider.value = attackTimer.getTime();
    }

    public void setState(PlayerStates state)
    {
        if(state != PlayerStates.Recalling && recall != null)
            ((Recall)recall).interupt();

        states = state;
        Time.timeScale = 1;
        controller.enabled = false;
    }

    public PlayerStates getState(){
        return states;
    }




}

public enum PlayerStates
{
    Idle,
    Walking,
    Attacking,
    InMenu,
    Recalling,
    IsChating
};
