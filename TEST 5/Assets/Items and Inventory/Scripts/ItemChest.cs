using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemChest : MonoBehaviour
{
    //Script til Items der skal Addes til inventory


    [SerializeField] Item item;
    [SerializeField] Inventory inventory;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Color emptyColor;
    [SerializeField] KeyCode itemPickUp = KeyCode.E;

    private bool isInRange;
    private bool isEmpty;

    private void OnValidate()
    {
        if(inventory == null)
        {
            inventory = FindObjectOfType<Inventory>();
        }
        if(spriteRenderer == null)
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }
        spriteRenderer.sprite = item.Icon;
        spriteRenderer.enabled = false; 
        
    }

    private void Update()
    {
        if (isInRange && Input.GetKeyDown(itemPickUp))
        {
            if (!isEmpty)
            {

                inventory.AddItem(Instantiate(item));
                isEmpty = true;
                spriteRenderer.color = emptyColor;

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isInRange = true;
            spriteRenderer.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isInRange = false;
            spriteRenderer.enabled = false;
        }
        
    }
}
