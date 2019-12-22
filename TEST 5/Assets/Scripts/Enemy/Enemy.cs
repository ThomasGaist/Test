using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    //Loot
    [SerializeField]
    private LootTable thisLoot;
    public LootTable ThisLoot { get => thisLoot; }
    [SerializeField]
    private GameObject lootForDrop;

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


    public override void Start()
    {
        base.Start();
        Player.Instance.Dead += new DeadEventHandler(RemoveTarget);
        sr = GetComponent<SpriteRenderer>();
        lootDropper = FindObjectOfType<LootTables>();

        ChangeState(new IdleState());
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            health = 0;
            //Death();
        }
        if (!IsDead)
        {
            if (!TakingDamage)
            {
                currentState.Execute();
            }
            
            
            LookAtTarget();
        }
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

        if (!this.Animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            Animator.SetFloat("Speed", 1);

            transform.Translate(GetDirection() * (movementSpeed * Time.deltaTime));
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

    public override IEnumerator TakeDamage()
    {
        health -= 10;

        if (!IsDead)
        {
            Animator.SetTrigger("Damage");
        }
        else
        {
            Animator.SetTrigger("Death");
                yield return null; 
        }
    }

    public override void Death()
    {
        // MakeLoot();

        //Will only use this until good death animation. 
        Destroy(gameObject);
        Destroy(transform.parent.gameObject);

        //transparent on Death()
      
    }

/*
    //Loot method
    private void MakeLoot()
    {
        if(ThisLoot != null)
        {
            lootForDrop.GetComponent<SpriteRenderer>().enabled = false;
            
            Item current = ThisLoot.lootDrop();
            lootForDrop.GetComponent<DroppedLoot>().MyDroppedLoot = current;
            if(current != null)
            {
                lootForDrop.GetComponent<SpriteRenderer>().enabled = true;
                lootForDrop.GetComponent<DroppedLoot>().Dropped = true;
                lootForDrop.GetComponent<DroppedLoot>().Layer++;
                if(lootForDrop.GetComponent<DroppedLoot>().Layer >= 1000)
                {
                    lootForDrop.GetComponent<DroppedLoot>().Layer = 0;
                }
                GameObject nextLoot = Instantiate(lootForDrop, new Vector3((transform.position.x + Random.Range(-20.0f,20.0f)), transform.position.y, transform.position.z), Quaternion.identity);
                nextLoot.GetComponent<SpriteRenderer>().sortingOrder = lootForDrop.GetComponent<DroppedLoot>().Layer;
                nextLoot.active = true;

            }
        }
    }*/
    
}
