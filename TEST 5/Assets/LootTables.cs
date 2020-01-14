using System;
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

    //Loot array
    [SerializeField]
    private Item[] loot;


    //List of Live Enemies
    [SerializeField]
    private List<GameObject> enemies = new List<GameObject>();


    [SerializeField]
    private GameObject lootForDrop;

    private bool lootDropped = false;

    private bool enemyDeath;

    private Player player;
    
    public bool MyLootDropped { get => lootDropped;}

    #endregion

    #region EVENTS
    private GameEvents eventsystem; 

    #endregion

    #region METHODS
    private void Awake()
    {
        eventsystem = GameEvents.current;
        eventsystem.onEnemyDeath += GenerateLoot;

        if (genericEnemyItems == null)
        {
            genericEnemyItems = new List<Item>();
        }

        //find player
        player = FindObjectOfType <Player> ();

        //items in arrays
        genericEnemyTable.AddEntry(genericEnemyItems[0], 10, false, false, true);
        genericEnemyTable.AddEntry(genericEnemyItems[1], 20, false, false, true);
        genericEnemyTable.AddEntry(genericEnemyItems[2], 80, false, false, true);
        genericEnemyTable.AddEntry(genericEnemyItems[3], 20, false, false, true);
        //Null entry for probability for no drop
        //genericEnemyTable.AddEntry(new RDSNullValue(80), 80, false, false, true);

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



    }

    public void GenerateLoot()
    {
        //Debug.Log("loot generated!");
        enemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
        List<GameObject> tmpEnemies = new List<GameObject>(enemies);
        for (int i = enemies.Count - 1; i >= 0; i--)
        {
            if (enemies[i].GetComponent<Enemy>().IsDead)
            {
                //retrieving int that determines what loottable to use
                int lootTable = enemies[i].GetComponent<Enemy>().LootLevel;

                //Set max and min itemdropvalue according to enemy level, player level, luck etc. 

                //setting amount of items to drop, Random at the moment
                //subTables[lootTable].rdsCount = UnityEngine.Random.Range(1,3);
                subTables[lootTable].rdsCount = 2;




                if (subTables[lootTable].rdsCount > 0)
                {
                    List<IRDSObject> loot2;
                    loot2 = new List<IRDSObject>(subTables[lootTable].rdsResult);

                    List<Item> loot3 = new List<Item>();
                    for (int x = 0; x < subTables[lootTable].rdsCount; x++)
                    {
                        if (loot2[x] != null)
                        {
                            Item item = loot2[x] as Item;
                            loot3.Add(item); //= loot2[x] as Item; 
                        }

                    }


                    loot = new Item[loot3.Count];
                    for (int x = 0; x < loot3.Count; x++)
                    {

                        loot[x] = loot3[x];

                    }
                }
                lootDropped = true;

                if (loot != null)
                {
                    SpawnLoot(enemies[i]);
                    return;
                }
                lootDropped = false;

                /*
                //remove enemy from list
                enemies.Remove(enemies[i]);
                */
                break;

            }

        }
    }


    private void FixedUpdate()
    {
       

        
    }

    #endregion

    #region SUBTABLE generic

    public void SpawnLoot(GameObject enemy)
    {
        lootForDrop.GetComponent<SpriteRenderer>().enabled = false;


        #region
        foreach (Item item in loot)
         {
            //Debug.Log(item);
            DroppedLoot(item, enemy);
         }

        for (int i = 0; i < loot.Length; i++)
        {
            loot[i] = null; 
        }
        return;
        
        #endregion


    }
    #endregion

    public void DroppedLoot(Item current, GameObject enemy)
    {

        //GOLD DROP AMOUNT CALCULATION
        if(current is Gold)
        {
            ((Gold)current).GoldAmount = GoldDrop(enemy.GetComponent<Enemy>().EnemyLevel, player.MyPlayerLevel);
        }
        Instantiate(current);
        lootForDrop.GetComponent<DroppedLoot>().MyDroppedLoot = current;
        lootForDrop.GetComponent<SpriteRenderer>().enabled = true;
        lootForDrop.GetComponent<DroppedLoot>().Dropped = true;
        lootForDrop.GetComponent<DroppedLoot>().Layer++;
        if (lootForDrop.GetComponent<DroppedLoot>().Layer >= 1000)
        {
            lootForDrop.GetComponent<DroppedLoot>().Layer = 0;
        }

        GameObject nextLoot = Instantiate(lootForDrop, new Vector3((enemy.transform.position.x + UnityEngine.Random.Range(-20.0f, 20.0f)), transform.position.y, transform.position.z), Quaternion.identity);
        nextLoot.GetComponent<SpriteRenderer>().sortingOrder = lootForDrop.GetComponent<DroppedLoot>().Layer;
        nextLoot.name = nextLoot.GetComponent<DroppedLoot>().MyDroppedLoot.ItemName;
        nextLoot.SetActive(true);
       
    }



    public int GoldDrop(int enemyLevel, int playerLevel)
    {

        //Maybe add area level, player luck etc. 
        int gold = Mathf.RoundToInt((10 * enemyLevel + playerLevel)* UnityEngine.Random.Range(1.0f, 1.5f));
        return gold;

    }

    private void OnDestroy()
    {
        eventsystem.onEnemyDeath -= GenerateLoot;
    }
}

