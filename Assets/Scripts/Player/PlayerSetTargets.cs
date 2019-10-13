using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetTargets : MonoBehaviour
{

    public List<GameObject> targets;
    //public GameObject mouseClickObject; 
    
    public GameObject effect;
    private ParticleSystem effectParticles;
    
    private bool mouseIsDown = false;
    private Vector2 mousePos2D;
    
    // Start is called before the first frame update
    void Start()
    {
        effectParticles = effect.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayerClick();
        UpdateEffect();
    }
    
    void UpdateEffect()
    {
        if (mouseIsDown)
        {
            effect.SetActive(true);
            effect.transform.position = (Vector3) mousePos2D;
            effectParticles.Play();
        }
        else
        {    
            effectParticles.Stop();
            effect.SetActive(false);
        }
    }
    
    public void CleanEnemyListsOfDeadEnemies()
    {
        for (var i = targets.Count - 1; i > -1; i--)
        {
            if (targets[i] == null)
                targets.RemoveAt(i);
        }
    }

    void CheckPlayerClick()
    {
        if (Input.GetMouseButton(0))
        {
            mouseIsDown = true;

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos2D = new Vector2(mousePos.x, mousePos.y);
            
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null) {
                
                GameObject hitGo = hit.collider.gameObject;
                if (hitGo.CompareTag("Enemy"))
                {
                    EnemyController ec = hitGo.GetComponent<EnemyController>();
                    print(ec.type);
                    if (ec.type == EnemyMaster.EnemyType.GRAPPLING)
                    {
                        bool enemyAlreadyTarget = targets.Contains(hitGo);
                        if (!enemyAlreadyTarget)
                        {
                            Debug.Log("Adding to list");
                            hitGo.GetComponent<EnemyController>().SetActiveTarget();
                            targets.Add(hitGo);
                        }
                    }

                    
                }

            }
        }
    }

}
