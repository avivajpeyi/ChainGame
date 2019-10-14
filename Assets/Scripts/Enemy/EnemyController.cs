using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static EnemyMaster.EnemyType;
using Random = UnityEngine.Random;


public class EnemyController : MonoBehaviour

{

    private BoxCollider2D Collider2D;
    private GameController gameController;
    private Rigidbody2D rb;
    private float maxSpeed = 1;
    
    public EnemyMaster.EnemyType type = GRAPPLING;
    public SpriteRenderer renderer;
    public GameObject effectsController;

    private float zombie_speed = 1;
    private float zombie_shamble_factor = 3;
    private float timeToSpawn = 2;
    private bool spawnComplete = false;

    private Color currentColor;
    private Color GrapplingColor = Color.red;
    private Color OtherColor = Color.green; 
    
    private GameObject Player;

    private IEnumerator colorCoroutine; 
    private IEnumerator colorCoroutine2; 
    
    // Start is called before the first frame update
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        Collider2D = GetComponent<BoxCollider2D>();
        Collider2D.enabled = false;
        if (Time.time<5)
        {
            type = GRAPPLING;
        }
        else
        {
            type = GetRandomType();
        }

        Player = FindObjectOfType<PlayerMaster>().gameObject;

        rb = this.gameObject.GetComponent<Rigidbody2D>();
        
        SetMyCustomSettings();
        SetMyColor();
        Collider2D.enabled = true;

        
        // convert into a GRAPPLING type after 15-30 seconds
        StartCoroutine(ConvertToGrappeling(Random.Range(5, 20)));
    }

    /// <summary>
    /// Converts to GRAPPELLING type after X seconds
    /// </summary>
    /// <param name="waitTime"></param>
    /// <returns></returns>
    private IEnumerator ConvertToGrappeling(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        type = GRAPPLING;
        colorCoroutine2 = convertColors(currentColor, GrapplingColor);
        StartCoroutine(colorCoroutine2);
       
    }


    public IEnumerator convertColors(Color startColor, Color endColor) {
        float ElapsedTime = 0.0f;
        float TotalTime = 1.0f;
        while (ElapsedTime < TotalTime) {
            ElapsedTime += Time.deltaTime;
            renderer.color = Color.Lerp(startColor, endColor, (ElapsedTime / TotalTime));
            yield return null;
        }
    }
    
    
    public static EnemyMaster.EnemyType GetRandomType()
    {
        EnemyMaster.EnemyType randomEnemy = 
            (EnemyMaster.EnemyType) Random.Range(
            min: 0,
            max: Enum.GetValues(typeof(EnemyMaster.EnemyType)).Length
        );
        
        return randomEnemy;
    }

    private void SetMyCustomSettings()
    {
        if (type == GRAPPLING ||
            type == BALLISTIC)
        {
            
            rb.velocity = new Vector2(
                Random.Range(-maxSpeed, maxSpeed),
                Random.Range(-maxSpeed, maxSpeed));
        }
        
    }

    public void SetActiveTarget()
    {
        if (colorCoroutine != null) 
            StopCoroutine(colorCoroutine);
        if (colorCoroutine2 != null)
            StopCoroutine(colorCoroutine2);
        currentColor = Color.blue;
        renderer.color = Color.blue;
    }

    void Update()
    {
        if (!gameController.isGameOver)
        {
            if (type == ZOMBIE)
            {
                zombie_shamble(
                    startPoint: this.transform.position, 
                    endPoint: Player.transform.position,
                    speed: zombie_speed
                );
            }
            else if (type == FOLLOWER)
            {
                float randx = Random.Range(-1, 1);
                float randy = Random.Range(-1, 1);
                Vector2 endPoint = (Vector2) Player.transform.position +
                                   new Vector2(randx, randy);
                    
                Vector2 pos = Vector2.Lerp (
                    a: (Vector2)transform.position, 
                    b:  endPoint, 
                    Time.fixedDeltaTime);
                rb.MovePosition(pos);
            }
        }


        
        
        
    }

    void zombie_shamble(Vector2 startPoint, Vector2 endPoint, float speed)
    {
        Vector2 direction = endPoint - startPoint; //unscaled
        direction = direction * (1 / (direction.magnitude)); //scaled
        rb.velocity = speed * direction;
        float randx = Random.Range(-1 * zombie_shamble_factor * zombie_speed,
            zombie_shamble_factor * zombie_speed);
        float randy = Random.Range(-1 * zombie_shamble_factor * zombie_speed,
            zombie_shamble_factor * zombie_speed);
        rb.velocity += new Vector2(randx, randy);
        //rb.AddForce(speed * direction);
    }


    private void SetMyColor()
    {
        if (type == gameController.grappleEnemyType)
            currentColor = GrapplingColor;
        else
            currentColor =OtherColor;
        
        Color initalColor = new Color(
            currentColor.r, currentColor.g, currentColor.b, 0); 

        renderer.color = initalColor;
        colorCoroutine = convertColors(initalColor, currentColor);

        StartCoroutine(colorCoroutine);
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
        gameController.IncreaseScore();
        // Create a gameobject at the location of where the effect should play
        GameObject myEffects = Instantiate(
            original: effectsController,
            position: this.transform.position,
            rotation: Quaternion.identity
        );
        // Play the effect
        myEffects.GetComponent<EffectsController>().Play(relativeVelocity);
        Destroy(this.gameObject);
        Destroy(this);
    }
}