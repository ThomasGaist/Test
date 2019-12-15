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
    public int Layer { get => layer; set => layer = value; }

    private SpriteRenderer sr;
    private BoxCollider2D collider;
    private Rigidbody2D rb;
    private CircleCollider2D trigger;

    [SerializeField]
    private float triggerRadius = 1.5f;

    private bool dropped;
    [SerializeField]
    float thrust = 1.0f;
    [SerializeField]
    private int layer = -1;

    [SerializeField] Inventory inventory;
    [SerializeField] KeyCode itemPickUp = KeyCode.E;

    [SerializeField]
    private bool isInRange;
  

    private void OnValidate()
    {
        if (inventory == null)
        {
            inventory = FindObjectOfType<Inventory>();
        }
    }

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        trigger = GetComponent<CircleCollider2D>();
        dropped = false;
       
        Physics2D.IgnoreLayerCollision(13, 13);
        //Physics2D.IgnoreLayerCollision(13, 11);

    }
    private void Update()
    {  
        sr.sprite = MyDroppedLoot.Icon;
        collider.offset = new Vector2(0, 0);
        collider.size = new Vector3(sr.bounds.size.x / transform.lossyScale.x, sr.bounds.size.y / transform.lossyScale.y, sr.bounds.size.z / transform.lossyScale.z);
        trigger.radius = triggerRadius;
        sr.sortingOrder = Layer;


        //Implement code for when inventory is full

        if (isInRange && Input.GetKeyDown(itemPickUp))
        {
            
                inventory.AddItem(Instantiate(MyDroppedLoot));
                Destroy(gameObject);
            
        }

    }
    private void FixedUpdate()
    {
    if (dropped == true)
        {
        
          
            rb.AddForce(transform.up * thrust);
            dropped = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isInRange = true;
            
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isInRange = false;
          
        }

    }
}
