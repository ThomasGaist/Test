using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsBottom : MonoBehaviour
{
    public bool atBottom;

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            atBottom = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            atBottom = false;
        }
    }
}
