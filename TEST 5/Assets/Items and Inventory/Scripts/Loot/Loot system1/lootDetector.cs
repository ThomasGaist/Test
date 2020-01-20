using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lootDetector : MonoBehaviour
{
    //[SerializeField]
    private List<DroppedLoot> lootOnGround = new List<DroppedLoot>();

    [SerializeField]
    private GameObject layoutGroup;

    [SerializeField]
    private GameObject lootName;
    [SerializeField]
    private GameObject lootNameBG;

    public KeyCode showLoot = KeyCode.Tab;
    
    [SerializeField]
    private List<string> lootNames = new List<string>();

    [SerializeField]
    private List<string> lootID = new List<string>();

    [SerializeField]
    private List<DroppedLoot> lootItems = new List<DroppedLoot>();

    GameEvents eventsystem;
    private void Start()
    {
        eventsystem = GameEvents.current;
        //eventsystem.onOverLappingLootNames += StackLootNames;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        
        if (collision.CompareTag("Loot"))
        {
            /*if (!lootID.Contains(collision.GetComponent<DroppedLoot>().MyDroppedLoot.ID))
            { */
            lootOnGround.Add(collision.GetComponent<DroppedLoot>());
            lootNames.Add(collision.GetComponent<DroppedLoot>().MyDroppedLoot.ItemName);
            lootID.Add(collision.GetComponent<DroppedLoot>().MyDroppedLoot.ID);
            lootItems.Add(collision.GetComponent<DroppedLoot>());
            
            
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
            if (collision.GetComponent<DroppedLoot>())
            {
                collision.GetComponent<DroppedLoot>().HideLootName();
            }
            lootItems.Remove(collision.GetComponent<DroppedLoot>());
        }
    }

    private void Update()
    {
        if (Input.GetKey(showLoot))
        {
            foreach (DroppedLoot item in lootItems)
            {
                item.ShowLootName();
            }
              //  StackLoot();
        }
        if (Input.GetKeyUp(showLoot))
        {
            foreach (DroppedLoot item in lootItems)
            {
                item.HideLootName();
            }
        }
    }
    /*
    private void StackLoot()
    {
        List<Rect> rects = new List<Rect>();
        foreach (DroppedLoot item in lootItems)
        {
            rects.Add(item.lootNameRect);
        }
        for (int i = 0; i <rects.Count; i++)
        {
            if (rects[i].Overlaps(rects[i + 1]))
            {
                Debug.Log("OVERLAP");
                Instantiate(layoutGroup,lootItems[i].GetComponentInChildren<Image>().transform);
            }
        }
        rects.Clear();
        
    }*/

   /* void StackLootNames()
    {
        foreach (DroppedLoot loot in lootItems)
        {
            if(loot.GetComponentInChildren<LootNameStacking>().dirty == true)
            {
                GameObject lootForStack = Instantiate(lootNameBG, transform);
                GameObject lootForStackBG = Instantiate(lootName, transform);
                lootForStackBG.GetComponentInChildren<Text>().text = loot.lootNameText.text;
            }
        }
        
    }*/
}
