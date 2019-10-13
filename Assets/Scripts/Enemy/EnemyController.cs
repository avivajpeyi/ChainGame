using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static EnemyMaster.EnemyType;
using Random = UnityEngine.Random;


public class EnemyController : MonoBehaviour

{



    private GameController gameController;
    private Rigidbody2D rb;
    private float maxSpeed = 1;


    // Enemy type (right now just 0 and non-zero)
    public EnemyMaster.EnemyType type = 0;
    public SpriteRenderer renderer;
    public GameObject effectsController;


    public static EnemyMaster.EnemyType GetRandomType()
    {
        return (EnemyMaster.EnemyType) Random.Range(
            min: 0,
            max: Enum.GetValues(typeof(EnemyMaster.EnemyType)).Length
        );
    }

    // Start is called before the first frame update
    void Start()
    {
        type = GetRandomType();
        gameController = FindObjectOfType<GameController>();
        rb = this.gameObject.GetComponent<Rigidbody2D>();

        SetMyColor();
        SetMyCustomSettings();
    }

    private void SetMyCustomSettings()
    {
        if (type == gameController.grappleEnemyType)
        {
            rb.velocity = new Vector2(
                Random.Range(-maxSpeed, maxSpeed),
                Random.Range(-maxSpeed, maxSpeed));
        }
    }


    private void SetMyColor()
    {
        if (type == gameController.grappleEnemyType)
        {
            renderer.color = Color.red;
        }
        else
        {
            renderer.color = Color.green;
        }
    }


    void OnCollisionEnter2D(Collision2D col)
    {
        // Player can only kill "grappleEnemyType"
        if (col.gameObject.CompareTag("Player") &&
            type == gameController.grappleEnemyType
        )
        {
            OnEnemyDeath(col.relativeVelocity);
        }
    }


    /// <summary>
    /// Instantiates death particle effects and destroys the current game obj
    /// </summary>
    /// <param name="relativeVelocity"></param>
    /// The relative velocity of the collision
    void OnEnemyDeath(Vector2 relativeVelocity)
    {
        // Create a gameobject at the location of where the effect should play
        GameObject myEffects = Instantiate(
            original: effectsController,
            position: this.transform.position,
            rotation: Quaternion.identity
        );
        // Play the effect
        myEffects.GetComponent<EffectsController>().Play(relativeVelocity);
        Destroy(this.gameObject);
    }
}