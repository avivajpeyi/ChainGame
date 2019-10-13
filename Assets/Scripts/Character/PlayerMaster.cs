using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMaster : MonoBehaviour
{
    public int grappleingEnemyType = 0;
    public SpriteRenderer renderer;
    public ParticleSystem trails;
    public GameObject deathEffectsController;

    private GameController gameController;

    // Start is called before the first frame update
    void Start()
    {

        gameController = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.tag == "Enemy")
        {
            GameObject enemy = col.transform.gameObject;
            EnemyController enemyController = enemy.GetComponent<EnemyController>();
            if (enemyController.enemyType >0)
                OnPlayerDeath(col.relativeVelocity);
        }
    }


    void OnPlayerDeath(Vector2 relativeVelocity)
    {
        GameObject myEffects = Instantiate(deathEffectsController,
            this.transform.position, Quaternion.identity);
        myEffects.GetComponent<EffectsController>().Play(relativeVelocity);
        gameController.GameOver();
        Destroy(this.gameObject);
    }
}