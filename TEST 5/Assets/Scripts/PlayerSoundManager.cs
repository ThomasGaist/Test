using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    GameEvents eventsystem;

    [SerializeField]
    private AudioClip[] footsteps;
    private int clipInt;
    private int random;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        eventsystem = GameEvents.current;
        eventsystem.onPlayerAttack += AttackAudio;
        eventsystem.onPlayerFootstep += FootSteps;

        audioSource = GetComponent<AudioSource>();
    }

    private void AttackAudio()
    {
        
    }
    private void FootSteps()
    {
        random = Random.Range(0, footsteps.Length);
        while(random == clipInt)
        {
            random = Random.Range(0, footsteps.Length);
        }

        clipInt = random;

        audioSource.PlayOneShot(footsteps[clipInt]);
    }
}
