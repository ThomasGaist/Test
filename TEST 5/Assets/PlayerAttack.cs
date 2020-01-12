using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    private Player player;
    GameEvents eventsystem;
    
    void Start()
    {
        player = Player.Instance;
        eventsystem = GameEvents.current;
        eventsystem.onPlayerAttack += Attack;
    }

 

    private void Attack()
    {
        Debug.Log("I'm attack! RAAAAWAR!!");
    }
}
