using System;
using System.Collections;
using System.Collections.Generic;

using Random = UnityEngine.Random;
using UnityEngine;

public class EffectsController : MonoBehaviour  
{
    public ParticleSystem sparks;
    public ParticleSystem shardParticles;
    [Header("Unity Setup")] public float time;
    
    
    [Range(0,1)]
    public float camShakeDuration = 0.5f;
    
    [Range(0,1)]
    public float camShakeMagnitude = 0.5f;
    
    public AudioSource audioSource;
    public AudioClip[] shatterSounds;
    private AudioClip shatterClip;
    

    void PlayAudioClip()
    {
        if (audioSource != null)
        {
            int index = Random.Range(0, shatterSounds.Length);
            shatterClip = shatterSounds[index];
            audioSource.clip = shatterClip;
            audioSource.Play();
        }
        
    }

    public void Play(Vector2 directionOfEffect)
    {
        PlayAudioClip();
        StartCoroutine(FindObjectOfType<CameraShake>().Shake(camShakeDuration, camShakeMagnitude));
        sparks.Play();
        shardParticles.transform.forward = directionOfEffect;
        shardParticles.Play();
        Destroy(gameObject, time);
    }


}
