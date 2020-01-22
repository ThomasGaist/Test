using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevel : MonoBehaviour
{
    public static int Level;
    public static int playerXP;
    public static int xPForNextLevel;
    private Player player;



    private void Awake()
    {
        xPForNextLevel = 50;
        Level = 1;
    }
    // Start is called before the first frame update
    void Start()
    {
        playerXP = 0;
        player = SetPlayer.player;


    }

    public static void SetXPForNextLevel()
    {
        //ARITHMETIC PROGRESSION
        xPForNextLevel = 25 * Level*(1+Level);
    }
}