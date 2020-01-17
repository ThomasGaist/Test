
using UnityEngine;
using UnityEngine.UI;
using TGaist.CharacterStats;

public class InventoryManager : MonoBehaviour
{

	public CharacterStat Strength;
	public CharacterStat Agility;
	public CharacterStat Intelligence;
	public CharacterStat Vitality;

	[SerializeField] Inventory inventory;
    [SerializeField] EquipmentPanel equipmentPanel;
	[SerializeField] StatPanel statPanel;
    [SerializeField] ItemToolTip itemToolTip;
    [SerializeField] Image draggableItem;
    [SerializeField] DropItemArea dropItemArea;
    [SerializeField] GameObject lootForDrop;

    private Player player;

    private ItemSlot draggedSlot;

    //loot system
    public static InventoryManager Instance { get; set; }
   

    private void Start()
    {
        if(itemToolTip == null)
        {
            itemToolTip = FindObjectOfType<ItemToolTip>();
        }
        //statPanel.UpdateStatValues();
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;

        player = FindObjectOfType<Player>();

		statPanel.SetStats(Strength, Agility, Intelligence, Vitality);
		statPanel.UpdateStatValues();

        //Setup Events:
        //Right Click

        inventory.OnRightClickEvent += Equip;
        equipmentPanel.OnRightClickEvent += Unequip;
        //pointer enter
        inventory.OnPointerEnterEvent += ShowToolTip;
        equipmentPanel.OnPointerEnterEvent += ShowToolTip;
        //PointerExit
        inventory.OnPointerExitEvent += HideToolTip;
        equipmentPanel.OnPointerExitEvent += HideToolTip;
        // Begin Drag
        inventory.OnBeginDragEvent += BeginDrag;
        equipmentPanel.OnBeginDragEvent += BeginDrag;
        // End Drag
        inventory.OnEndDragEvent += EndDrag;
        equipmentPanel.OnEndDragEvent += EndDrag;
        // Drag
        inventory.OnDragEvent += Drag;
        equipmentPanel.OnDragEvent += Drag;
        // Drop
        inventory.OnDropEvent += Drop;
        equipmentPanel.OnDropEvent += Drop;

        dropItemArea.OnDropEvent += DropItemOutsideUI;

    }
    private void Update()
    {
        statPanel.UpdateStatValues();
    }

    private void Equip(ItemSlot itemSlot)
    {

        EquippableItem equippableItem = itemSlot.Item as EquippableItem;
        if(equippableItem != null)
        {
            Equip(equippableItem);
            GameEvents.current.ItemEquipped();
        }
    }
    private void Unequip(ItemSlot itemSlot)
    {

        EquippableItem equippableItem = itemSlot.Item as EquippableItem;
        if (equippableItem != null)
        {
            Unequip(equippableItem);
            GameEvents.current.ItemEquipped();
        }
    }

    private void ShowToolTip(ItemSlot itemSlot)
    {
        EquippableItem equippableItem = itemSlot.Item as EquippableItem;
        if(equippableItem != null)
        {
            itemToolTip.ShowToolTip(equippableItem);
        }
    }

    private void HideToolTip(ItemSlot itemSlot)
    {
        itemToolTip.HideToolTip();
    }

    private void BeginDrag(ItemSlot itemSlot)
    {
        if(itemSlot.Item != null)
        {
            draggedSlot = itemSlot;
            draggableItem.sprite = itemSlot.Item.Icon;
            draggableItem.transform.position = Input.mousePosition;
            draggableItem.enabled = true;
        }

    }
    private void EndDrag(ItemSlot itemSlot)
    {
        draggedSlot = null;
        draggableItem.enabled = false;
    }
    private void Drag(ItemSlot itemSlot)
    {
        if (draggableItem.enabled)
        {
            draggableItem.transform.position = Input.mousePosition;
        }
       
    }
    private void Drop(ItemSlot dropItemSlot)
    {
      if (draggedSlot == null) return;

        if (dropItemSlot.CanReceiveItem(draggedSlot.Item) && draggedSlot.CanReceiveItem(dropItemSlot.Item))
        {

            EquippableItem dragItem = draggedSlot.Item as EquippableItem;
            EquippableItem dropItem = dropItemSlot.Item as EquippableItem;

            if(draggedSlot is EquipmentSlot)
            {
                if (dragItem != null) dragItem.Unequip(this); 
                if (dropItem != null) dropItem.Equip(this); 

            }
            if(dropItemSlot is EquipmentSlot)
            {
                if (dragItem != null) dragItem.Equip(this); 
                if (dropItem != null) dropItem.Unequip(this);
            }
            statPanel.UpdateStatValues();

            Item draggedItem = draggedSlot.Item;
            draggedSlot.Item = dropItemSlot.Item;
            dropItemSlot.Item = draggedItem;
            GameEvents.current.ItemEquipped();
        }

       // 

    }
    void DropItemOutsideUI()
    {

        if (draggedSlot == null) return;

         Instantiate(draggedSlot.Item);
         lootForDrop.GetComponent<DroppedLoot>().MyDroppedLoot = draggedSlot.Item;
         lootForDrop.GetComponent<SpriteRenderer>().enabled = true;
         lootForDrop.GetComponent<DroppedLoot>().Dropped = true;
         lootForDrop.GetComponent<DroppedLoot>().Layer++;
          if (lootForDrop.GetComponent<DroppedLoot>().Layer >= 1000)
          {
                lootForDrop.GetComponent<DroppedLoot>().Layer = 0;
          }

          GameObject nextLoot = Instantiate(lootForDrop, new Vector3((player.transform.position.x + UnityEngine.Random.Range(-20.0f, 20.0f)), player.transform.position.y, player.transform.position.z), Quaternion.identity);
          nextLoot.GetComponent<SpriteRenderer>().sortingOrder = lootForDrop.GetComponent<DroppedLoot>().Layer;
          nextLoot.name = nextLoot.GetComponent<DroppedLoot>().MyDroppedLoot.ItemName;

            //Destroy(draggedSlot.Item);
          draggedSlot.Item = null;
        if(draggedSlot is EquipmentSlot)
        {
            GameEvents.current.ItemUnEquipped();
        }
            nextLoot.SetActive(true);
        
    }


    public void Equip (EquippableItem item)
    {
        if (inventory.RemoveItem(item))
        {
            EquippableItem previousItem;
            if(equipmentPanel.AddItem(item, out previousItem))
            {
                if(previousItem != null)
                {
                    inventory.AddItem(previousItem);
                    previousItem.Unequip(this);
                    statPanel.UpdateStatValues();
                   
                }
                item.Equip(this);
                statPanel.UpdateStatValues();
            }
            else
            {
                inventory.AddItem(item);
            }
        }
    }
    public void Unequip(EquippableItem item)
    {
        if(!inventory.IsFull() && equipmentPanel.RemoveItem(item))
        {
            item.Unequip(this);
            statPanel.UpdateStatValues();
            inventory.AddItem(item);
        }
    }
}
