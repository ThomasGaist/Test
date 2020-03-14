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
    public event Action onEnemyAttack;
    public void EnemyAttacking()
    {
        if (onEnemyAttack != null)
        {
            onEnemyAttack();
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

    public event Action onFlipPlayer;

    public void PlayerFlipped()
    {
        if(onFlipPlayer != null)
        {
            onFlipPlayer();
        }
    }

    //ANIMATION EVENTS



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

    public event Action onPlayerDamage;
    public void PlayerDamage()
    {
        if (onPlayerDamage != null)
        {
            onPlayerDamage();
        }
    }

    public event Action onPlayerFootstep;
    public void PlayerFootstep()
    {
        if(onPlayerFootstep != null)
        {
            onPlayerFootstep();
        }
    }

    #endregion

    #region UI EVENTS
    public event Action onShowLoot;
    public void ShowLootOnGround()
    {
        if(onShowLoot!= null)
        {
            onShowLoot();
        }
    }
    public event Action onOverLappingLootNames;
    public void OverLappingLootNames()
    {
        if(onOverLappingLootNames != null)
        {
            onOverLappingLootNames();
        }
    }

    #endregion
    #region PLAYER LEVELLING
    public event Action onLevelChanged;
    public void LevelChanged()
    {
        if(onLevelChanged!= null)
        {
            onLevelChanged();
        }
    }

    public event Action onXPChanged;
    public void XPChanged()
    {
        if(onXPChanged != null)
        {
            onXPChanged();
        }
    }

    #endregion

}
