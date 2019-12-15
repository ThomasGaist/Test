using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DroppedLoot: MonoBehaviour
{
    [SerializeField]
    private Item droppedLoot;
   
    public Item MyDroppedLoot { get => droppedLoot; set => droppedLoot = value; }

    private SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        
    }
    private void Update()
    {
        sr.sprite = MyDroppedLoot.Icon;
    }
}
