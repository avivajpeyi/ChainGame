using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyController : MonoBehaviour
{

    public int enemyType = 0 ;
    public SpriteRenderer renderer;
    public GameObject effectsController;
    // Start is called before the first frame update
    void Start()
    {
        
        if (enemyType !=0)
            renderer.color = Color.green;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnCollisionEnter2D(Collision2D col)
    {

        if (col.transform.tag == "Player" && enemyType!=0)
        {
            OnEnemyDeath(col.relativeVelocity);
        }
    }


    void OnEnemyDeath(Vector2 relativeVelocity)
    {
        GameObject myEffects = Instantiate(effectsController, this.transform.position,  Quaternion.identity);
        myEffects.GetComponent<EffectsController>().Play(relativeVelocity);
        Destroy(this.gameObject);
    }
}