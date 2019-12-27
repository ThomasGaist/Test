using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ConsumableType
{
    ManaPotion,
    HealthPotion,
    Gold, 
}

[CreateAssetMenu]
public class ConsumableItem : Item
{
    public ConsumableType consumableType;

    private LootSpriteLibrary library = FindObjectOfType<LootSpriteLibrary>().GetComponent<LootSpriteLibrary>();


    //CONSTRUCTOR
   public ConsumableItem(string name, ConsumableType type)
    {
        this.ItemName = name;
        this.consumableType = type;

        if (consumableType == ConsumableType.ManaPotion)
        {
            this.Icon = library.consumableSprites[1];
        }
        else if (consumableType == ConsumableType.HealthPotion)
        {
            this.Icon = library.consumableSprites[0];
        }
    }

}
