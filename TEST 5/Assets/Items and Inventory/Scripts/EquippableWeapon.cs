using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    OneHanded,
    Twohanded,
}
public enum OneHandedType
{
    Axe,
    Sword,
    Bow,
    Mace,
    shield,

}
public enum TwoHandedType
{
    Longbow,
    BroadSword,

}
[CreateAssetMenu]
public class EquippableWeapon : EquippableItem
{
    [SerializeField]
    private WeaponType weaponType;
    [SerializeField]
    private OneHandedType oneHandedWeapon;
    [SerializeField]
    private TwoHandedType twoHandedWeapon;

    [SerializeField]
    private AnimationClip[] outerAnimationClips;

    [SerializeField]
    private AnimationClip[] innerAnimationClips;

    public WeaponType WeaponType { get => weaponType;}
    public OneHandedType OneHandedWeapon { get => oneHandedWeapon;}
    public TwoHandedType TwoHandedWeapon { get => twoHandedWeapon;}
    public AnimationClip[] OuterAnimationClips { get => outerAnimationClips;}
    public AnimationClip[] InnerAnimationClips { get => innerAnimationClips;}
}
