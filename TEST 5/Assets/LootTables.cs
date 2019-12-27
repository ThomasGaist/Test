using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LootTables : MonoBehaviour
{
    #region PARAMETERS
    //main RDS table
    private RDSTable mainTable = new RDSTable();

    //sub tables
    private RDSTable genericEnemyTable = new RDSTable();
    [SerializeField]
    private List<Item> genericEnemyItems;

    //sub tables list
    private List<RDSTable> subTables;

    //Loot list
    [SerializeField]
    private List<Item> loot;

    //[SerializeField]
   // private Item[] lootArray;

    [SerializeField]
    private List<Item> tmp;

    //List of Live Enemies
    [SerializeField]
    private List<GameObject> enemies = new List<GameObject>();

    [SerializeField]
    private GameObject lootForDrop;

    private bool lootDropped = false;

    public bool MyLootDropped { get => lootDropped;}
    #endregion

    #region METHODS
    private void Awake()
    {
        if(genericEnemyItems == null)
        {
            genericEnemyItems = new List<Item>();
        }
       

        //subtable generic, int 0
        /*genericEnemyTable.AddEntry(new ConsumableItem("Mana Potion", ConsumableType.ManaPotion), 10, false, false, true);
        genericEnemyTable.AddEntry(new ConsumableItem("Health Potion", ConsumableType.HealthPotion), 20, false, true, true);*/
        genericEnemyTable.AddEntry(genericEnemyItems[0], 10, false, false, true);
        genericEnemyTable.AddEntry(genericEnemyItems[1], 20, false, true, true);

        //Remove dead enemies from enemies at start
        enemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].GetComponent<Enemy>().IsDead)
            {
                enemies.RemoveAt(i);
            }
        }
        //define subtables list
        subTables = new List<RDSTable>
        {
            genericEnemyTable,
        };

        loot = new List<Item>();
       

    }

    private void FixedUpdate()
    {
        enemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
        List<GameObject> tmpEnemies = new List<GameObject>(enemies); 
        for (int i = enemies.Count-1; i >= 0; i--)
        {
            if (enemies[i].GetComponent<Enemy>().IsDead)
            {
                //retrieving int that determines what loottable to use
                int lootTable = enemies[i].GetComponent<Enemy>().LootLevel;

                //setting amount of items to drop
                subTables[lootTable].rdsCount = 2;

                //lootArray = new Item[subTables[lootTable].rdsCount];

                if (subTables[lootTable].rdsCount > 0)
                {
                    foreach (Item item in subTables[lootTable].rdsResult)
                    {
                        loot.Add(item);  
                    }
                    /*for (int x = 0; x < subTables[lootTable].rdsCount; x++)
                    {
                        lootArray[x] = loot[i];
                    }*/
                }
                lootDropped = true;

                if(loot!= null)
                {
                    SpawnLoot(enemies[i]);
                }
                
                lootDropped = false;
                enemies.Remove(enemies[i]);
                return; 
                //loot = new List<Item>();
                //return;
            }
            
            

        }
       // enemies = new List<GameObject>(tmpEnemies);

        //tmp = new List<Item>(loot);
        loot.Clear();
        tmp = new List<Item>(loot);
    }

    #endregion

    #region SUBTABLE generic

    public void SpawnLoot(GameObject enemy)
    {
        lootForDrop.GetComponent<SpriteRenderer>().enabled = false;

        tmp = new List<Item>(loot);

        #region
        
        int count = 0;

         foreach (Item item in loot)
         {
            // Item current = item;
             //Instantiate(current);
             DroppedLoot(item, enemy);
             tmp.RemoveAt(count);

             count++;

         }
        loot = new List<Item>(tmp);
        //loot.Clear();

        return;
        
        #endregion

        #region
        /*
        for (int i = loot.Count-1; i > -1; i--)
        {
            DroppedLoot(loot[i], enemy);
            loot[i] = null;
        }
        return;
        /*

        foreach (Item item in lootArray)
        {
            DroppedLoot(item, enemy);
        }
        loot.Clear();
        
        
        //loot = new List<Item>(tmp);

        return;
        */
        #endregion

    }
    #endregion

    public void DroppedLoot(Item current, GameObject enemy)
    {
        Instantiate(current);
        lootForDrop.GetComponent<DroppedLoot>().MyDroppedLoot = current;
        lootForDrop.GetComponent<SpriteRenderer>().enabled = true;
        lootForDrop.GetComponent<DroppedLoot>().Dropped = true;
        lootForDrop.GetComponent<DroppedLoot>().Layer++;
        if (lootForDrop.GetComponent<DroppedLoot>().Layer >= 1000)
        {
            lootForDrop.GetComponent<DroppedLoot>().Layer = 0;
        }

        GameObject nextLoot = Instantiate(lootForDrop, new Vector3((enemy.transform.position.x + Random.Range(-20.0f, 20.0f)), transform.position.y, transform.position.z), Quaternion.identity);
        nextLoot.GetComponent<SpriteRenderer>().sortingOrder = lootForDrop.GetComponent<DroppedLoot>().Layer;
        nextLoot.name = nextLoot.GetComponent<DroppedLoot>().MyDroppedLoot.ItemName;
        nextLoot.SetActive(true);
        //lootForDrop.SetActive(false);
    }
}
