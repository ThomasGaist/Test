using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    GameEvents eventsystem;

    [SerializeField]
    private AudioClip[] footsteps;

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
        int random = Random.Range(0, footsteps.Length);

        audioSource.PlayOneShot(footsteps[random]);
    }
}
