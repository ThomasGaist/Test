using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairClimbing : PhysicsObject
{

 private bool triggerEntered = false;
 private BoxCollider2D trigger;

    private void Update()
    {
        trigger = GetComponent<BoxCollider2D>();

        if(Input.GetKey(KeyCode.UpArrow) && triggerEntered == true)
        {
            trigger.enabled = true;
        }
        else {
            trigger.enabled = false;
        }
    }
    void OnTriggerEnter()
    {
        triggerEntered = true;   
    }
    void OnTriggerExit()
    {
        triggerEntered = false;
    }

}

