using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponSocket : GearSocket
{
    
    
    [SerializeField]
    private EquipmentSlot weaponSlot1;
    [SerializeField]
    private EquipmentSlot weaponSlot2;

    Player player;

    private GearSocket gearSocket;

    [SerializeField]
    AnimationClip[] outerAnimations1;
    [SerializeField]
    AnimationClip[] innerAnimations1;

    [SerializeField]
    AnimationClip[] outerAnimations2;
    [SerializeField]
    AnimationClip[] innerAnimations2;

    [SerializeField]
    private EquippableWeapon weapon1;
    [SerializeField]
    private EquippableWeapon weapon2;

    public EquippableWeapon Weapon1 { get => weapon1;}
    public EquippableWeapon Weapon2 { get => weapon2;}

    // Start is called before the first frame update
    void Start()
    {
        
        gearSocket = GetComponent<GearSocket>();
        outerAnimations1 = new AnimationClip[gearSocket.AnimationArraySize];
        innerAnimations1 = new AnimationClip[gearSocket.AnimationArraySize];

        outerAnimations2 = new AnimationClip[gearSocket.AnimationArraySize];
        innerAnimations2 = new AnimationClip[gearSocket.AnimationArraySize];


        weapon1 = Weapon1Equipped();

        player = GetComponentInParent<Player>();
      
        if(Weapon1Equipped() == null)
        {
            UnEquipWeapon();
        }
        else if (Weapon2Equipped() == null)
        {
            UnEquipWeapon();
        }
        else
        {
            AddAnimations(weapon1, outerAnimations1, innerAnimations1);
        }

    }

    // Update is called once per frame
    void Update()
    {
       

        weapon1 = Weapon1Equipped();
        weapon2 = Weapon2Equipped();
        
        if (weapon1 != null)
            {
                AddAnimations(weapon1, outerAnimations1, innerAnimations1);
                EquipWeapon(outerAnimations1, innerAnimations1, outerAnimations2, innerAnimations2);
            }
        else if (weapon1 == null)
            {
                RemoveAnimations(outerAnimations1, innerAnimations1);
                EquipWeapon(outerAnimations1, innerAnimations1, outerAnimations2, innerAnimations2);
            }

        if (weapon2 != null)
        {
            AddAnimations(weapon2, outerAnimations2, innerAnimations2);
            EquipWeapon(outerAnimations1, innerAnimations1, outerAnimations2, innerAnimations2);
        }
        else if (weapon2 == null)
            {
                RemoveAnimations(outerAnimations2, innerAnimations2);
                EquipWeapon(outerAnimations1, innerAnimations1, outerAnimations2, innerAnimations2);
            }
        
        
        

    }

   /*
    * parameters set in base class, redundant in this class
    * private void FixedUpdate()
    {
        //Animation parameters
        MyAnimator.SetFloat("Speed", Speed);
        MyAnimator.SetBool("OnGround", OnGround);
    }*/

    private void AddAnimations(EquippableWeapon equippedWeapon, AnimationClip[] outerAnimations, AnimationClip[] innerAnimations)
    {
       
      
            for (int i = 0; i < equippedWeapon.OuterAnimationClips.Length; i++)
            {
                outerAnimations[i] = equippedWeapon.OuterAnimationClips[i];
                
            }
            for (int i = 0; i < equippedWeapon.InnerAnimationClips.Length; i++)
            {
                innerAnimations[i] = equippedWeapon.InnerAnimationClips[i];
            }
            
            return; 
       
    }

    private void RemoveAnimations(AnimationClip[] outerAnimations, AnimationClip[] innerAnimations)
    {
        for (int i = 0; i < outerAnimations.Length; i++)
        {
            outerAnimations[i] = null; 
        }
        for (int i = 0; i < innerAnimations.Length; i++)
        {
            innerAnimations[i] = null;
        }

    }

    //Animations change sides when character faces other direction
    public void EquipWeapon(AnimationClip[] outerAnimations1, AnimationClip[] innerAnimations1, AnimationClip[] outerAnimations2, AnimationClip[] innerAnimations2)
    {
        spriteRenderer.color = Color.white;
        if (!FacingRight && right)
        {
            animatorOverrideController["Idle"] = outerAnimations2[0];
            animatorOverrideController["IdleToWalk"] = outerAnimations2[1];
            animatorOverrideController["Walk"] = outerAnimations2[2];
            animatorOverrideController["WalkToIdle"] = outerAnimations2[3];
        }
        else if (FacingRight && right)
        {
            animatorOverrideController["Idle"] = outerAnimations1[0];
            animatorOverrideController["IdleToWalk"] = outerAnimations1[1];
            animatorOverrideController["Walk"] = outerAnimations1[2];
            animatorOverrideController["WalkToIdle"] = outerAnimations1[3];
        }
        else if (!FacingRight && !right)
        {
            animatorOverrideController["Idle"] = innerAnimations1[0];
            animatorOverrideController["IdleToWalk"] = innerAnimations1[1];
            animatorOverrideController["Walk"] = innerAnimations1[2];
            animatorOverrideController["WalkToIdle"] = innerAnimations1[3];
        }
        else if (FacingRight && !right)
        {
            animatorOverrideController["Idle"] = innerAnimations2[0];
            animatorOverrideController["IdleToWalk"] = innerAnimations2[1];
            animatorOverrideController["Walk"] = innerAnimations2[2];
            animatorOverrideController["WalkToIdle"] = innerAnimations2[3];
        }
    }

    public void UnEquipWeapon()
    {
        
            animatorOverrideController["Idle"] = null;
            animatorOverrideController["IdleToWalk"] = null;
            animatorOverrideController["Walk"] = null;
            animatorOverrideController["WalkToIdle"] = null;

            Color c = spriteRenderer.color;
            c.a = 0;
            spriteRenderer.color = c;
        


    }   
    private EquippableWeapon Weapon1Equipped()
    {
        if(weaponSlot1.Item != null)
        {
            //Debug.Log(weaponSlot1.Item.name + " Equipped");
            EquippableWeapon weapon = weaponSlot1.Item as EquippableWeapon;
            return weapon;
            
        }
        else
        {
            //Debug.Log("nothing Equipped");
            return null; 
        }
    }
    private EquippableWeapon Weapon2Equipped()
    {
        if (weaponSlot2.Item != null)
        {
            //Debug.Log(weaponSlot1.Item.name + " Equipped");
            EquippableWeapon weapon = weaponSlot2.Item as EquippableWeapon;
            return weapon;

        }
        else
        {
            //Debug.Log("nothing Equipped");
            return null;
        }
    }

    public override void UnEquip(AnimationClip[] animationClips)
    {
        animationClips = null; 
    }
}
