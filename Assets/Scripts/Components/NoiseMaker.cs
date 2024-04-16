using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseMaker : MonoBehaviour
{
    public float volumeDistance;
    public bool canMakeSound; // variable where designers can check it to make sound for the other players to hear
    public AudioSource movingAudioSource;
    public AudioSource ability1AudioSource;
    public AudioSource ability2AudioSource;
    public AudioSource ability3AudioSource;
    void Start()
    {

    }

    void Update()
    {
        
    }

    public void PlayMovingSound()
    {
        if (canMakeSound == true)
        {
            if (movingAudioSource.isPlaying == false)
            {
                movingAudioSource.Play();
            }
        }
    }

    public void StopMovingSound()
    {
        movingAudioSource.Pause();
    }

    public void PlayAbility1Sound()
    {
        if (ability1AudioSource.isPlaying == false)
        {
            ability1AudioSource.Play();
        }
    }

    public void StopAbility1Sound()
    {
        ability1AudioSource.Pause();
    }

    public void PlayAbility2Sound()
    {
        if (ability2AudioSource.isPlaying == false)
        {
            ability2AudioSource.Play();
        }
    }

    public void StopAbility2Sound()
    {
        ability2AudioSource.Pause();
    }

    public void PlayAbility3Sound()
    {
        if (ability3AudioSource.isPlaying == false)
        {
            ability3AudioSource.Play();
        }
    }

    public void StopAbility3Sound()
    {
        ability3AudioSource.Pause();
    }
}
