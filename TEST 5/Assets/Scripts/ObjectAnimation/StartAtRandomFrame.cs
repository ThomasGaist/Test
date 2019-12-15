using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAtRandomFrame : MonoBehaviour
{
    private Animator animator;
    [SerializeField] string animationName;
    [SerializeField] int animationLayer;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        Play(); 

    }

    // Update is called once per frame
    void Play()
    {
        animator.Play(animationName, animationLayer, Random.Range(0.0f, 1.0f));
    }
}
