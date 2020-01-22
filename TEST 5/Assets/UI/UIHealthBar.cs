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
    }

    private void Update()
    {
        current = player.Health;
        max = player.MaxHealth;
        bar.fillAmount = GetComponent<UIBarFill>().fillBar(current, max);
    }

}
