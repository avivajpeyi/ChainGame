using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using UnityEngine.UI;
using static EnemyMaster.EnemyType;

using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour
{
    
    public TMPro.TextMeshProUGUI scoreTxt;
    public TMPro.TextMeshProUGUI chainTxt;
    public TMPro.TextMeshProUGUI highScoreTxt;
    public TMPro.TextMeshProUGUI currentScoreTxt;
    public int chainScore; 
    public int score;
    private int highScore = 0;
    public enum DeadEyeType
    {
        FORCE,
        VELOCITY
    };
    
    public bool isGameOver = false;
    
    // the int ID of the enemy type that the player can grapple to 
    public EnemyMaster.EnemyType grappleEnemyType = GRAPPLING;
    public GameObject gameOverpanel;
    private ChainMove ChainMove;

    public void Start()
    {
        ChainMove = FindObjectOfType<ChainMove>();
    }

    public void IncreaseScore()
    {
        if (!isGameOver)
        {
            chainScore += 1;
            
            if (ChainMove.Chainmode)
                score += chainScore;
            else
                score += 1;
        }
    }

    private void Update()
    {
        scoreTxt.text="SCORE : " + getScoreString(score);
        if (ChainMove.Chainmode)
            chainTxt.text = "x" + chainScore;
        else
        {
            chainTxt.text = "";
            chainScore = 0;
        }
    }

    void updateHighScore()
    {
        highScore = PlayerPrefs.GetInt("highscore", 0);
        if (highScore < score)
        {
            highScore = score;
            PlayerPrefs.SetInt("highscore", highScore); 
        }
    }

    string getScoreString(int scoreVal)
    {
        // score as a string
        string scoreString = scoreVal.ToString();
        int scoreLength = 3;
        // get number of 0s needed
        int numZeros = scoreLength - scoreString.Length;

        string newScoreStr = "";
        for (int i = 0; i < numZeros; i++)
            newScoreStr += "0";
        newScoreStr += scoreString;

        return newScoreStr;
    }


    public void GameOver()
    {
        updateHighScore();
        scoreTxt.gameObject.SetActive(false);
        highScoreTxt.text="HIGH SCORE:\n" + getScoreString(highScore);
        currentScoreTxt.text="SCORE:\n" + getScoreString(score);
        isGameOver = true;
        gameOverpanel.SetActive(true);
    }


    public void RestartScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void SetDeadEyeToForce()
    {
        SetDeadEyePref(DeadEyeType.FORCE);
    }
    
    public void SetDeadEyeToVel()
    {
        SetDeadEyePref(DeadEyeType.VELOCITY);
    }

    public void SetDeadEyePref(DeadEyeType deadEyeType)
    {
        PlayerPrefs.SetInt("DeadEyeType", (int) deadEyeType);
    }
    
    public DeadEyeType GetDeadEyePref()
    {
        return (DeadEyeType) PlayerPrefs.GetInt("DeadEyeType", 1);
    }

}
