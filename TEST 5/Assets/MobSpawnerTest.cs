using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MobSpawnerTest : MonoBehaviour
{

    private KeyCode mobSpawnKey = KeyCode.Dollar;
    private Transform mobSpawnPos;

    [SerializeField]
    private GameObject mob;
    // Start is called before the first frame update
    void Start()
    {
        mobSpawnPos = this.transform;
       

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(mobSpawnKey))
        {
            Instantiate(mob,mobSpawnPos.position, Quaternion.identity);
            GameEvents.current.EnemyInstantiated();
        }
    }
}
