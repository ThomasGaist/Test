using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsTop : MonoBehaviour
{
    public bool atTop;

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            atTop = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            atTop = false;
        }
    }
}
