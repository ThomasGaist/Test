using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootOnGroundLayout : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Loot"))
        {
            // other.GetComponent<DroppedLoot>().lootNameText.GetComponentInParent<GameObject>().transform.SetParent(transform, false);
            //transform.SetAsFirstSibling();


        }
    }
   
}
