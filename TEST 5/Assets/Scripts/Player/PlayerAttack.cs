using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    private Player player;
    GameEvents eventsystem;
    private BoxCollider2D hurtBox;
    private int attackDamage;

    //COOLDOWN
    
    private float timeBtwAttack;
    [SerializeField]
    private float startTimeBtwAttack;


    //SETUP
    [SerializeField]
    private Transform attackPosition;
    [SerializeField]
    private float attackRange;
    [SerializeField]
    private LayerMask enemyLayerMask;


    public int AttackDamageTotal { get =>attackDamage; set => attackDamage = value; }

    void Start()
    {
        player = Player.Instance;
        eventsystem = GameEvents.current;
        eventsystem.onPlayerAttack += Attack;
        
    }

    private void Awake()
    {
        hurtBox = GetComponent<BoxCollider2D>();
        hurtBox.enabled = false;
    }

    private void Attack()
    {
        if (timeBtwAttack <= 0)
        {
            MeleeAttack();

            {
                timeBtwAttack = startTimeBtwAttack;
            }
        }

        else
        {
            return;
        }
    }
    private void FixedUpdate()
    {
        timeBtwAttack -= Time.deltaTime;
    }

    private void Update()
    {
       /* if (Input.GetKeyDown(player.attackKey))
        {
            Attack();
        }*/
    }

    private void MeleeAttack()
    {
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPosition.position, attackRange, enemyLayerMask);
        AttackDamageTotal = CalculateAttack();
        foreach (Collider2D enemy in enemiesToDamage)
        {
            enemy.GetComponent<Enemy>().TakeHitDamage(AttackDamageTotal);
            //PlayerLevel.playerXP += enemy.GetComponent<Enemy>().EnemyXPDrop;

        }
        foreach (Collider2D enemy in enemiesToDamage)
        {
            
            if (enemy.GetComponent<Enemy>().EnemyHealth <= 0 && !enemy.CompareTag("DeadEnemy"))
            {
                //ADD XP OF DEAD ENEMIES
                PlayerLevel.playerXP += enemy.GetComponent<Enemy>().EnemyXPDrop;
                eventsystem.XPChanged();
            }

        }
    }
    private int CalculateAttack()
    {
        return Mathf.RoundToInt(player.MyAttackDamage);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPosition.position, attackRange);
    }
}

    /*
    private IEnumerator AttackTime()
    {
        hurtBox.enabled = true;
        AttackDamageTotal = CalculateAttack();
        yield return new WaitForSeconds(0.05f);
        hurtBox.enabled = false;
        player.attack = false;

        yield break;
    }*/

