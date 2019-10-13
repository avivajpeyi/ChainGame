using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using UnityEngine.UI;


using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour
{
    
    
    public Canvas gameOverCanvas;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


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
