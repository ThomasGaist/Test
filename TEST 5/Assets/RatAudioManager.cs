using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatAudioManager : MonoBehaviour
{
    GameEvents eventsystem;
    [SerializeField]
    private AudioClip[] damageSounds;
    [SerializeField]
    private AudioClip[] deathSounds;


    int random;
    int clipInt;
    AudioSource audioSource;


    void Start()
    {
        eventsystem = GameEvents.current;

        eventsystem.onEnemyDamage += DamageAudio;
        eventsystem.onEnemyDeath += DamageAudio; //Maybe separate deathsounds at some point

        audioSource = GetComponent<AudioSource>();
    }

    void DamageAudio()
    {
        RandomClipNumber();

        audioSource.PlayOneShot(damageSounds[clipInt]);
    }


    void DeathAudio()
    {
        RandomClipNumber();

        audioSource.PlayOneShot(deathSounds[clipInt]);
    }

    private int RandomClipNumber()
    {
        random = Random.Range(0, damageSounds.Length);
        while (random == clipInt)
        {
            random = Random.Range(0, damageSounds.Length);
        }

        clipInt = random;

        return clipInt;
    }


}
