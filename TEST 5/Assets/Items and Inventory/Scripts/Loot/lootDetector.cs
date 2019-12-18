using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lootDetector : MonoBehaviour
{
    //[SerializeField]
    private List<DroppedLoot> lootOnGround = new List<DroppedLoot>();

    
    [SerializeField]
    private List<string> lootNames = new List<string>();

    [SerializeField]
    private List<string> lootID = new List<string>();

    private void OnTriggerEnter2D(Collider2D collision)
    {

        
        if (collision.CompareTag("Loot"))
        {
            /*if (!lootID.Contains(collision.GetComponent<DroppedLoot>().MyDroppedLoot.ID))
            { */
            lootOnGround.Add(collision.GetComponent<DroppedLoot>());
            lootNames.Add(collision.GetComponent<DroppedLoot>().MyDroppedLoot.ItemName);
            lootID.Add(collision.GetComponent<DroppedLoot>().MyDroppedLoot.ID);
            
            
        }
        else
        {
            return;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Loot"))
        {
            lootOnGround.Remove(collision.GetComponent<DroppedLoot>());
            lootNames.Remove(collision.GetComponent<DroppedLoot>().MyDroppedLoot.ItemName);
            lootID.Remove(collision.GetComponent<DroppedLoot>().MyDroppedLoot.ID);
        }
    }
}
