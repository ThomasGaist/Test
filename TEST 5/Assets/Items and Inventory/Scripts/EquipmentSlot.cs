using System.Collections.Generic;
using UnityEngine;

public class EquipmentSlot : ItemSlot
{
    public List<EquipmentType> EquipmentType;
    [SerializeField]
    private string slotName;

    private void OnValidate()
    {
        gameObject.name = slotName + " slot";
    }
    protected override void Awake()
    {
        base.Awake();
        gameObject.name = slotName + " slot";
    }

    public override bool CanReceiveItem(Item item)
    {
       if(item == null)

            return true;
        
        EquippableItem equippableItem = item as EquippableItem;
        return equippableItem != null && EquipmentType.Contains(equippableItem.EquipmentType);
    }

    
}
