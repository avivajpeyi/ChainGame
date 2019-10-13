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
    public GameObject[] grapplePoints;

    // PRIVATE ATTRIBUTES
    private EnemyMaster enemyMaster;
    private Rigidbody2D rb;
    private int grapplePointIdx = 0;
    private bool Chainmode;

    // Start is called before the first frame update
    void Start()
    {
        enemyMaster = FindObjectOfType<EnemyMaster>();
        rb = this.gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        enemyMaster.resetList(); // TODO: this is too expensive 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Chainmode = true;
        }

        if (Chainmode == true)
        {
            grapplePoints = enemyMaster.targetEnemyList.ToArray();
            ChainToCurrentGrapplePoint();
        }

        
    }


    void TurnOffChainMode()
    {
        Chainmode=false;
        grapplePointIdx=0;
    }


    void ChainToCurrentGrapplePoint()
    {
        if (enemyMaster.targetEnemyList.Count<1  // if no grapple points
            || 
            grapplePointIdx > enemyMaster.targetEnemyList.Count - 1 // trying to access grapple point that doesnt exist
            )
        {
            TurnOffChainMode();
            return;
        }


        GameObject gp = grapplePoints[grapplePointIdx];
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
                grapplePointIdx = grapplePointIdx + 1;
            }
        }
        catch (MissingReferenceException e)
        {
            TurnOffChainMode();
            enemyMaster.resetList();
            
        }
    }


    void DashToPoint(Vector2 startPoint, Vector2 endPoint, float speed)
    {
        Vector2 direction = endPoint - startPoint; //unscaled
        direction = direction * (1 / (direction.magnitude)); //scaled
        //rb.velocity = speed * direction; //velocity
        rb.AddForce(speed * direction);
    }
}