using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetTargets : MonoBehaviour
{

    public List<GameObject> targets;
    private SloMo slowMoController;
    private GameController gameController;
    public GameObject effect;
    private ParticleSystem effectParticles;
    
    private bool mouseIsDown = false;
    private Vector2 mousePos2D;

    public AudioSource audioSource;
    public AudioClip[] selectionSounds;
    private AudioClip selectionClip;
    

    void PlayAudioClip()
    {
        int index = Random.Range(0, selectionSounds.Length);
        selectionClip = selectionSounds[index];
        audioSource.clip = selectionClip;
        audioSource.Play();
    }   
    
    // Start is called before the first frame update
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        slowMoController = FindObjectOfType<SloMo>();
        effectParticles = effect.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameController.isGameOver)
        {
            CheckPlayerClick();
            UpdateEffect();
        }
    }
    
    void UpdateEffect()
    {
        if (mouseIsDown)
        {
            slowMoController.StartSlowmo();
            effect.SetActive(true);
            effect.transform.position = (Vector3) mousePos2D;
            effectParticles.Play();
        }
        else
        {   
            slowMoController.EndSlowmo();
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
                    if (ec.type == EnemyMaster.EnemyType.GRAPPLING)
                    {
                        bool enemyAlreadyTarget = targets.Contains(hitGo);
                        if (!enemyAlreadyTarget)
                        {
                            PlayAudioClip();
                            hitGo.GetComponent<EnemyController>().SetActiveTarget();
                            targets.Add(hitGo);
                        }
                    }

                    
                }

            }
        }
        else
        {
            mouseIsDown = false;
        }


        
    }

}
