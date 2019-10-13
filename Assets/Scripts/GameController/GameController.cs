using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using UnityEngine.UI;
using static EnemyMaster.EnemyType;

using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour
{
    public bool isGameOver = false;
    
    // the int ID of the enemy type that the player can grapple to 
    public EnemyMaster.EnemyType grappleEnemyType = GRAPPLING;
    public GameObject gameOverpanel;
    


    public void GameOver()
    {
        isGameOver = true;
        gameOverpanel.SetActive(true);
    }


    public void RestartScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }


}
