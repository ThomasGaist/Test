using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IEnemyState
{
    private Enemy enemy;
    private float patrolTimer;
    private float patrolDuration = 3;

    public void Enter(Enemy enemy)
    {
        this.enemy = enemy; 
    }

    public void Execute()
    {
        //to make sure enemy is not moving when taking damage
        if (!enemy.Animator.GetCurrentAnimatorStateInfo(0).IsTag("Damage"))
        {
            Patrol();

            enemy.Move();

            // Debug.Log("Patrolling");

            if (enemy.Target != null)
            {
                enemy.ChangeState(new ChaseState());
            }
        }
    }

    public void Exit()
    {
        
    }

    public void OnTriggerEnter(Collider2D other)
    {
        if(other.tag == "PatrolPoint")
        {
            enemy.ChangeDirection();
        }
    }

    private void Patrol()
    {
    

        patrolTimer += Time.deltaTime;

        if (patrolTimer >= patrolDuration)
        {
            enemy.ChangeState(new IdleState());
        }
    }

}
