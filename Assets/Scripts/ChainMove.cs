using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Rendering;
using System;
using UnityEngine.UI;

public class ChainMove : MonoBehaviour
{
    private Rigidbody2D rb;
    public GameObject[] grapplePoints;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("q"))
        {
            print("q key was pressed");
            PerformChain();
        }
    }


    void PerformChain()
    {
        print("Perform chain");
        // if grapple points empty return 
        if (grapplePoints.Length < 1)
        {
            print("no grapple points");
            return;
        }

        print("Getting grapple points");


        // iterate through list of grapple points
        foreach (GameObject gp in grapplePoints)
        {
            print("Grapple point: " + gp.name + "(" + gp.transform.position + ")");
            //this.transform.position = gp.transform.position
            DashToPoint(this.transform.position, gp.transform.position, 1);
        }

        // interpolate player position from one grapple point to next
    }


    void DashToPoint(Vector2 startPoint, Vector2 endPoint, float speed)
    {
        float tolerance = 1.0f;
        float distanceBwPoints = Vector2.Distance(startPoint, endPoint);
        
        while (distanceBwPoints > tolerance)
        {
            Vector2 direction = startPoint - endPoint; //unscaled
            direction =  direction * (1 / (direction.magnitude)); //scaled
            rb.velocity = speed * direction; //velocity
        }
    }
}