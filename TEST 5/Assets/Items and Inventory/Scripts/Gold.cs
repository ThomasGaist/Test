using UnityEngine;

[CreateAssetMenu]
public class Gold : Item
{
    private ConsumableType type = ConsumableType.Gold;
    [SerializeField]
    private int goldAmount;
   
    public int GoldAmount { get => goldAmount; set => goldAmount = value; }
}
