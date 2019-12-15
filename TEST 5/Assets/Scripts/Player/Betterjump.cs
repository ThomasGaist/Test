using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Betterjump : PhysicsObject
{

    //jump physics variables

    public float fallMultiplier = 2.5f;
    public float lowJumpMultipler = 2f;

    public int playerJumpPower = 100;
    public KeyCode jumpKey = KeyCode.Space;

    //Variables for resetting jump using OverlapCircle
    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    private int extraJumps;
    public int extraJumpsvalue;

    //Jump improved feel
    Rigidbody2D rb;

    //animation variables
    public Animator animator;
    public float jumpTimer = 2f;
    private float jumpKeyTime;


    void Awake()
    {
        //extra jumps defined
        extraJumps = extraJumpsvalue;

        rb = GetComponent<Rigidbody2D>();

    }
    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

    }

    void Update()
    {
       /* if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(jumpKey))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultipler - 1) * Time.deltaTime;
        }*/

        if (isGrounded == true)
        {
            extraJumps = extraJumpsvalue;
        }
        if (Input.GetKeyDown(jumpKey) && extraJumps > 0)
        {
            Jump();
            extraJumps--;
        }
        else if (Input.GetKeyDown(jumpKey) && extraJumps == 0 && isGrounded == true)
        {
            Jump();
        }

        //set animator bool
        if (Input.GetKeyDown(jumpKey))
        {
            animator.SetTrigger("JumpTrigger");

        }
       /* else
        { 
           animator.SetBool("JumpKeyDown", false);
       }
      /*
       if (Input.GetKeyUp(jumpKey))
       {
           animator.SetBool("JumpKeyDown", false);
       }*/
            if (extraJumps > 0)
            {
                animator.SetBool("JumpsLeft", true);
            }
            else
                animator.SetBool("JumpsLeft", false);

            if (isGrounded == false)
            {
                animator.SetBool("OnGround", false);
            }
            else
            {
                animator.SetBool("OnGround", true);
            }


            //Bool for interrupting,not necessary if triggerjump
            /*
            if (Input.GetKey(jumpKey))
            {
                jumpKeyTime = Time.time;
            }
            if (Input.GetKey(jumpKey) && jumpTimer-jumpKeyTime < 0)
            {
                animator.SetBool("IgnoreJump", true);
            }
            if (jumpTimer-jumpKeyTime >0)
            {
                animator.SetBool("IgnoreJump", false);
            } 
            */


            void Jump() =>
                //Jumping Code
                GetComponent<Rigidbody2D>().AddForce(Vector2.up * playerJumpPower);
        }
    }
