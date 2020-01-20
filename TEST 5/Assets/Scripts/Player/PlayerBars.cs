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

    private void Start()
    {
        player = FindObjectOfType<Player>();
       
        healthFill = GetComponent<Image>();
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
}
