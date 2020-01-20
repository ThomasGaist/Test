using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    GameEvents eventsystem;
    private BoxCollider2D hurtBox;
    private int attackDamage;
    private Player player;
    //COOLDOWN

    //SETUP
    [SerializeField]
    private Transform attackPosition;
    [SerializeField]
    private float attackRange;
    [SerializeField]
    private LayerMask attackLayerMask;
    [SerializeField]
    private Enemy enemy;


    public int AttackDamageTotal { get => attackDamage; set => attackDamage = value; }

    void Start()
    {
        player = FindObjectOfType<Player>();
        eventsystem = GameEvents.current;
        eventsystem.onEnemyAttack += Attack;
        //eventsystem.onPlayerAttack += Attack;

    }

    private void Awake()
    {
       
    }

    private void Attack()
    { 
        MeleeAttack();

    }
   
    private void MeleeAttack()
    {
        Collider2D damageTarget = Physics2D.OverlapCircle(attackPosition.position, attackRange, attackLayerMask);
        AttackDamageTotal = CalculateAttack();
        if(damageTarget != null)
        {
        player.PlayerDamage(AttackDamageTotal);
        }
    }
    private int CalculateAttack()
    {
        //Melee or Ranged? player buffs or resistens?
        return Mathf.RoundToInt(enemy.MeleeAttackDamage);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPosition.position, attackRange);
    }
}


