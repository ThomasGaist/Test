using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeState : IEnemyState
{
    private Enemy enemy;
    private float attackTimer;
    private float attackCoolDown = 2;
    private bool canAttack = true;


    public void Enter(Enemy enemy)
    {
        this.enemy = enemy; 
    }

    public void Execute()
    {
        if (enemy.Target != null && enemy.InMeleeRange)
        {
            Attack();
        }

        /*else if (enemy.Target != null && !enemy.InMeleeRange)
        {
            enemy.Chase();
        }*/
        //Debug.Log("Combat");


        if(enemy.Target != null && !enemy.InMeleeRange && !enemy.Animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            enemy.ChangeState(new ChaseState());
        }

        if (enemy.Target == null && !enemy.Animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            enemy.ChangeState(new IdleState());
        }


    }

    public void Exit()
    {
        
    }

    public void OnTriggerEnter(Collider2D other)
    {
        
    }

    private void Attack()
    {
        
        attackTimer += Time.deltaTime;

        if(attackTimer >= attackCoolDown)
        {
            canAttack = true;
            attackTimer = 0;
            
        }
        if (canAttack)
        {
            canAttack = false;
            enemy.Animator.SetTrigger("Attack");
           
            enemy.Animator.SetFloat("Speed", 0);

            if (enemy.Animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
            {
                enemy.Rigidbody2D.velocity = Vector2.zero;
            }

        }
    }

}
