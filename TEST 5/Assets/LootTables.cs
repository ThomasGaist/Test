using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootTables : MonoBehaviour
{
    #region PARAMETERS
    //main RDS table
    private RDSTable mainTable = new RDSTable();

    //sub tables
    private RDSTable genericEnemyTable = new RDSTable();

    //sub tables list
    private List<RDSTable> subTables;

    //Loot list
    [SerializeField]
    private List<Item> loot = new List<Item>();
    private List<Item> tmp = new List<Item>();

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
        //subtable generic, int 0
        genericEnemyTable.AddEntry(new ConsumableItem("Mana Potion", ConsumableType.ManaPotion), 10, false, false, true);
        genericEnemyTable.AddEntry(new ConsumableItem("Health Potion", ConsumableType.HealthPotion), 20, false, true, true);

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
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].GetComponent<Enemy>().IsDead)
            {

                int lootTable = enemies[i].GetComponent<Enemy>().LootLevel;

                subTables[lootTable].rdsCount = 2;

                foreach (Item item in subTables[lootTable].rdsResult)
                {
                    loot.Add(item);  
                }
                lootDropped = true;

                if(loot!= null)
                {
                    SpawnLoot(enemies[i]);
                }
                
                lootDropped = false;
                tmpEnemies.RemoveAt(i);
                
                return;
            }

        }
        enemies = new List<GameObject>(tmpEnemies);
        loot = new List<Item>(tmp);
    }

    #endregion

    #region SUBTABLE generic

    public void SpawnLoot(GameObject enemy)
    {
        lootForDrop.GetComponent<SpriteRenderer>().enabled = false;

        tmp = new List<Item>(loot);

        int count = 0;
        foreach (Item item in loot)
        {
            Item current = item;
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
            
            tmp.RemoveAt(count);
            
            count++;

        }
       
        return; 

        #endregion
    }
}
