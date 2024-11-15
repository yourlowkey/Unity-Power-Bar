using System;
using UnityEngine;
using UnityEngine.UI;
public class PowerBarController : MonoBehaviour
{
    [SerializeField]
    //string[] colors = { "Red", "Yellow", "Green", "Blue" };
    public Slider powerBarSingle;
    public float pointerSpeed = 50;
    private Color[] colors = new Color[]
   {
           // Red
        Color.yellow, // Yellow
        Color.blue,   // Blue
        Color.green   // Green
   };


    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        //foreach (Color color in colors)
        //{
        //    Debug.Log(color);
        //    Slider clonePowerBarSingle = Instantiate(powerBarSingle);
        //    if (clonePowerBarSingle.handleRect != null)
        //    {
        //        //Destroy(clonePowerBarSingle);
        //        clonePowerBarSingle.handleRect = null;
        //    }

        //    if (clonePowerBarSingle.fillRect != null)
        //    {
        //        Image fillImage = clonePowerBarSingle.fillRect.GetComponent<Image>();

        //        if (fillImage != null)
        //        {
        //            // Set the color of the fill
        //            fillImage.color = color;
        //        }
        //    }
        //}
    }

    // Update is called once per frame
    void Update()
    {
        //powerBarSingle.handleRect.transform.Rotate(new Vector3(0, 0, pointerSpeed) * Time.deltaTime);
        //if(powerBarSingle.handleRect.transform.rotation.z >= 136.68)
        //{
        //    goBack();
        //}
        //if (powerBarSingle.handleRect.transform.rotation.z <= -43.532)
        //{
        //    goFort();
        //}
        float maxZ = 136f;
        float minZ = -43f;

        float zPosition = Mathf.PingPong(Time.time * pointerSpeed, maxZ - minZ) +minZ;
        powerBarSingle.handleRect.transform.rotation = Quaternion.Euler(0, 0, zPosition);
    }
    void goBack()
    {
        powerBarSingle.handleRect.transform.Rotate(new Vector3(0, 0, -pointerSpeed) * Time.deltaTime);
    }
    void goFort()
    {
        powerBarSingle.handleRect.transform.Rotate(new Vector3(0, 0, pointerSpeed) * Time.deltaTime);
    }
}
