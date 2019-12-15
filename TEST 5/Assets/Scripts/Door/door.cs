using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door : MonoBehaviour
{

    public Animator animator;
    //protected bool openDoor = false;
    protected KeyCode actionKey = KeyCode.E;
    private bool open = false;
    protected bool atDoor = false;
    public GameObject target1;

    public bool locked;


    private void Awake()
    {
        animator = GetComponent<Animator>();

        //GameObject.Find("doorDisable").GetComponent<DisableDoor>().disabled;
    }



    // Update is called once per frame
    void Update()
    {
       if(Input.GetKeyDown(actionKey) && locked == true)
        {
            StartCoroutine(openLocked());
        }

        if (locked == true)
        {
            animator.SetBool("locked", true);
        }

        if (locked == false)
        {
            animator.SetBool("locked", false);
        }

        if (Input.GetKeyDown(actionKey) && atDoor == true && open == false && target1.GetComponent<DisableDoor>().disabled == false && locked == false)
        {
            animator.SetBool("Open", true);
            StartCoroutine(disableCollider());
            //open = true;
        }

    


        if (Input.GetKeyDown(actionKey) && atDoor == true && open && target1.GetComponent<DisableDoor>().disabled == false && locked == false)
        {
            animator.SetBool("Open", false);
            GetComponent<BoxCollider2D>().enabled = true;
            //open = false;
        }

        /*if (Input.GetKeyDown(actionKey) && atDoor == true && open && target1.GetComponent<DisableDoor>().disabled == true)
        {
            animator.SetBool("Open", true);
            GetComponent<BoxCollider2D>().enabled = false;
            //open = false;
        }
        */


        if (GetComponent<BoxCollider2D>().enabled == false)
        {
            open = true;
        }
        if (GetComponent<BoxCollider2D>().enabled == true)
        {
            open = false;
        }

    }



void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
             
                GetComponent<Animator>().SetBool("atDoor", true);
                atDoor = true;

            }
         
        }
void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
               
                GetComponent<Animator>().SetBool("atDoor", false);
                atDoor = false;
            }
        }

    //Delay for door collider disable

    IEnumerator disableCollider()
    {
        yield return new WaitForSeconds(0.2f);
        GetComponent<BoxCollider2D>().enabled = false;
    }

    IEnumerator openLocked()
    {
        animator.SetBool("OpenLocked", true);
        yield return new WaitForSeconds(0.2f);
        animator.SetBool("OpenLocked", false);
    }


    }



