using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInstances : MonoBehaviour
{
    static int num;

    private void Start()
    {
        GameEvents.current.onEnemyInstantiation += SetSortingOrder;
    }

    private void SetSortingOrder()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();

        foreach (var enemy in enemies)
        {
            enemy.GetComponent<SpriteRenderer>().sortingOrder = num;
            if(num == 1000)
            {
                num = 0;
            }
        }
    }
}
