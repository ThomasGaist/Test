using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairCase : MonoBehaviour
{
    public bool onStairs;

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            onStairs = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            onStairs = false;
        }
    }
}
