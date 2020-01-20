using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevel : MonoBehaviour
{
    public static int playerXP;
    private Player player;
    private int playerLevel;


    // Start is called before the first frame update
    void Start()
    {
        playerXP = 0;
        player = FindObjectOfType<Player>();
        playerLevel = player.PlayerLevel;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
