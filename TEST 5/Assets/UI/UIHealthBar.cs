using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

    [RequireComponent(typeof(UIBarFill))]
public class UIHealthBar : MonoBehaviour
{
    private Image bar;
    private Player player;
    private int current;
    private int max;
    
        // Start is called before the first frame update
    void Start()
    {
        bar = GetComponent<Image>();
        player = SetPlayer.player;
        UpdateValues();
    }

    private void Update()
    {
        UpdateValues();
        bar.fillAmount = GetComponent<UIBarFill>().fillBar(current, max);
    }

    void UpdateValues()
    {
        current = player.Health;
        max = player.MaxHealth;
    }

}
