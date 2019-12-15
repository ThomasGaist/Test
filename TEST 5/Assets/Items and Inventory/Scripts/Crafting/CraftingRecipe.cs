using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ItemAmount
{
    public Item item;
    [Range(1,99)]
    public int Amount; 
}

[CreateAssetMenu]
public class CraftingRecipe: ScriptableObject
{
    public List<ItemAmount> Materials;
    public List<ItemAmount> Results;

    public bool CanCraft(IItemContainer itemContainer)
    {
        foreach (ItemAmount itemAmount in Materials)
        {
            if(itemContainer.ItemCount(itemAmount.item.ID)< itemAmount.Amount)
            {
                return false; 
            }
        }
        return true; 
    }

    public void Craft(Inventory itemContainer)
    {
        foreach (ItemAmount itemAmount in Materials)
        {
            for (int i = 0; i < itemAmount.Amount; i++)
            {
                Item oldItem = itemContainer.RemoveItem(itemAmount.item.ID);
                Destroy(oldItem);
            }
            
        }
        foreach (ItemAmount itemAmount in Results)
        {
            for (int i = 0; i < itemAmount.Amount; i++)
            {
                itemContainer.AddItem(Instantiate(itemAmount.item));
            }

        }
    }
}
