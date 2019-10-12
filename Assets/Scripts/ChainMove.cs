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
    private int i = 0;
    private bool Chainmode;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        rb=this.gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //print(this.transform.position);
        //print(rb.velocity);
        if (Input.GetKeyDown("q"))
        {
            print("q key was pressed");
            Chainmode=true;
        }
        if(Chainmode==true)
        {
            fly();
        }
    }


    void fly()
    {
        print("Fly my pretty");
        // if grapple points empty return 
        if (grapplePoints.Length < 1)
        {
            print("no grapple points");
            return;
        }
        if(i>grapplePoints.Length-1)//reset
        {
            Chainmode=false;
            i=0;
            return;
        }
        print("Getting grapple points");

        // iterate through list of grapple points
        //foreach (GameObject gp in grapplePoints)
        GameObject gp=grapplePoints[i];
        print("Grapple point: " + gp.name + "(" + gp.transform.position + ")");
        //this.transform.position = gp.transform.position
        float tolerance = 1.0f;
        float distanceBwPoints = Vector2.Distance(this.transform.position, gp.transform.position);
        if(distanceBwPoints > tolerance)
        {
            DashToPoint(this.transform.position, gp.transform.position, speed);
        }
        else
        {
            i=i+1;
        }
    }


    void DashToPoint(Vector2 startPoint, Vector2 endPoint, float speed)
    {
        Vector2 direction = endPoint-startPoint; //unscaled
        direction =  direction * (1 / (direction.magnitude)); //scaled
        rb.velocity = speed * direction; //velocity
        //rb.AddForce(speed*direction);

    }


}