using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDisable : MonoBehaviour
{
   public bool disableTop;
  


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            disableTop = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            disableTop = false;
        }
    }
}
