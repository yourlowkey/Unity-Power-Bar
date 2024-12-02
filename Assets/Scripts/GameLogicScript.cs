using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameLogicScript : MonoBehaviour
{
    [SerializeField]
    public int levelScore;
    public GameObject gameOverScreen;
    public TMPro.TextMeshProUGUI scoreText;
    public GameObject powerBarAndBall;
    public GameObject levelScene;
    public GameObject ball;
    public Canvas hitSuccessWrapper;
    public Canvas hitFailWrapper;
    public Canvas hitTimeoutWrapper;
    public Canvas levelCompleteWrapper;
    //get level from local storage
    public int level = 0;
    public int levelBallComing = 3;
    public float ballDuration = 6.0f;
    public List<Sprite> hitStatusSprite ;
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
        ball = currentPowerBarAndBall.transform.Find("Ball").gameObject;
        BallController ballScript = ball.GetComponent<BallController>();
        Debug.Log("current duration : " + ballScript.thisLevelDuration + "remaining time" + ballScript.timeRemaining);
       
        setHitStatusUI(ballScript.timeRemaining, ballScript.thisLevelDuration);
        levelScore += scoreToAdd;
        scoreText.text = levelScore.ToString();
        Destroy(currentPowerBarAndBall);
        levelBallComingCount += 1;
        //set active ball hit status UI
        hitSuccessWrapper.gameObject.SetActive(true);
        if (levelBallComingCount == levelBallComing)
        {
            //out level logic
            StartCoroutine(levelCompleteAfterTimeout(1.5f));
        }else
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
        //levelScore = 0;
        scoreText.text = levelScore.ToString();
        currentPowerBarAndBall.SetActive(false);
        // set active miss ball anim
        hitFailWrapper.gameObject.SetActive(true);
        StartCoroutine(missBallUIAfterTimeout(1.5f));
    }
    public void gameOverByTime()
    {
        //levelScore = 0;
        scoreText.text = levelScore.ToString();
        currentPowerBarAndBall.SetActive(false);
        //set time out anim
        hitTimeoutWrapper.gameObject.SetActive(true);
        StartCoroutine(ballTimeoutUIAfterTimeout(1.5f));

    }
    public void nextLevel()
    {
        level++;
        levelCompleteWrapper.gameObject.SetActive(false);
        currentPowerBarAndBall = Instantiate(powerBarAndBall, levelScene.transform);
        currentPowerBarAndBall.SetActive(true);
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
    IEnumerator levelCompleteAfterTimeout(float timeout)
    {
        // Wait for the specified time
        yield return new WaitForSeconds(timeout);

        // Code to execute after the timeout
        hitSuccessWrapper.gameObject.SetActive(false);
        levelBallComingCount = 0;
        levelCompleteWrapper.gameObject.SetActive(true);
    }
    string getHitStatus (float timeRemaining , float duration)
    {
        if (timeRemaining > duration * 3 / 4 && timeRemaining <= duration)
        {
            return "PERFECT";
        }
        if (timeRemaining > duration * 1 / 2 && timeRemaining <= duration * 3 / 4)
        {
            return "GREAT";
        }

        if (timeRemaining > duration * 1 / 4 && timeRemaining <= duration * 1 / 2)
        {
            return "GOOD";
        }
        else return "NOTBAD";
    }
    void setHitStatusUI(float timeRemaining, float duration)
    {
        Image hitSuccessUI = hitSuccessWrapper.transform.Find("Image").GetComponent<Image>();
        Sprite hitSuccessSprite= hitSuccessUI.sprite;
        //switch (getHitStatus(timeRemaining, duration))
        //{
        //    case "NOTBAD":
        //        Debug.Log("hitttttt");
        //        hitSuccessSprite = hitStatusSprite[0]; break;
        //    case "GOOD":
        //        Debug.Log("hitttttt");
        //        hitSuccessSprite = hitStatusSprite[1]; break;
        //    case "GREAT":
        //        Debug.Log("hitttttt");
        //        hitSuccessSprite = hitStatusSprite[2]; break;
        //    case "PERFECT":
        //        Debug.Log("hitttttt");
        //        hitSuccessSprite = hitStatusSprite[3]; break;
        //}
        string hitStatus = getHitStatus(timeRemaining , duration);
        if (hitStatus=="NOTBAD")
        {
            Debug.Log("hitttttt" + hitStatus);
            hitSuccessUI.sprite = hitStatusSprite[3];
            hitSuccessUI.SetNativeSize();
        }
        if (hitStatus == "GOOD")
        {
            Debug.Log("hitttttt" + hitStatus);
            hitSuccessUI.sprite = hitStatusSprite[2];
            hitSuccessUI.SetNativeSize();
        }
        if (hitStatus == "GREAT")
        {
            Debug.Log("hitttttt" + hitStatus);
            hitSuccessUI.sprite = hitStatusSprite[1];
            hitSuccessUI.SetNativeSize();
        }
        if (hitStatus == "PERFECT")
        {
            Debug.Log("hitttttt" + hitStatus);
            hitSuccessUI.sprite = hitStatusSprite[0];
            hitSuccessUI.SetNativeSize();

        }


        TextMeshProUGUI textMeshProUGUI = hitSuccessWrapper.transform.Find("HitSuccessText").GetComponent<TextMeshProUGUI>();
        textMeshProUGUI.text = getHitStatus(timeRemaining, duration);
        
        //hitSuccessUI.color = Color.black;
        //}
    }

}
