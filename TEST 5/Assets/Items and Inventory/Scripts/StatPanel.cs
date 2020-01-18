using UnityEngine;
using TGaist.CharacterStats;

public class StatPanel : MonoBehaviour
{
    [SerializeField] StatDisplay[] statDisplays;
    [SerializeField] string[] statNames;

    private CharacterStat[] stats;
    private GameEvents eventsystem;
    private void Awake() //alternatively OnValidate if needed
    {
        statDisplays = GetComponentsInChildren<StatDisplay>();
        UpdateStatNames();
        //UpdateStatValues();
    }
    private void Start()
    {
        eventsystem = GameEvents.current;

        eventsystem.onItemEquipped += UpdateStatValues;
        eventsystem.onItemUnEquipped += UpdateStatValues;
    }

    public void SetStats(params CharacterStat[] charstats)
    {
        stats = charstats;

        if(stats.Length > statDisplays.Length)
        {
            Debug.LogError("Not Enough Stat Displays!");
            return;
        }

        for (int i = 0; i < statDisplays.Length; i++)
        {
            statDisplays[i].gameObject.SetActive(i < stats.Length);

            if (i < stats.Length)
            {
                statDisplays[i].Stat = stats[i];
            }
        }
    }
    public void UpdateStatValues()
    {
        for (int i = 0; i < stats.Length; i++)
        {
            statDisplays[i].UpdateStatValue();
        }
    }
    public void UpdateStatNames()
    {
        for (int i = 0; i < statNames.Length; i++)
        {
            statDisplays[i].Name = statNames[i];
        }
    }
}
