using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsTrigger2 : MonoBehaviour
{

    //public bool colIgnore = true;
    private bool onStairs = false;
    public GameObject target;
    public GameObject target2;
    public GameObject player;
   // private float moveX;

    private void Awake()
    {
        target.GetComponent<CapsuleCollider2D>().enabled = false;
        target2.GetComponent<BoxCollider2D>().enabled = true;

        //moveX = player.GetComponent<Rigidbody2D>().velocity.magnitude;
    }

    private void Update()
    {
        if(onStairs == true)
        {
            target.GetComponent<CapsuleCollider2D>().enabled = true;
            target2.GetComponent<BoxCollider2D>().enabled = false;
        }
        else
        {
            target.GetComponent<CapsuleCollider2D>().enabled = false;
            target2.GetComponent<BoxCollider2D>().enabled = true;
        }
    }
    void OnTriggerStay2D(Collider2D collision)
    {
                //colIgnore = cc2d.enabled = false;
        //ignoreCollision = Physics2D.IgnoreLayerCollision(0, 8, true);

        if (collision.gameObject.CompareTag("Player") && Input.GetKey(KeyCode.UpArrow))
        {
            onStairs = true;
            Debug.Log("Triggered!1!!");
        }
        if (collision.gameObject.CompareTag("Player") && Input.GetKey(KeyCode.W))
        {
            onStairs = true;
            Debug.Log("Triggered!1!!");
        }
        if (collision.gameObject.CompareTag("Player") && Input.GetKey(KeyCode.DownArrow))
        {
            onStairs = true;
        }
        if (collision.gameObject.CompareTag("Player") && Input.GetKey(KeyCode.S))
        {
            onStairs = true;
        }


        /*else if (Input.GetKeyDown(KeyCode.DownArrow))

           ignoreCollision = false;

        else
        {
            ignoreCollision = true;
        }*/
    }
    void OnTriggerExit2D(Collider2D other)
    {

        onStairs = false;

        Debug.Log("Untriggered!1!!");
        /* if (other.gameObject.CompareTag("Player")){

             Physics2D.IgnoreLayerCollision(0, 8, true); }*/
    }
}
