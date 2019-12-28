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

    
    public bool MyLootDropped { get => lootDropped;}
    #endregion

    #region METHODS
    private void Awake()
    {
        if (genericEnemyItems == null)
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

                //extract enemy transform for DroppedLoot method
                Transform enemyPos = enemies[i].transform;

                
                if (subTables[lootTable].rdsCount > 0)
                {
                    List<IRDSObject> loot2;
                    loot2 = new List<IRDSObject>(subTables[lootTable].rdsResult);
                    loot = new Item[subTables[lootTable].rdsCount];
                    for (int x = 0; x < 2; x++)
                    {
                        loot[x]=loot2[x] as Item;
                        
                    }
                }
                lootDropped = true;

                if(loot!= null)
                {
                    SpawnLoot(enemyPos);
                    return;
                }
                lootDropped = false;

                //remove enemy from list
                enemies.Remove(enemies[i]);

                break; 
                //loot = new List<Item>();
                //return;
            }
            
            

        }
       // enemies = new List<GameObject>(tmpEnemies);

        //tmp = new List<Item>(loot);
       // loot.Clear();
        //tmp = new List<Item>(loot);
    }

    #endregion

    #region SUBTABLE generic

    public void SpawnLoot(Transform enemypos)
    {
        lootForDrop.GetComponent<SpriteRenderer>().enabled = false;


        #region
        foreach (Item item in loot)
         {
            //Debug.Log(item);
            DroppedLoot(item, enemypos);
         }

        for (int i = 0; i < loot.Length; i++)
        {
            loot[i] = null; 
        }
        return;
        
        #endregion


    }
    #endregion

    public void DroppedLoot(Item current, Transform enemypos)
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

        GameObject nextLoot = Instantiate(lootForDrop, new Vector3((enemypos.position.x + Random.Range(-20.0f, 20.0f)), transform.position.y, transform.position.z), Quaternion.identity);
        nextLoot.GetComponent<SpriteRenderer>().sortingOrder = lootForDrop.GetComponent<DroppedLoot>().Layer;
        nextLoot.name = nextLoot.GetComponent<DroppedLoot>().MyDroppedLoot.ItemName;
        nextLoot.SetActive(true);
        //lootForDrop.SetActive(false);
    }
}

