using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBars : MonoBehaviour
{
    [SerializeField]
    float fillAmountOffset;
    private Player player;
    private int currentHealth;
    private int maxHealth;
    private Image healthFill;
    [SerializeField]
    private Image XPBar;
    private int playerLevel;
    private int playerXP;
    private int XPForNextLevel = 70;

    private void Start()
    {
        player = FindObjectOfType<Player>();
       
        healthFill = GetComponent<Image>();
    }

    private void Update()
    {
        fillOrb();
        fillXPBar();
    }

    private void fillOrb()
    {
        currentHealth = player.Health;
        maxHealth = player.MaxHealth;
        float current = currentHealth * 1f;
        float max = maxHealth * 1f;
        
        healthFill.fillAmount = (current / max)+fillAmountOffset;
    }
    void fillXPBar()
    {
        playerLevel = player.MyPlayerLevel;
        playerXP = PlayerLevel.playerXP;

        float level = playerLevel * 1f;
        float xp = playerXP * 1f;
        XPBar.fillAmount = xp/ XPForNextLevel*1f;
        if(XPBar.fillAmount >= 1)
        {
            player.MyPlayerLevel++;
            PlayerLevel.playerXP = 0;
        }
    }
}
