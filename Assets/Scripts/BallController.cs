using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{
    //public Color ballColor;
    //public ColorUtilsScript ballColorScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Image ball;
    public float reduceSpeed = 0.2f;
    public GameLogicScript gameLogicScript;
    public TextMeshProUGUI timeText;
    private float reduceAcceleration = 0.07f;
    private float duration;
    public float thisLevelDuration;
    public float timeRemaining;
    private float targetFillAmount = 0f;
    private float minDuration = 3f; // Minimum duration (e.g., 2.5 seconds)
    private float decayRate = 0.1f; // Decay rate constant (adjust for speed)
    private float timeElapsed = 0f;

    void Start()
    {
        //ballColorScript = GameObject.FindGameObjectWithTag("Logic").GetComponent<ColorUtilsScript>();
        ball = gameObject.GetComponent<Image>();
        gameLogicScript = GameObject.FindGameObjectWithTag("Logic").GetComponent<GameLogicScript>();
        
        //reduceSpeed = gameLogicScript.level* reduceAcceleration + reduceSpeed;
        duration =gameLogicScript.ballDuration;
        thisLevelDuration = CalculateDuration(gameLogicScript.level);
        
        //StartCoroutine(ReduceFillAmount());
    }

    // Update is called once per frame
    void Update()
    {   
        timeElapsed += Time.deltaTime;
         timeRemaining = thisLevelDuration - timeElapsed;
        timeText.text = Math.Round(timeRemaining, 1).ToString();
        if (ball != null && ball.fillAmount > 0)
        {
            // Reduce the fill amount over time
            ball.fillAmount = Mathf.Lerp(1f, 0f, timeElapsed / thisLevelDuration);

        }
        else {
            gameLogicScript.gameOverByTime();
        }
        //IEnumerator FillColor()
        //{
        //    Color initialColor = ball.color;  // The starting color
        //    float elapsedTime = 0f;
        //    float duration = 3f;

        //    while (elapsedTime < duration)
        //    {
        //        elapsedTime += Time.deltaTime;
        //        float t = elapsedTime / duration;  // Calculate the interpolation factor (0 to 1)
        //        ball.color = Color.Lerp(initialColor, Color.white, t);  // Gradually change the color
        //        yield return null;  // Wait for the next frame
        //    }

        //    // Ensure the final color is set
        //    //targetImage.color = targetColor;
    }
    //private IEnumerator ReduceFillAmount()
    //{
    //    float startFill = ball.fillAmount; // Starting fill amount
    //    float elapsedTime = 0f; // Timer

    //    while (elapsedTime < duration)
    //    {
    //        elapsedTime += Time.deltaTime;
    //        // Linearly interpolate the fill amount
    //        ball.fillAmount = Mathf.Lerp(startFill, targetFillAmount, elapsedTime / duration);
    //        yield return null; // Wait for the next frame
    //    }

    //    // Ensure fill amount is set to the target value
    //    ball.fillAmount = targetFillAmount;
    //}
    private float CalculateDuration(int level)
    {
        // Apply the exponential decay formula
        return minDuration + (duration - minDuration) * Mathf.Exp(-decayRate * level);
    }
}
