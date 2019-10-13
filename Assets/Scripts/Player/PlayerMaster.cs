using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMaster : MonoBehaviour
{
    public BoxCollider2D collider2D;
    private Rigidbody2D rb;
    public SpriteRenderer renderer;
    public ParticleSystem trails;
    public GameObject deathEffectsController;
    private GameController gameController;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        gameController = FindObjectOfType<GameController>();
    }
    

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            // non grappelling can kill me
            GameObject enemy = col.transform.gameObject;
            EnemyController enemyController = enemy.GetComponent<EnemyController>();
            if (enemyController.type != gameController.grappleEnemyType)
                OnPlayerDeath(col.relativeVelocity);
        }
    }


    void OnPlayerDeath(Vector2 relativeVelocity)
    {
        GameObject myEffects = Instantiate(deathEffectsController,
            this.transform.position, Quaternion.identity);
        myEffects.GetComponent<EffectsController>().Play(relativeVelocity);
        gameController.GameOver();

        trails.Stop();
        collider2D.enabled = false;
        this.rb.velocity = Vector2.zero;
        renderer.color = new Color(0.0f,0.0f,0.0f,0.0f);
        Destroy(this.gameObject, 3);
    }
}