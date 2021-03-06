﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IEnemyState
{

    private Enemy enemy;

    private float idleTimer;

    private float idleDuration = 3; 

    public void Enter(Enemy enemy)
    {
        this.enemy = enemy; 
    }

    public void Execute()
    {
        if (!enemy.Animator.GetCurrentAnimatorStateInfo(0).IsTag("Damage"))
        {
            //Debug.Log("I'm idling");
            Idle();
            if (enemy.Target != null)
            {
                enemy.ChangeState(new PatrolState());
            }
        }
    }

    public void Exit()
    {
       
    }

    public void OnTriggerEnter(Collider2D other)
    {
      
    }

    private void Idle()
    {
        enemy.Animator.SetFloat("Speed", 0);

        idleTimer += Time.deltaTime; 

        if (idleTimer >= idleDuration)
        {
            enemy.ChangeState(new PatrolState());
        }
    }

}
