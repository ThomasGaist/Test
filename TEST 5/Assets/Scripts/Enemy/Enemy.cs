using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    //Loot
   
    private IEnemyState currentState;

    public GameObject Target { get; set; }

    public float movementSpeed;

    public float chaseMultiplier = 1.5f;

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
       
        Destroy(gameObject);
    }


    
}
