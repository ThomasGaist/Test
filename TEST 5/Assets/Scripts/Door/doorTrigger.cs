using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorTrigger : MonoBehaviour
{

    //protected bool openDoor = false;
    public GameObject target1;
    protected static bool atDoor = false;
    protected KeyCode actionKey = KeyCode.E;
    //private bool open = false;


    void Update()
    {
       /* if (open == false)
        {
            target1.GetComponent<BoxCollider2D>().enabled = true;
        }
        else if(open == true)
        {
            target1.GetComponent<BoxCollider2D>().enabled = true;
        }*/
        
        
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //openDoor = true;
            //target1.GetComponent<Animator>().enabled = true;
            target1.GetComponent<Animator>().SetBool("atDoor", true);

            Debug.Log("triggered");
            atDoor = true;

        }
        /*if (collision.gameObject.CompareTag("Player") && Input.GetKeyDown(actionKey)){
            open = !open;
        }*/
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //openDoor = false;
            //target1.GetComponent<Animator>().enabled = false;
            target1.GetComponent<Animator>().SetBool("atDoor", false);

            Debug.Log("untriggered");

            atDoor = false;
        }
    }
}

