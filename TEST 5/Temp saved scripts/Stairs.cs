using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairs : MonoBehaviour
{
    protected CapsuleCollider2D cc2d;
    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        cc2d = target.GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
