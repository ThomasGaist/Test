using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollision : MonoBehaviour
{

    [SerializeField]
    private Collider2D other;

    private void Awake()
    {
        other = FindObjectOfType<Player>().GetComponent<BoxCollider2D>();
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), other, true);
        Physics2D.IgnoreLayerCollision(12, 12);
    }

    private void OnDisable()
    {
       // Physics2D.IgnoreCollision(GetComponent<Collider2D>(), other, false);
    }
}
