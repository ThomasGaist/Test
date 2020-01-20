using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootNameStacking : MonoBehaviour
{
    [SerializeField]
    private GameObject lootUIStacker;
    public bool dirty = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("LootName") && !dirty)
        {
           // Instantiate(lootUIStacker, transform);

            GameEvents.current.OverLappingLootNames();
            dirty = true;
        }
    }
    private void OnEnable()
    {
        Vector2 boxSize = new Vector2(GetComponent<RectTransform>().rect.width, GetComponent<RectTransform>().rect.height);
        GetComponent<BoxCollider2D>().size = boxSize;

    }
}
