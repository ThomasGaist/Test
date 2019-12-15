using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : IEnemyState
{

    private Enemy enemy;

    public void Enter(Enemy enemy)
    {
        this.enemy = enemy; 
    }

    public void Execute()
    {
     
        enemy.Chase();

        if (enemy.Target != null && enemy.InMeleeRange)
        {
            enemy.ChangeState(new MeleeState());
        }

        else if(enemy.Target == null)
        {
            enemy.ChangeState(new IdleState());
        }

        


        Debug.Log("Chasing");
    }

    public void Exit()
    {


    }

    public void OnTriggerEnter(Collider2D other)
    {

    }


}
