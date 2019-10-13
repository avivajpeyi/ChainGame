using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyController : MonoBehaviour
{

    private GameController gameController;
    
    // Enemy type (right now just 0 and non-zero)
    public int enemyType = 0 ;
    public SpriteRenderer renderer;
    public GameObject effectsController;
    // Start is called before the first frame update
    void Start()
    {

        gameController = FindObjectOfType<GameController>();
        SetMyColor();
    }


    private void SetMyColor()
    {
        if (enemyType == gameController.grappleEnemyType)
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
            enemyType==gameController.grappleEnemyType  
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