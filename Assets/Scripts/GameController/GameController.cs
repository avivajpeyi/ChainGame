using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using UnityEngine.UI;
using static EnemyMaster.EnemyType;

using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour
{

    // the int ID of the enemy type that the player can grapple to 
    public EnemyMaster.EnemyType grappleEnemyType = GRAPPLING;
    public Canvas gameOverCanvas;
    


    public void GameOver()
    {
        gameOverCanvas.gameObject.SetActive(true);
    }


    public void RestartScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }


}
