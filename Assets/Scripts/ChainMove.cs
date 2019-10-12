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
    public float speed, max_speed, acc;
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            print("Space key was pressed");
            Chainmode=true;
        }
        if(Chainmode==true)
        {
            fly();
        }

        if (Input.GetKey(KeyCode.A))
        {
            print("a key was pressed");
            forceleft();
        }

        if (Input.GetKey(KeyCode.D))
        {
            print("d key was pressed");
            forceright();
        }
        
        if (Input.GetKey(KeyCode.S))
        {
            print("s key was pressed");
            forcedown();
        }

        if (Input.GetKey(KeyCode.W))
        {
            print("w key was pressed");
            forceup();
        }
    }

    void forceleft()
    {
        print("moving left");
        rb.AddForce(new Vector2(-acc,0));
        if (rb.velocity.x < - max_speed)
        {
            rb.velocity= new Vector2(-max_speed,rb.velocity.y);
        }
    }

    void forceright()
    {
        print("moving right");
        rb.AddForce(new Vector2(acc,0));
        if (rb.velocity.x > max_speed)
        {
            rb.velocity= new Vector2(max_speed,rb.velocity.y);
        }
    }

    void forcedown()
    {
        print("moving down");
        rb.AddForce(new Vector2(0,-acc));
        if (rb.velocity.y < - max_speed)
        {
            rb.velocity= new Vector2(rb.velocity.x,-max_speed);
        }
    }

    void forceup()
    {
        print("moving up");
        rb.AddForce(new Vector2(0,acc));
        if (rb.velocity.y > max_speed)
        {
            rb.velocity= new Vector2(rb.velocity.x,max_speed);
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
        //rb.velocity = speed * direction; //velocity
        rb.AddForce(speed*direction);

    }


}