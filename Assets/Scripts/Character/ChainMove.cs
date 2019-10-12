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
    float tolerance = 1.0f;

    private EnemyMaster enemyMaster;

    public float speed, max_speed, acc;

    // Start is called before the first frame update
    void Start()
    {
        enemyMaster = FindObjectOfType<EnemyMaster>();
        rb = this.gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //print(this.transform.position);
        //print(rb.velocity);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //print("Space key was pressed");
            Chainmode = true;
        }

        if (Chainmode == true)
        {
           
            grapplePoints = enemyMaster.currentEnemyList.ToArray();
            fly();
        }

        if (Input.GetKey(KeyCode.A))
        {
            // print("a key was pressed");
            forceleft();
        }

        if (Input.GetKey(KeyCode.D))
        {
            // print("d key was pressed");
            forceright();
        }

        if (Input.GetKey(KeyCode.S))
        {
            //print("s key was pressed");
            forcedown();
        }

        if (Input.GetKey(KeyCode.W))
        {
            // print("w key was pressed");
            forceup();
        }
    }

    void forceleft()
    {
        rb.AddForce(new Vector2(-acc, 0));
        if (rb.velocity.x < -max_speed)
        {
            rb.velocity = new Vector2(-max_speed, rb.velocity.y);
        }
    }

    void forceright()
    {
        rb.AddForce(new Vector2(acc, 0));
        if (rb.velocity.x > max_speed)
        {
            rb.velocity = new Vector2(max_speed, rb.velocity.y);
        }
    }

    void forcedown()
    {
        rb.AddForce(new Vector2(0, -acc));
        if (rb.velocity.y < -max_speed)
        {
            rb.velocity = new Vector2(rb.velocity.x, -max_speed);
        }
    }

    void forceup()
    {
        rb.AddForce(new Vector2(0, acc));
        if (rb.velocity.y > max_speed)
        {
            rb.velocity = new Vector2(rb.velocity.x, max_speed);
        }
    }


    void fly()
    {
        if (grapplePoints.Length < 1)
        {
            print("no grapple points");
            return;
        }

        if (i > grapplePoints.Length - 1) //reset
        {
            Chainmode = false;
            i = 0;
            return;
        }

        GameObject gp = grapplePoints[i];
        try
        {
            float distanceBwPoints =
                Vector2.Distance(this.transform.position, gp.transform.position);
            if (distanceBwPoints > tolerance)
            {
                DashToPoint(this.transform.position, gp.transform.position, speed);
            }
            else
            {
                i = i + 1;
            }
        }
        catch (MissingReferenceException e)
        {
            i = 0;
            Chainmode = false;
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