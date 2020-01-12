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

    #endregion

}
