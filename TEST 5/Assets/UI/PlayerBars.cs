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
    private Text levelIndicator;
    [SerializeField]
    private Text xPIndicator;
    private int playerLevel;
    private int playerXP;
    private GameEvents eventsystem;
    private void Start()
    {
        eventsystem = GameEvents.current;
        player = SetPlayer.player;
        healthFill = GetComponent<Image>();
        levelIndicator = XPBar.GetComponentInChildren<Text>();
       
        eventsystem.onXPChanged += fillXPBar;
        eventsystem.onLevelChanged += ChangePlayerLevel;


        UpdateXPIndicator();
    }

    private void Update()
    {
        fillOrb();
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
      
        playerXP = PlayerLevel.playerXP;
        float xp = playerXP * 1f;
        XPBar.fillAmount = xp/ PlayerLevel.xPForNextLevel*1f;
        if (XPBar.fillAmount >= 1)
        {
           
           

        }
        UpdateXPIndicator();
    }

    void ChangePlayerLevel()
    {
        levelIndicator.text = $"{PlayerLevel.Level}";
    }

    void UpdateXPIndicator()
    {
        xPIndicator.text = $"{playerXP}/{PlayerLevel.xPForNextLevel}";
    }
}
