using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Rendering;
using System;
using UnityEngine.UI;

public class ChainMove : MonoBehaviour
{
    // PUBLCIC ATTRIBUTES
    [Range(0,2.0f)]
    public float tolerance = 0.7f;
    [Range(0,50.0f)]
    public float grappleForceMagnitude=20;

    public ParticleSystem chainTrail;
    
    // PRIVATE ATTRIBUTES
    private EnemyMaster enemyMaster;
    private PlayerSetTargets PlayerSetTargets;
    private Rigidbody2D rb;
    private bool Chainmode;

    // Start is called before the first frame update
    void Start()
    {
        PlayerSetTargets = GetComponent<PlayerSetTargets>();
        enemyMaster = FindObjectOfType<EnemyMaster>();
        rb = this.gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonUp(0))
        {
            if (PlayerSetTargets.targets.Count != 0)
            {
                Chainmode = true;
            }

        }

        if (Chainmode)
        {
            TurnOnChainMode();
        }

        
    }

    void TurnOnChainMode()
    {
        chainTrail.Play();
        PlayerSetTargets.CleanEnemyListsOfDeadEnemies();
        enemyMaster.CleanEnemyListsOfDeadEnemies(); // TODO: this is too expensive 
        ChainToCurrentGrapplePoint();
    }


    void TurnOffChainMode()
    {
        chainTrail.Stop();
        Chainmode=false;
    }


    void ChainToCurrentGrapplePoint()
    {
        
        if (PlayerSetTargets.targets.Count==0)
        {
            TurnOffChainMode();
            return;
        }


        GameObject gp = PlayerSetTargets.targets[0];
        gp.GetComponent<EnemyController>().SetActiveTarget();
        try
        {
            float distanceBwPoints =
                Vector2.Distance(this.transform.position, gp.transform.position);
            if (distanceBwPoints > tolerance)
            {
                DashToPoint(this.transform.position, gp.transform.position, grappleForceMagnitude);
            }
            else
            {
                PlayerSetTargets.targets.Remove(gp);
            }
        }
        catch (MissingReferenceException e)
        {
            Debug.LogError("Error accessing enemy list : " + e);
            TurnOffChainMode();
            PlayerSetTargets.CleanEnemyListsOfDeadEnemies();
            enemyMaster.CleanEnemyListsOfDeadEnemies();
            
        }
    }


    void DashToPoint(Vector2 startPoint, Vector2 endPoint, float speed)
    {
        Vector2 direction = endPoint - startPoint; //unscaled
        direction = direction * (1 / (direction.magnitude)); //scaled
        rb.velocity = speed * direction; //velocity
        //rb.AddForce(speed * direction);
    }
}