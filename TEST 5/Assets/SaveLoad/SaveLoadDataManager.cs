using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadDataManager : MonoBehaviour
{
    private Player player;
    public static SaveLoadDataManager instance;

    private void Start()
    {
        if(instance == null)
        {
            instance = this; 
        }
        else
        {
            Destroy(this);
        }

        player = SetPlayer.player;
    }

    public void SaveGame()
    {

        ES2.Save(player.MaxHealth, "max health");
        ES2.Save(PlayerLevel.Level, "player level");
        ES2.Save(PlayerLevel.playerXP, "player xp");
        ES2.Save(player.transform.position, "player position");
      
    }
    public void LoadGame()
    {
        player.MaxHealth = ES2.Load<int>("max health");
        PlayerLevel.Level = ES2.Load<int>("player level");
        PlayerLevel.playerXP = ES2.Load<int>("player xp");
        player.transform.position = ES2.Load<Vector3>("player position");

    }
}
