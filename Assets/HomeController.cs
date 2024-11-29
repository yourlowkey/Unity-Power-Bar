using UnityEngine;

public class HomeController : MonoBehaviour
{
    public GameObject levelScene;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            gameObject.SetActive(false);
            levelScene.SetActive(true);
        }
    }
}
