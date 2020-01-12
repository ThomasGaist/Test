using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SocketType
{
    Default,
    ArmHand,
    Weapon,

};

public class GearSocket : MonoBehaviour
{
    [SerializeField]
    private SocketType socketType;

    [SerializeField]
    private bool facingRight;

    [SerializeField]
    protected bool right;

    [SerializeField]
    private EquipmentType[] equipmentTypes;
    [SerializeField]
    private AnimationClip[] defaultClips;

    private int animationArraySize;

    public Animator MyAnimator { get; set; }

    protected SpriteRenderer spriteRenderer;

    private Animator parentAnimator;

    protected AnimatorOverrideController animatorOverrideController;

    private Player player;

    private float speed;

    private bool onGround;

    public float Speed { get => speed; set => speed = value; }
    public bool OnGround { get => onGround; set => onGround = value; }
    public bool FacingRight { get => facingRight; set => facingRight = value; }

    [SerializeField]
    public int AnimationArraySize { get => animationArraySize; }
    public AnimationClip[] DefaultClips { get => defaultClips; }

    #region ArmHand Variables

    [SerializeField]
    AnimationClip[] outerNoWeapon;
    [SerializeField]
    AnimationClip[] innerNoWeapon;

    [SerializeField]
    AnimationClip[] outerOneHanded;
    [SerializeField]
    AnimationClip[] innerOneHanded;

    [SerializeField]
    private GearSocket weaponSocket1;
    [SerializeField]
    private GearSocket weaponSocket2;

    [SerializeField]
    private bool oneHandedEquipped1;

    [SerializeField]
    private bool oneHandedEquipped2;

    #endregion

    #region WeaponSlot Variables
    [SerializeField]
    private EquipmentSlot weaponSlot1;
    [SerializeField]
    private EquipmentSlot weaponSlot2;

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

    public EquippableWeapon Weapon1 { get => weapon1; }
    public EquippableWeapon Weapon2 { get => weapon2; }


    #endregion

    #region EVENTS
    private GameEvents eventsystem;

    #endregion

    public virtual void Awake()
    {
        //EVENTS
        eventsystem = GameEvents.current;
        eventsystem.onItemEquipped += EquipItems;


        //amount of overall character animations
        animationArraySize = 4;

        spriteRenderer = GetComponent<SpriteRenderer>();

        parentAnimator = GetComponentInParent<Animator>();

        MyAnimator = GetComponent<Animator>();

        animatorOverrideController = new AnimatorOverrideController(MyAnimator.runtimeAnimatorController);

        MyAnimator.runtimeAnimatorController = animatorOverrideController;

        player = GetComponentInParent<Player>();

        //find weaponsockets
        weaponSocket1 = GameObject.Find("Right Hand Weapon").GetComponent<GearSocket>();
        weaponSocket2 = GameObject.Find("Left Hand Weapon").GetComponent<GearSocket>();

        if (socketType == SocketType.Default)
        {
            UnEquip(DefaultClips);
        }
        else if (socketType == SocketType.ArmHand)
        {
            WeaponsEquipped();

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
                UnEquip(defaultClips);
            }

            if (oneHandedEquipped1 == true && oneHandedEquipped2 == true)
            {
                EquipWeapons(outerNoWeapon, innerNoWeapon, outerOneHanded, innerOneHanded);
            }
        }

        else if (socketType == SocketType.Weapon)
        {
            outerAnimations1 = new AnimationClip[AnimationArraySize];
            innerAnimations1 = new AnimationClip[AnimationArraySize];

            outerAnimations2 = new AnimationClip[AnimationArraySize];
            innerAnimations2 = new AnimationClip[AnimationArraySize];


            weapon1 = Weapon1Equipped();
            weapon2 = Weapon2Equipped();

            player = GetComponentInParent<Player>();

            if (Weapon1Equipped() == null)
            {
                UnEquipWeapon();
            }
            else if (Weapon2Equipped() == null)
            {
                UnEquipWeapon();
            }
            else if (Weapon1Equipped() != null)
            {
                AddAnimations(weapon1, outerAnimations1, innerAnimations1);
            }
            else if (Weapon2Equipped() != null)
            {
                AddAnimations(weapon2, outerAnimations2, innerAnimations2);
            }
        }
    }


    void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    { }

    private void EquipItems()
    {

        if (socketType == SocketType.Default)
        {
            UnEquip(DefaultClips);
        }
        else if (socketType == SocketType.ArmHand)
        {
            WeaponsEquipped();

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
                UnEquip(defaultClips);
            }

            if (oneHandedEquipped1 == true && oneHandedEquipped2 == true)
            {
                EquipWeapons(outerNoWeapon, innerNoWeapon, outerOneHanded, innerOneHanded);
            }
        }
        else if(socketType == SocketType.Weapon)
        {
            HandleWeaponSocket();
        }
    }
    void FixedUpdate()
    {
       //Handle all animation parameters in fixedupdate

        MyAnimator.SetFloat("Speed", Speed);
        MyAnimator.SetBool("OnGround", OnGround);
       
    }

    public void Equip(AnimationClip[] animations)
    {
        animatorOverrideController["Idle"] = animations[0];
        animatorOverrideController["IdleToWalk"] = animations[1];
        animatorOverrideController["Walk"] = animations[2];
        animatorOverrideController["WalkToIdle"] = animations[3];
    }

    public virtual void UnEquip(AnimationClip[] animationClips)
    {
        if (animationClips != null)
        {

            animatorOverrideController["Idle"] = animationClips[0];
            animatorOverrideController["IdleToWalk"] = animationClips[1];
            animatorOverrideController["Walk"] = animationClips[2];
            animatorOverrideController["WalkToIdle"] = animationClips[3];
        }
        else if(socketType == SocketType.Weapon)
        {

            animatorOverrideController["Idle"] = null;
            animatorOverrideController["IdleToWalk"] = null;
            animatorOverrideController["Walk"] = null;
            animatorOverrideController["WalkToIdle"] = null;
        }
        else
        {

            animatorOverrideController["Idle"] = null;
            animatorOverrideController["IdleToWalk"] = null;
            animatorOverrideController["Walk"] = null;
            animatorOverrideController["WalkToIdle"] = null;
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
        if (FacingRight && right)
        {
            animatorOverrideController["Idle"] = outerOneHanded[0];
            animatorOverrideController["IdleToWalk"] = outerOneHanded[1];
            animatorOverrideController["Walk"] = outerOneHanded[2];
            animatorOverrideController["WalkToIdle"] = outerOneHanded[3];
        }
        else if (!FacingRight && right)
        {
            animatorOverrideController["Idle"] = outerUnEquipped[0];
            animatorOverrideController["IdleToWalk"] = outerUnEquipped[1];
            animatorOverrideController["Walk"] = outerUnEquipped[2];
            animatorOverrideController["WalkToIdle"] = outerUnEquipped[3];
        }
        if (FacingRight && !right)
        {
            animatorOverrideController["Idle"] = innerUnEquipped[0];
            animatorOverrideController["IdleToWalk"] = innerUnEquipped[1];
            animatorOverrideController["Walk"] = innerUnEquipped[2];
            animatorOverrideController["WalkToIdle"] = innerUnEquipped[3];
        }
        else if (!FacingRight && !right)
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
        if (!FacingRight && right)
        {
            animatorOverrideController["Idle"] = outerOneHanded[0];
            animatorOverrideController["IdleToWalk"] = outerOneHanded[1];
            animatorOverrideController["Walk"] = outerOneHanded[2];
            animatorOverrideController["WalkToIdle"] = outerOneHanded[3];
        }
        else if (FacingRight && right)
        {
            animatorOverrideController["Idle"] = outerUnEquipped[0];
            animatorOverrideController["IdleToWalk"] = outerUnEquipped[1];
            animatorOverrideController["Walk"] = outerUnEquipped[2];
            animatorOverrideController["WalkToIdle"] = outerUnEquipped[3];
        }
        if (!FacingRight && !right)
        {
            animatorOverrideController["Idle"] = innerUnEquipped[0];
            animatorOverrideController["IdleToWalk"] = innerUnEquipped[1];
            animatorOverrideController["Walk"] = innerUnEquipped[2];
            animatorOverrideController["WalkToIdle"] = innerUnEquipped[3];
        }
        else if (FacingRight && !right)
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
        if (!FacingRight && right)
        {
            animatorOverrideController["Idle"] = outerOneHanded[0];
            animatorOverrideController["IdleToWalk"] = outerOneHanded[1];
            animatorOverrideController["Walk"] = outerOneHanded[2];
            animatorOverrideController["WalkToIdle"] = outerOneHanded[3];
        }
        else if (FacingRight && right)
        {
            animatorOverrideController["Idle"] = outerOneHanded[0];
            animatorOverrideController["IdleToWalk"] = outerOneHanded[1];
            animatorOverrideController["Walk"] = outerOneHanded[2];
            animatorOverrideController["WalkToIdle"] = outerOneHanded[3];
        }
        if (!FacingRight && !right)
        {
            animatorOverrideController["Idle"] = innerOneHanded[0];
            animatorOverrideController["IdleToWalk"] = innerOneHanded[1];
            animatorOverrideController["Walk"] = innerOneHanded[2];
            animatorOverrideController["WalkToIdle"] = innerOneHanded[3];
        }
        else if (FacingRight && !right)
        {
            animatorOverrideController["Idle"] = innerOneHanded[0];
            animatorOverrideController["IdleToWalk"] = innerOneHanded[1];
            animatorOverrideController["Walk"] = innerOneHanded[2];
            animatorOverrideController["WalkToIdle"] = innerOneHanded[3];
        }

    }
    #region WEAPONSOCKET METHODS
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
        if (weaponSlot1.Item != null)
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

    public void HandleWeaponSocket()
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
    #endregion
    private void OnDestroy()
    {
        eventsystem.onItemEquipped -= EquipItems;
    }
}