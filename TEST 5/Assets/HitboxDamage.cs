using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxDamage : MonoBehaviour
{
    private Enemy enemy;
    private Player player;

    private void Awake()
    {
        player = Player.Instance;
        enemy = GetComponentInParent<Enemy>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerHurtBox"))
        {
            enemy.EnemyHealth -= Mathf.RoundToInt(player.MyAttackDamage);
            enemy.Damage();
        }
    }
}
