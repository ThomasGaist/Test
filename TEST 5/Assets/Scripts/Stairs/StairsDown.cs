using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsDown : MonoBehaviour
{
    public GameObject stairsSprite1;
    public GameObject stairsSprite2;
    public GameObject stairsSpriteMiddle;
    public GameObject stairCollider;
    public GameObject floorCollider;

    public GameObject top;
    public GameObject stairs;
    public GameObject bottom;

    public GameObject topDisable;
    public GameObject bottomDisable;

    // Start is called before the first frame update
    void Start()
    {
     
            stairsSprite1.GetComponent<SpriteRenderer>().color = new Color(0.7f, 0.7f, 0.7f);
            stairsSprite2.GetComponent<SpriteRenderer>().color = new Color(0.7f, 0.7f, 0.7f);
            stairsSpriteMiddle.GetComponent<SpriteRenderer>().color = new Color(0.7f, 0.7f, 0.7f);
     
           
        floorCollider.GetComponent<BoxCollider2D>().enabled = true;
        stairCollider.GetComponent<CapsuleCollider2D>().enabled = false;



    //    top.gameObject.GetComponent<StairsTop>().atTop == true
     //     bottom.gameObject.GetComponent<StairsBottom>().atBottom == true
    // stairs.gameObject.GetComponent<StairCase>().onStairs == true

    }

    // Update is called once per frame
    void Update()
    {
       //top of stairs, activating staircase

        if(top.gameObject.GetComponent<StairsTop>().atTop == true && Input.GetKeyDown(KeyCode.DownArrow)| Input.GetKeyDown(KeyCode.S) && floorCollider.GetComponent<BoxCollider2D>().enabled == true)
        {
            floorCollider.GetComponent<BoxCollider2D>().enabled = false;
            stairCollider.GetComponent<CapsuleCollider2D>().enabled = true;

            StartCoroutine(fromShadow());
        }

        if (top.gameObject.GetComponent<StairsTop>().atTop == true && Input.GetKey(KeyCode.UpArrow) | Input.GetKey(KeyCode.W)  && floorCollider.GetComponent<BoxCollider2D>().enabled == false)
        {
            floorCollider.GetComponent<BoxCollider2D>().enabled = true;
            stairCollider.GetComponent<CapsuleCollider2D>().enabled = false;

            StartCoroutine(intoShadow());
        }

        if(topDisable.gameObject.GetComponent<TopDisable>().disableTop == true && stairCollider.GetComponent<CapsuleCollider2D>().enabled == true)
        {
            floorCollider.GetComponent<BoxCollider2D>().enabled = true;
            stairCollider.GetComponent<CapsuleCollider2D>().enabled = false;

            StartCoroutine(intoShadow());
        }

     

        if (stairs.gameObject.GetComponent<StairCase>().onStairs == true && Input.GetKeyDown(KeyCode.DownArrow) | Input.GetKeyDown(KeyCode.S) && stairCollider.GetComponent<CapsuleCollider2D>().enabled == true)
        {
            StartCoroutine(waitDisableFloor());
            stairCollider.GetComponent<CapsuleCollider2D>().enabled = false;

            StartCoroutine(intoShadow());
        }

        if(bottom.gameObject.GetComponent<StairsBottom>().atBottom == true && Input.GetKey(KeyCode.UpArrow) | Input.GetKey(KeyCode.W) && stairCollider.GetComponent<CapsuleCollider2D>().enabled == false)
        {
            floorCollider.GetComponent<BoxCollider2D>().enabled = false;
            stairCollider.GetComponent<CapsuleCollider2D>().enabled = true;

            StartCoroutine(fromShadow());
        }

        if(bottom.gameObject.GetComponent<StairsBottom>().atBottom == true && Input.GetKey(KeyCode.DownArrow) | Input.GetKey(KeyCode.S) && stairCollider.GetComponent<CapsuleCollider2D>().enabled == true)
        {
            floorCollider.GetComponent<BoxCollider2D>().enabled = true;
            stairCollider.GetComponent<CapsuleCollider2D>().enabled = false;

            StartCoroutine(intoShadow());
        }


        /*if (bottom.gameObject.GetComponent<StairsBottom>().atBottom == true && stairCollider.GetComponent<CapsuleCollider2D>().enabled == true)
        {
            floorCollider.GetComponent<BoxCollider2D>().enabled = true;
            stairCollider.GetComponent<CapsuleCollider2D>().enabled = false;

            StartCoroutine(intoShadow());
        }*/


        //Bottom disable, trigger enabled when on stairs, trigger turns off stairs when player reaches bottom of stairs. 

        if (stairs.gameObject.GetComponent<StairCase>().onStairs == true && stairCollider.GetComponent<CapsuleCollider2D>().enabled == true)
        {
            bottomDisable.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }



        if (stairCollider.GetComponent<CapsuleCollider2D>().enabled == true && bottomDisable.gameObject.GetComponent<BottomDisable>().bottomDisable == true)
        {
            floorCollider.GetComponent<BoxCollider2D>().enabled = true;
            stairCollider.GetComponent<CapsuleCollider2D>().enabled = false;

            bottomDisable.gameObject.GetComponent<BoxCollider2D>().enabled = false;

            StartCoroutine(intoShadow());
        }


    }

    IEnumerator fromShadow()
    {
        stairsSprite1.GetComponent<SpriteRenderer>().color = new Color(.8f, .8f, .8f);
        stairsSprite2.GetComponent<SpriteRenderer>().color = new Color(.8f, .8f, .8f);
        stairsSpriteMiddle.GetComponent<SpriteRenderer>().color = new Color(.8f, .8f, .8f);
        yield return new WaitForSeconds(0.05f);
        stairsSprite1.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
        stairsSprite2.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
        stairsSpriteMiddle.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);

    }
IEnumerator intoShadow()
    {
        stairsSprite1.GetComponent<SpriteRenderer>().color = new Color(.8f, .8f, .8f);
        stairsSprite2.GetComponent<SpriteRenderer>().color = new Color(.8f, .8f, .8f);
        stairsSpriteMiddle.GetComponent<SpriteRenderer>().color = new Color(.8f, .8f, .8f);
        yield return new WaitForSeconds(0.05f);
        stairsSprite1.GetComponent<SpriteRenderer>().color = new Color(.7f, .7f, .7f);
        stairsSprite2.GetComponent<SpriteRenderer>().color = new Color(.7f, .7f, .7f);
        stairsSpriteMiddle.GetComponent<SpriteRenderer>().color = new Color(.7f, .7f, .7f);

    }
IEnumerator waitDisableFloor()
    {
        yield return new WaitForSeconds(0.2f);
        floorCollider.GetComponent<BoxCollider2D>().enabled = true;
    }
}
