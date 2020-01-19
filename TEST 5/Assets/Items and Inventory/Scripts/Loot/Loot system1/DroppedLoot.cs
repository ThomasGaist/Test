using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DroppedLoot: MonoBehaviour
{
    [SerializeField]
    private Item droppedLoot;

    public Text lootNameText;
    
   
    public Item MyDroppedLoot { get => droppedLoot; set => droppedLoot = value; }
    public bool Dropped { get => dropped; set => dropped = value; }
    public int Layer { get => layer; set => layer = value; }

    private SpriteRenderer sr;
    private BoxCollider2D collider;
    private Rigidbody2D rb;
    private CircleCollider2D trigger;

    [SerializeField]
    private string ID;

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

    //TIMING WHEN LOOT IS ON FLOOR
    bool onFloorTimer = false;

    private void Awake()
    {


        if (inventory == null)
        {
            inventory = FindObjectOfType<Inventory>();
        }

        ID = MyDroppedLoot.ID;
    }

  

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        trigger = GetComponent<CircleCollider2D>();
        dropped = false;

        Physics2D.IgnoreLayerCollision(13, 13);
        lootNameText = GetComponentInChildren<Text>();
        lootNameText.text = MyDroppedLoot.ItemName;
        lootNameText.enabled = false;
        lootNameText.GetComponentInParent<Image>().enabled = false;
        //Physics2D.IgnoreLayerCollision(13, 11);

        StartCoroutine(DropTimer());
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
            if (!inventory.IsFull())
            {
                inventory.AddItem(Instantiate(MyDroppedLoot));
                Destroy(gameObject);
            }
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
    private void OnMouseDown()
    {
        if (!inventory.IsFull())
        {
            inventory.AddItem(Instantiate(MyDroppedLoot));
            Destroy(gameObject);
        }
    }
    private void OnMouseOver()
    {
        ShowLootName();
        
    }
    private void OnMouseExit()
    {
        HideLootName();
    }

    public void ShowLootName()
    {
        lootNameText.enabled = true;
        lootNameText.GetComponentInParent<Image>().enabled = true;
    }
    public void HideLootName()
    {
        lootNameText.enabled = false;
        lootNameText.GetComponentInParent<Image>().enabled = false;
    }

    //COROUTINE FOR TIMING DELAY BEFORE TOOLTIP AND PICKUP

    IEnumerator DropTimer()
    {
        yield return new WaitForSecondsRealtime(1f);
        onFloorTimer = true;
        StopCoroutine(DropTimer());
    }
}
