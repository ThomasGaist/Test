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
    private int playerLevel;
    private int playerXP;
    private int XPForNextLevel = 70;
    private GameEvents eventsystem;
    private void Start()
    {
        eventsystem = GameEvents.current;
        player = FindObjectOfType<Player>();
        healthFill = GetComponent<Image>();
        levelIndicator = XPBar.GetComponentInChildren<Text>();
        eventsystem.onXPChanged += fillXPBar;
        eventsystem.onLevelChanged += ChangePlayerLevel;
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
        XPBar.fillAmount = xp/ XPForNextLevel*1f;
        if(XPBar.fillAmount >= 1)
        {
            player.MyPlayerLevel++;
            eventsystem.LevelChanged();
            PlayerLevel.playerXP = 0;
        }
    }

    void ChangePlayerLevel()
    {
        levelIndicator.text = $"{player.MyPlayerLevel}";
    }
}
