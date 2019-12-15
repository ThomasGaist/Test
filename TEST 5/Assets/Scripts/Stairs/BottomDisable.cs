using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomDisable : MonoBehaviour
{
    public bool bottomDisable = false;
    public GameObject stairCollider;

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && stairCollider.GetComponent<CapsuleCollider2D>().enabled == true)
        {
            bottomDisable = true;
        }
        if (collision.gameObject.CompareTag("Player") && stairCollider.GetComponent<CapsuleCollider2D>().enabled == false)
        {
            bottomDisable = false;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            bottomDisable = false;
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
