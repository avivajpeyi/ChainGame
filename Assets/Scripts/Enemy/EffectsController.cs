using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class EffectsController : MonoBehaviour  
{
    public ParticleSystem sparks;
    public ParticleSystem shardParticles;
    
    [Header("Unity Setup")] public float time;
    
    
    [Range(0,1)]
    public float camShakeDuration = 0.5f;
    
    [Range(0,1)]
    public float camShakeMagnitude = 0.5f;
    
    private void Start()
    { 
       
    }

    public void Play(Vector2 directionOfEffect)
    {
        print("shards will be shot towards " + directionOfEffect);
        StartCoroutine(FindObjectOfType<CameraShake>().Shake(camShakeDuration, camShakeMagnitude));
        sparks.Play();
        shardParticles.transform.forward = directionOfEffect;
        shardParticles.Play();
        Destroy(gameObject, time);
    }


}
