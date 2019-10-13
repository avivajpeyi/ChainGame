using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    private Rigidbody2D rb;
    public float maxSpeed = 10;
    public float acc = 40;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
    }
    
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            // print("a key was pressed");
            Forceleft();
        }

        if (Input.GetKey(KeyCode.D))
        {
            // print("d key was pressed");
            Forceright();
        }

        if (Input.GetKey(KeyCode.S))
        {
            //print("s key was pressed");
            Forcedown();
        }

        if (Input.GetKey(KeyCode.W))
        {
            // print("w key was pressed");
            Forceup();
        }
    }
    
    
    void Forceleft()
    {
        rb.AddForce(new Vector2(-acc, 0));
        if (rb.velocity.x < -maxSpeed)
        {
            rb.velocity = new Vector2(-maxSpeed, rb.velocity.y);
        }
    }

    void Forceright()
    {
        rb.AddForce(new Vector2(acc, 0));
        if (rb.velocity.x > maxSpeed)
        {
            rb.velocity = new Vector2(maxSpeed, rb.velocity.y);
        }
    }

    void Forcedown()
    {
        rb.AddForce(new Vector2(0, -acc));
        if (rb.velocity.y < -maxSpeed)
        {
            rb.velocity = new Vector2(rb.velocity.x, -maxSpeed);
        }
    }

    void Forceup()
    {
        rb.AddForce(new Vector2(0, acc));
        if (rb.velocity.y > maxSpeed)
        {
            rb.velocity = new Vector2(rb.velocity.x, maxSpeed);
        }
    }

    
}
