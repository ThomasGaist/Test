﻿using System; 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void DeadEventHandler(); 

[RequireComponent(typeof(Controller2D))]
public class Player : Character
{

    private static Player instance;

    public static Player Instance
    {
        get
        {
            if(instance == null)
            {
                instance = GameObject.FindObjectOfType<Player>();
            }
            return instance; 

        }
    }

    public event DeadEventHandler Dead;

    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    
    //Equipmentkode
    [SerializeField]
    private GearSocket[] gearsockets;
    [SerializeField]
    private WeaponSocket[] weaponsockets;
    private bool flipped;

	public float jumpHeight = 4;
    public float timeToJumpApex = .4f;


    private bool immortal = false;
    public Vector3 startPos;

    [SerializeField]
    private float immortalTime;


    float accelerationTimeAirborne = .2f;
    [SerializeField] float accelerationTimeGrounded = .1f;
    public float moveSpeed = 30f;
    //public bool flip;
    private bool attack; 

    float gravity;
    float jumpVelocity;

    Vector3 velocity;
    float velocityXSmoothing;


    Controller2D controller;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    Rigidbody2D rb;

    private float currentSpeed;

    public override bool IsDead
    {
        get
        {

            if (health <= 0)
            {
                OnDead();
               
            }
            return health <= 0; 
        }
    }

    public float CurrentSpeed { get => currentSpeed;}
    public bool Flipped { get => flipped;}

    void Start()
    {
        controller = GetComponent<Controller2D>();

        rb = GetComponent<Rigidbody2D>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;

        startPos = new Vector3(-2.11f, -0.87f, 4);

       
       // flip = spriteRenderer.flipX;

    }
    // print("Gravity:" + gravity + "Jump Velocity" + jumpVelocity);

    void Update()
    {

        
        

        if (!TakingDamage && !IsDead)
        {
            controller.Move(velocity * Time.deltaTime);

            Flip();
            HandleMovement();

            HandleInput();
        }

        if(!TakingDamage && !IsDead)
        {
            if(transform.position.y <= -700f)
            {
                Death();
            }
        }

    }

    private void FixedUpdate()
    {

        if (!TakingDamage && !IsDead)
        {
            HandleAttacks();
            //HandleMovement();
            ResetValues();
        }
    }

    public void OnDead()
    {
        if(Dead != null)
        {
            Dead();
        }
    }


    private void HandleMovement()
    {
        bool grounded = true;

        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
        }

        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (Input.GetKeyDown(jumpKey) && controller.collisions.below)
        {
            velocity.y = jumpVelocity;

            animator.SetTrigger("JumpTrigger");
        }
        if (Input.GetKeyUp(jumpKey))
        {
            animator.ResetTrigger("JumpTrigger");
        }


        //Animations
        if (controller.collisions.below)
        {
            animator.SetBool("OnGround", true);
            grounded = true;
        }
        if (!controller.collisions.below)
        {
            animator.SetBool("OnGround", false);
            grounded = false;
        }

        animator.SetFloat("Speed", Mathf.Abs(velocity.x));




        //Movement speed


        float targetVelocityX = input.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);

        velocity.y += gravity * Time.deltaTime;


        //sets animation parameters for the individual bodyparts.
        foreach (GearSocket g in gearsockets)
        {
            g.Speed = Mathf.Abs(velocity.x);
            g.OnGround = grounded;
            g.FacingRight = facingRight;
        }
		foreach (WeaponSocket w in weaponsockets)
		{
			w.Speed = Mathf.Abs(velocity.x);
			w.OnGround = grounded;
			w.FacingRight = facingRight;
		}
        
    }

    public void Flip()
    {
        flipped = !flipped; 
        if (velocity.x > 0f && !facingRight || velocity.x < 0f && facingRight)
        {
            ChangeDirection();
        }
    }

    private void HandleAttacks()
    {
        if (attack && !this.animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            animator.SetTrigger("Attack");
            //rb.velocity = Vector2.zero;
        }
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            attack = true;
        }

    }

    private void ResetValues()
    {
        attack = false; 
    }

    /* blinker hvis spiller rammes
    private IEnumerator IndicateImmortal()
    {
        while (immortal)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(.1f);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(.1f);
        }
    }*/


    public override IEnumerator TakeDamage()
    {

        if (!immortal)
        {
            health -= 10;
            if (!IsDead)
            {
                //Animator.SetLayerWeight(1, 0);
                animator.SetTrigger("Damage");
                immortal = true;

                //StartCoroutine(IndicateImmortal());
                yield return new WaitForSeconds(immortalTime);

                immortal = false;

            }

            else
            {
                animator.SetLayerWeight(1, 0);
                animator.SetTrigger("Death");
                //Death();
            }
        }
      
       
        
    }

    public override void Death()
    {
        Destroy(gameObject);

        /*
        //Respawn Code, need startposition var and Idle animator Trigger.
        controller. = new Vector3(0,0,0); 
        Animator.SetTrigger("Idle");
        health = 30;
        transform.position = startPos;*/
    }


}