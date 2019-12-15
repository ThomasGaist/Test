using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DroppedLoot: MonoBehaviour
{
    [SerializeField]
    private Item droppedLoot;
   
    public Item MyDroppedLoot { get => droppedLoot; set => droppedLoot = value; }
    public bool Dropped { get => dropped; set => dropped = value; }

    private SpriteRenderer sr;
    private BoxCollider2D collider;
    private Rigidbody2D rb;
    private bool dropped;
    [SerializeField]
    float thrust = 1.0f;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        dropped = false;
       
    }
    private void Update()
    {  
        sr.sprite = MyDroppedLoot.Icon;
        collider.offset = new Vector2(0, 0);
        collider.size = new Vector3(sr.bounds.size.x / transform.lossyScale.x, sr.bounds.size.y / transform.lossyScale.y, sr.bounds.size.z / transform.lossyScale.z);

       
    }
    private void FixedUpdate()
    {
    if (dropped == true)
        {
           
            rb.AddForce(transform.up * thrust);
            dropped = false;
        }
    }
}
