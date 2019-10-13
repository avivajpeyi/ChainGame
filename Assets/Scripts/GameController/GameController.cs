using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using UnityEngine.UI;


using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour
{

    // the int ID of the enemy type that the player can grapple to 
    public int grappleEnemyType = 0;
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
