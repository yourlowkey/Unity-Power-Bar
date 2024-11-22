using System.Collections;
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
    void Start()
    {
        //ballColorScript = GameObject.FindGameObjectWithTag("Logic").GetComponent<ColorUtilsScript>();
        ball = gameObject.GetComponent<Image>();
        gameLogicScript = GameObject.FindGameObjectWithTag("Logic").GetComponent<GameLogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ball != null && ball.fillAmount > 0)
        {
            // Reduce the fill amount over time
            ball.fillAmount -= reduceSpeed * Time.deltaTime;

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
}
