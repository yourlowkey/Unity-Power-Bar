using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using System;
using UnityEngine.Windows;
using System.Collections;
using Unity.VisualScripting;

public class GameLogicScript : MonoBehaviour
{
    public int levelScore;
    public GameObject gameOverScreen;
    public TMPro.TextMeshProUGUI scoreText;
    public GameObject powerBarAndBall;
    public GameObject levelScene;
    public Canvas hitSuccessWrapper;
    public Canvas hitFailWrapper;
    public Canvas hitTimeoutWrapper;
    public int levelBallComing = 5;
    private int levelBallComingCount = 0;
    private GameObject currentPowerBarAndBall;

    void Start()
    {
        currentPowerBarAndBall = Instantiate(powerBarAndBall,levelScene.transform);
        currentPowerBarAndBall.SetActive(true);
        //gameOverScreen= Instantiate(gameOverScreen);
        //gameOverScreen.SetActive(false);
    }
    public void hitSuccess (int scoreToAdd)
    {
        levelScore += scoreToAdd;
        scoreText.text = levelScore.ToString();
        Destroy(currentPowerBarAndBall);
        levelBallComingCount += 1;
        if(levelBallComingCount == levelBallComing)
        {
            //out level logic
        }
        //set active ball hit status UI
        hitSuccessWrapper.gameObject.SetActive(true);
        //re instantiate ball and bar wrapper
        StartCoroutine(activeBallAndBarAfterTimeout(1.5f));
    }
    public void restartGame()
    {
        Debug.Log("Gamne restart");
        //Destroy(gameOverScreen);
        gameOverScreen.SetActive(false);
        Destroy(currentPowerBarAndBall);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
        //re instantiate ball and bar wrapper
        StartCoroutine(activeBallAndBarAfterTimeout(0f));
    }
    public void gameOver()
    {
        levelScore = 0;
        scoreText.text = levelScore.ToString();
        currentPowerBarAndBall.SetActive(false);
        // set active miss ball anim
        hitFailWrapper.gameObject.SetActive(true);
        StartCoroutine(missBallUIAfterTimeout(1.5f));
    }
    public void gameOverByTime()
    {
        levelScore = 0;
        scoreText.text = levelScore.ToString();
        currentPowerBarAndBall.SetActive(false);
        //set time out anim
        hitTimeoutWrapper.gameObject.SetActive(true);
        StartCoroutine(ballTimeoutUIAfterTimeout(1.5f));

    }

    IEnumerator missBallUIAfterTimeout(float timeout)
    {
        // Wait for the specified time
        yield return new WaitForSeconds(timeout);

        // Code to execute after the timeout
        hitFailWrapper.gameObject.SetActive(false);
        gameOverScreen.SetActive(true);


    }
    IEnumerator activeBallAndBarAfterTimeout(float timeout)
    {
        // Wait for the specified time
        yield return new WaitForSeconds(timeout);

        // Code to execute after the timeout
        hitSuccessWrapper.gameObject.SetActive(false);
        currentPowerBarAndBall = Instantiate(powerBarAndBall, levelScene.transform);
        currentPowerBarAndBall.SetActive(true);
    }
    IEnumerator ballTimeoutUIAfterTimeout(float timeout)
    {
        // Wait for the specified time
        yield return new WaitForSeconds(timeout);

        // Code to execute after the timeout
        hitTimeoutWrapper.gameObject.SetActive(false);
        gameOverScreen.SetActive(true);


    }

}
