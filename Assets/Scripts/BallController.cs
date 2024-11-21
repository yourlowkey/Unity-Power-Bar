using UnityEngine;

public class BallController : MonoBehaviour
{
    //public Color ballColor;
    public ColorUtilsScript ballColorScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ballColorScript = GameObject.FindGameObjectWithTag("Logic").GetComponent<ColorUtilsScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
