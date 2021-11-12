using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static int highScore;

    public Text scoreTxt;

    //Used to check if the game has already ended
    private bool gameHasEnded = false;

    public float restartTime = 1f;
    public void GameOver()
    {
        if (!gameHasEnded)
        {
            gameHasEnded = true;
            
            Invoke("Restart", restartTime);
        }
        
    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Update()
    {
        //Update the score counter
        scoreTxt.text = ("Score: " + GameManager.highScore.ToString());
        if(highScore <= 0){
            highScore = 0;
        }
        if(highScore >= 1000)
        {
            AchievementManager.Instance.EarnAchievement("Coin Master");
        }
    }
}
