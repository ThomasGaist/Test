using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Loot
{
    public Item lootItem;
    public float lootChance;

    //loot table should generate new Items instead of using prefabs. 

}

[CreateAssetMenu]
public class LootTable : ScriptableObject
{
    public Loot[] loots;

    
    public Item lootDrop()
    {
        float cumProb = 0;
        float currentProb = Random.Range(0, 100);
        for (int i = 0; i < loots.Length; i++)
        {
            cumProb += loots[i].lootChance;
            if(currentProb <= cumProb)
            {
                
                return loots[i].lootItem;
            }
        }
        return null;
    }
}
