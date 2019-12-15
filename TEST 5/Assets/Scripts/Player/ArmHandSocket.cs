using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmHandSocket : GearSocket
{
    GearSocket gearSocket;

    [SerializeField]
    AnimationClip[] outerNoWeapon;
    [SerializeField]
    AnimationClip[] innerNoWeapon;

    [SerializeField]
    AnimationClip[] outerOneHanded;
    [SerializeField]
    AnimationClip[] innerOneHanded;

    /*[SerializeField]
    private EquippableItem equipment;
    [SerializeField]
    private EquipmentSlot equipmentSlot;*/

    [SerializeField]
    private WeaponSocket weaponSocket1;
    [SerializeField]
    private WeaponSocket weaponSocket2;

    [SerializeField]
    private bool oneHandedEquipped1;

    [SerializeField]
    private bool oneHandedEquipped2;

    


    // Start is called before the first frame update
    void Start()
    {
        WeaponsEquipped();
        gearSocket = GetComponent<GearSocket>();

        if (oneHandedEquipped1 == true)
        {
            EquipWeapon1(outerNoWeapon, innerNoWeapon, outerOneHanded, innerOneHanded);
        }
        else if (oneHandedEquipped1 == false)
        {
            UnEquipWeapon();
        }

        /* outerNoWeapon = new AnimationClip[gearSocket.AnimationArraySize];
         innerNoWeapon = new AnimationClip[gearSocket.AnimationArraySize];

         innerOneHanded = new AnimationClip[gearSocket.AnimationArraySize];
         outerOneHanded = new AnimationClip[gearSocket.AnimationArraySize];*/
    }

    // Update is called once per frame
    void Update()
    {
        WeaponsEquipped();
        MyAnimator.SetFloat("Speed", gearSocket.Speed);
        MyAnimator.SetBool("OnGround", gearSocket.OnGround);

        if (oneHandedEquipped1 == true)
        {
            EquipWeapon1(outerNoWeapon, innerNoWeapon, outerOneHanded, innerOneHanded);
        }

        else if (oneHandedEquipped2 == true)
        {
            EquipWeapon2(outerNoWeapon, innerNoWeapon, outerOneHanded, innerOneHanded);
        }
        else if (oneHandedEquipped1 == false && oneHandedEquipped2 == false)
        {
            UnEquipWeapon();
        }

        if (oneHandedEquipped1 == true && oneHandedEquipped2 == true)
        {
            EquipWeapons(outerNoWeapon, innerNoWeapon, outerOneHanded, innerOneHanded);
        }
        
        
        
      

    }

    private void WeaponsEquipped()
    {
        if (weaponSocket1.Weapon1 != null)
        {
            oneHandedEquipped1 = true;
           
        }
        else
        {
            oneHandedEquipped1 = false;
            
        }
        if (weaponSocket1.Weapon2 != null)
        {
            oneHandedEquipped2 = true;
        }
        else
        {
            oneHandedEquipped2 = false;
        }
    }
    public void EquipWeapon1(AnimationClip[] outerUnEquipped, AnimationClip[] innerUnEquipped, AnimationClip[] outerOneHanded, AnimationClip[] innerOneHanded)
    {
        spriteRenderer.color = Color.white;
        if (gearSocket.FacingRight && right)
        {
            animatorOverrideController["Idle"] = outerOneHanded[0];
            animatorOverrideController["IdleToWalk"] = outerOneHanded[1];
            animatorOverrideController["Walk"] = outerOneHanded[2];
            animatorOverrideController["WalkToIdle"] = outerOneHanded[3];
        }
        else if (!gearSocket.FacingRight && right)
        {
            animatorOverrideController["Idle"] = outerUnEquipped[0];
            animatorOverrideController["IdleToWalk"] = outerUnEquipped[1];
            animatorOverrideController["Walk"] = outerUnEquipped[2];
            animatorOverrideController["WalkToIdle"] = outerUnEquipped[3];
        }
        if (gearSocket.FacingRight && !right)
        {
            animatorOverrideController["Idle"] = innerUnEquipped[0];
            animatorOverrideController["IdleToWalk"] = innerUnEquipped[1];
            animatorOverrideController["Walk"] = innerUnEquipped[2];
            animatorOverrideController["WalkToIdle"] = innerUnEquipped[3];
        }
        else if (!gearSocket.FacingRight && !right)
        {
            animatorOverrideController["Idle"] = innerOneHanded[0];
            animatorOverrideController["IdleToWalk"] = innerOneHanded[1];
            animatorOverrideController["Walk"] = innerOneHanded[2];
            animatorOverrideController["WalkToIdle"] = innerOneHanded[3];
        }



    }

    public void EquipWeapon2(AnimationClip[] outerUnEquipped, AnimationClip[] innerUnEquipped, AnimationClip[] outerOneHanded, AnimationClip[] innerOneHanded)
    {
        spriteRenderer.color = Color.white;
        if (!gearSocket.FacingRight && right)
        {
            animatorOverrideController["Idle"] = outerOneHanded[0];
            animatorOverrideController["IdleToWalk"] = outerOneHanded[1];
            animatorOverrideController["Walk"] = outerOneHanded[2];
            animatorOverrideController["WalkToIdle"] = outerOneHanded[3];
        }
        else if (gearSocket.FacingRight && right)
        {
            animatorOverrideController["Idle"] = outerUnEquipped[0];
            animatorOverrideController["IdleToWalk"] = outerUnEquipped[1];
            animatorOverrideController["Walk"] = outerUnEquipped[2];
            animatorOverrideController["WalkToIdle"] = outerUnEquipped[3];
        }
        if (!gearSocket.FacingRight && !right)
        {
            animatorOverrideController["Idle"] = innerUnEquipped[0];
            animatorOverrideController["IdleToWalk"] = innerUnEquipped[1];
            animatorOverrideController["Walk"] = innerUnEquipped[2];
            animatorOverrideController["WalkToIdle"] = innerUnEquipped[3];
        }
        else if (gearSocket.FacingRight && !right)
        {
            animatorOverrideController["Idle"] = innerOneHanded[0];
            animatorOverrideController["IdleToWalk"] = innerOneHanded[1];
            animatorOverrideController["Walk"] = innerOneHanded[2];
            animatorOverrideController["WalkToIdle"] = innerOneHanded[3];
        }



    }

    public void EquipWeapons(AnimationClip[] outerUnEquipped, AnimationClip[] innerUnEquipped, AnimationClip[] outerOneHanded, AnimationClip[] innerOneHanded)
    {
        spriteRenderer.color = Color.white;
        if (!gearSocket.FacingRight && right)
        {
            animatorOverrideController["Idle"] = outerOneHanded[0];
            animatorOverrideController["IdleToWalk"] = outerOneHanded[1];
            animatorOverrideController["Walk"] = outerOneHanded[2];
            animatorOverrideController["WalkToIdle"] = outerOneHanded[3];
        }
        else if (gearSocket.FacingRight && right)
        {
            animatorOverrideController["Idle"] = outerOneHanded[0];
            animatorOverrideController["IdleToWalk"] = outerOneHanded[1];
            animatorOverrideController["Walk"] = outerOneHanded[2];
            animatorOverrideController["WalkToIdle"] = outerOneHanded[3];
        }
        if (!gearSocket.FacingRight && !right)
        {
            animatorOverrideController["Idle"] = innerOneHanded[0];
            animatorOverrideController["IdleToWalk"] = innerOneHanded[1];
            animatorOverrideController["Walk"] = innerOneHanded[2];
            animatorOverrideController["WalkToIdle"] = innerOneHanded[3];
        }
        else if (gearSocket.FacingRight && !right)
        {
            animatorOverrideController["Idle"] = innerOneHanded[0];
            animatorOverrideController["IdleToWalk"] = innerOneHanded[1];
            animatorOverrideController["Walk"] = innerOneHanded[2];
            animatorOverrideController["WalkToIdle"] = innerOneHanded[3];
        }





    }
    private void UnEquipWeapon()
    {
        animatorOverrideController["Idle"] = gearSocket.DefaultClips[0];
        animatorOverrideController["IdleToWalk"] = gearSocket.DefaultClips[1];
        animatorOverrideController["Walk"] = gearSocket.DefaultClips[2];
        animatorOverrideController["WalkToIdle"] = gearSocket.DefaultClips[3];
    }
}
  
