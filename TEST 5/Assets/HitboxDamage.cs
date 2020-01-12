using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxDamage : MonoBehaviour
{
    private Enemy enemy;
    private Player player;
    private Collider2D collider;

    private void Start()
    {
        GameEvents.current.onPlayerAttack += TakeDamage;
        player = Player.Instance;
        enemy = GetComponentInParent<Enemy>();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerHurtBox"))
        {
            collider = collision; 
            TakeDamage();
          
        }
    }
    public void TakeDamage()
    {
        enemy.EnemyHealth -= collider.GetComponent<PlayerAttack>().AttackDamageTotal;
        enemy.Damage();
    }

    private void OnDisable()
    {
        GameEvents.current.onPlayerAttack -= TakeDamage;
    }
}
