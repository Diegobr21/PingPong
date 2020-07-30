using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFX : MonoBehaviour
{
    
    public AudioClip[] fxs; // 0-ball drop 1- musica
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void BallDrop(){

        audioSource.clip = fxs[0];
        audioSource.Play();
    }

    public void Musica(){
        audioSource.clip = fxs[1];
        audioSource.Play();
    }

    public void Point(){
        audioSource.clip = fxs[2];
        audioSource.Play();
    }


   
}
