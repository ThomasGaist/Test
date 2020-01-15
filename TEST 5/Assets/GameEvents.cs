using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;
    private void Awake()
    {
        if(current == null)
        {
            current = this;
        }
    }


    #region ENEMY EVENT
    public event Action onEnemyDeath;

    //event for loot generation at enemy death
    public void EnemyDeath()
    {
        if(onEnemyDeath!= null)
        {
            onEnemyDeath();
        }
    }

    public event Action onEnemyDamage;
    public void EnemyDamage()
    {
        if (onEnemyDamage != null)
        {
            onEnemyDamage();
        }
    }

    public event Action onEnemyInstantiation;
    public void EnemyInstantiated()
    {
        if(onEnemyInstantiation != null)
        {
            onEnemyInstantiation();
        }
    }

    #endregion

    #region CHARACTER ANIMATIONS

    public event Action onItemEquipped;

    public void ItemEquipped()
    {
        if(onItemEquipped != null)
        {
            onItemEquipped();
        }
    }
    public event Action onItemUnEquipped;

    public void ItemUnEquipped()
    {
        if (onItemUnEquipped != null)
        {
            onItemUnEquipped();
        }
    }

    #endregion

    #region PLAYER EVENTS
    public event Action onPlayerAttack;

    public void PlayerAttack()
    {
        if(onPlayerAttack!= null)
        {
            onPlayerAttack();
        }
    }

    #endregion

    
}
