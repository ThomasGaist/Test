using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Character 
{


    private SpriteRenderer sr;

    private IEnemyState currentState;

    public GameObject Target { get; set; }

    public float movementSpeed;

    public float chaseMultiplier = 1.5f;

    #region VITALS and LEVEL

    private int level = 1;
   
    private LootTables lootDropper;
    private int lootlevel = 0;
    public int LootLevel {get => lootlevel;}

    public int EnemyLevel { get => level; set => level = value; }
    public int EnemyHealth { get => health; set => health = value; }
   

    //ATTACK PARAMETERS

    [SerializeField]
    private float meleeAttackDamage;
    public float MeleeAttackDamage { get => meleeAttackDamage; }
    [SerializeField]
    private float rangedAttackDamage;
    public float RangedAttackDamage { get => rangedAttackDamage; }
    #endregion

    #region UI
    public Image healthBar;
    #endregion

    #region EVENTS
    private GameEvents eventsystem;

    #endregion

    #region ID
   
    private int num = 1;
   
    #endregion

    [SerializeField]
    public float meleeRange = 3f; 
    public bool InMeleeRange
    {
        get
        {
            if(Target != null)
        {
            return Vector2.Distance(transform.position, Target.transform.position) <= meleeRange;
        }
            return false;
        }
    }

    public override bool IsDead
    {
        get
        {
            return health <= 0;
        }
    }

    private void Awake()
    {
        
       
    }
    public override void Start()
    {
        eventsystem = GameEvents.current;
        //eventsystem.onEnemyDamage += Damage;
      

        base.Start();
        Player.Instance.Dead += new DeadEventHandler(RemoveTarget);
        EnemyHealth = maxHealth;
        sr = GetComponent<SpriteRenderer>();
        //Set unique sorting layer
        
        //name += num.ToString();
        sr.sortingOrder = num;

        num++;
        if (num == 1000)
        {
            num = 0;
        }
        //unique instances

        lootDropper = FindObjectOfType<LootTables>();

        ChangeState(new IdleState());

      
    }
  

    // Update is called once per frame
    void Update()
    {
        if (!IsDead)
        {
            if (!TakingDamage)
            {
                currentState.Execute();
            }
            
            
            LookAtTarget();
        }
        //make sure loot drops at death
        if (IsDead && lootDropper.MyLootDropped == true)
        {
            Death();
        }

    }



    public void RemoveTarget()
    {
        Target = null;
        ChangeState(new PatrolState());
    }

    private void LookAtTarget()
    {
        if (Target != null)
        {
            float xDir = Target.transform.position.x - transform.position.x;

            if (!this.Animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
            { 
                if (xDir < 0 && facingRight || xDir > 0 && !facingRight)
                {
                    ChangeDirection();
                }
            }
        }
    }



    public void ChangeState(IEnemyState newState)
    {
        if(currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;

        currentState.Enter(this);
    }

    private void Flip(float horizontal)
    {

        if(horizontal > 0 && !facingRight || horizontal<0 && facingRight)
        {
            ChangeDirection();
        }
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        currentState.OnTriggerEnter(other);
    }

    public void Move()
    {

        if (!this.Animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack") || !this.Animator.GetCurrentAnimatorStateInfo(0).IsTag("Damage"))
        {
            Animator.SetFloat("Speed", 1);

            transform.Translate(GetDirection() * (movementSpeed * Time.deltaTime));
        }


        
    }
    public void Damage()
    {
     
        if (health >0)
        {
            Animator.SetTrigger("Damage");

            eventsystem.EnemyDamage();
        }
        else if (health <= 0)
        {
           int callcount = 0;
            if (callcount == 0 && !this.CompareTag("DeadEnemy"))
            {
                eventsystem.EnemyDeath();
                callcount++;
            }
            
        }
    }

    public void Chase()
    {
        if (!this.Animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            Animator.SetFloat("Speed", 3);

            transform.Translate(GetDirection() * ((movementSpeed*chaseMultiplier) * Time.deltaTime));
        }
        else if(Target.transform.position != transform.position){
            Animator.SetFloat("Speed", 3);

            transform.Translate(GetDirection() * ((movementSpeed * chaseMultiplier) * Time.deltaTime));
        }
    }

    public Vector2 GetDirection()
    {
        return facingRight ? Vector2.right : Vector2.left;
    }

    public void TakeHitDamage(int damage)
    {
        health -= damage;

        float current = EnemyHealth * 1;
        float max = maxHealth * 1;
       
        healthBar.fillAmount = current/max;
       
        
        Damage();
    }

    public override IEnumerator TakeDamage()
    {

        if (!IsDead)
        {
            Animator.SetTrigger("Damage");
        }
        else
        {
            Animator.SetTrigger("Death");
            eventsystem.EnemyDeath();
            yield return null; 
        }

        yield break; 
    }

    public override void Death()
    {
        Animator.SetTrigger("Death");
        eventsystem.EnemyDamage();

        this.enabled = false;
        this.tag = "DeadEnemy";
        healthBar.GetComponentInParent<Canvas>().enabled = false;
        //eventsystem.onEnemyDamage -= Damage;

    }

   public void TriggerAttackEvent()
    {
        GameEvents.current.EnemyAttacking();
    }
}
