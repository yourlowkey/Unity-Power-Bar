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
        //powerBarAndBall.SetActive(false);
        Destroy(currentPowerBarAndBall);
        //set active ball hit status UI

        //re instantiate ball and bar wrapper
        StartCoroutine(activeBallAndBarAfterTimeout(1.5f));
    }
    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log("Gamne restart");
        //Destroy(gameOverScreen);
        gameOverScreen.SetActive(false);
        Destroy(currentPowerBarAndBall);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //set active ball hit status UI

        //re instantiate ball and bar wrapper
        StartCoroutine(activeBallAndBarAfterTimeout(0f));
    }
    public void gameOver()
    {
        levelScore = 0;
        scoreText.text = levelScore.ToString();
        //powerBarAndBall.SetActive(false);
        //Instantiate(gameOverScreen);
        gameOverScreen.SetActive(true);
    }

    IEnumerator activeBallAndBarAfterTimeout(float timeout)
    {
        // Wait for the specified time
        yield return new WaitForSeconds(timeout);

        // Code to execute after the timeout
        currentPowerBarAndBall = Instantiate(powerBarAndBall, levelScene.transform);
        currentPowerBarAndBall.SetActive(true);
    }
}
