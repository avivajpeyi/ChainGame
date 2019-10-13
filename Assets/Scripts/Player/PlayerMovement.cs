using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    private Rigidbody2D rb;
    public float max_speed = 10;
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

    
}
