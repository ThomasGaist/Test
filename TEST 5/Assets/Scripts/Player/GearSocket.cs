using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearSocket : MonoBehaviour
{
    [SerializeField]
    private bool facingRight;

    [SerializeField]
    protected bool right;

    [SerializeField]
    private EquipmentType[] equipmentTypes; 
    [SerializeField]
    private AnimationClip[] defaultClips;

    private int animationArraySize = 4;

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
    public int AnimationArraySize { get => animationArraySize;}
    public AnimationClip[] DefaultClips { get => defaultClips;}

    private void Awake()
    {
      

        spriteRenderer = GetComponent<SpriteRenderer>();

        parentAnimator = GetComponentInParent<Animator>();

        MyAnimator = GetComponent<Animator>();

        animatorOverrideController = new AnimatorOverrideController(MyAnimator.runtimeAnimatorController);

        MyAnimator.runtimeAnimatorController = animatorOverrideController;

        player = GetComponentInParent<Player>();

        
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UnEquip(defaultClips);
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

    public void UnEquip(AnimationClip[] animationClips)
    {
        if (animationClips != null)
        {

            animatorOverrideController["Idle"] = animationClips[0];
            animatorOverrideController["IdleToWalk"] = animationClips[1];
            animatorOverrideController["Walk"] = animationClips[2];
            animatorOverrideController["WalkToIdle"] = animationClips[3];
        }
        else {

            animatorOverrideController["Idle"] = null;
            animatorOverrideController["IdleToWalk"] = null;
            animatorOverrideController["Walk"] = null;
            animatorOverrideController["WalkToIdle"] = null;
        }
        
       
    }

    

   
}
