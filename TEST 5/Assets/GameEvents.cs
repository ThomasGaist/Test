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

    public void EnemyDeath()
    {
        if(onEnemyDeath!= null)
        {
            onEnemyDeath();
        }
    }

    #endregion

    #region CHARACTER ANIMATIONS

    #endregion

}
