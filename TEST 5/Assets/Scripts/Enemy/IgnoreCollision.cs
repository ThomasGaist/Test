using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollision : MonoBehaviour
{

    [SerializeField]
    private Collider2D other;

    private void Awake()
    {
        other = SetPlayer.player.GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), other, true);
        Physics2D.IgnoreLayerCollision(12, 12);
        Physics2D.IgnoreLayerCollision(12, 17);
    }

    private void OnDisable()
    {
       // Physics2D.IgnoreCollision(GetComponent<Collider2D>(), other, false);
    }
}
