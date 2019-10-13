using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyController : MonoBehaviour

{

    private GameController gameController;
    private Rigidbody2D rb;
    private float enemy_max_speed=1;
    // Enemy type (right now just 0 and non-zero)
    public int enemyType = 0 ;
    public SpriteRenderer renderer;
    public GameObject effectsController;
    private float zombie_speed = 1;
    private float zombie_shamble_factor = 3;
    private GameObject Player;
    private float randx, randy;

    // Start is called before the first frame update
    void Start()
    {
        Player=FindObjectOfType<PlayerMaster>().gameObject;
        gameController = FindObjectOfType<GameController>();
        SetMyColor();
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        if(enemyType == gameController.grappleEnemyType)
        {
            rb.velocity = new Vector2(
                Random.Range(-enemy_max_speed, enemy_max_speed),
                Random.Range(-enemy_max_speed, enemy_max_speed));
        }
    }
    void Update()
    {
        if(enemyType == 1)
        {
            //player_position = Player.transform.position;
            zombie_shamble(this.transform.position, Player.transform.position, zombie_speed);
        }
    }

    void zombie_shamble(Vector2 startPoint, Vector2 endPoint, float speed)
    {
        Vector2 direction = endPoint - startPoint; //unscaled
        direction = direction * (1 / (direction.magnitude)); //scaled
        rb.velocity = speed * direction;
        randx = Random.Range(-1 * zombie_shamble_factor * zombie_speed,zombie_shamble_factor * zombie_speed);
        randy = Random.Range(-1 * zombie_shamble_factor * zombie_speed,zombie_shamble_factor * zombie_speed);
        rb.velocity += new Vector2(randx,randy);
        //rb.AddForce(speed * direction);
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