using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(UIBarFill))]
public class UIXPBar : MonoBehaviour
{
    private Image bar;
    private Player player;
    private int current;
    private int max;
    private Text xPBarText;

    void Start()
    {
        bar = GetComponent<Image>();
        player = SetPlayer.player;
        xPBarText = GetComponentInChildren<Text>();
        UpdateValues();
        UpdateXPIndicator();
    }



    void Update()
    {
        UpdateValues();
        bar.fillAmount = GetComponent<UIBarFill>().fillBar(current, max);
        if (bar.fillAmount >= 1)
        {
            PlayerLevel.Level++;
            PlayerLevel.SetXPForNextLevel();
            GameEvents.current.LevelChanged();
        }
        UpdateXPIndicator();
    }

    void UpdateXPIndicator()
    {
        UpdateValues();
        xPBarText.text = $"{current}/{max}";
    }

    void UpdateValues()
    {
        current = PlayerLevel.playerXP;
        max = PlayerLevel.xPForNextLevel;
    }
}
