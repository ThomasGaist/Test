using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int level;
    public int xp;
    public int maxHealth;

    public PlayerData(Player player)
    {
        level = PlayerLevel.Level;
        xp = PlayerLevel.playerXP;
        maxHealth = player.MaxHealth;
    }
}
