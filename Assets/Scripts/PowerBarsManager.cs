using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class PowerBarsManager : MonoBehaviour
{
    //string[] colors = { "Red", "Yellow", "Green", "Blue" };
    public Slider powerBarSingle;
    public Slider powerBarSingleWithHandle ;
    public float pointerSpeed = 50;
    public Color[] incomingColors = new Color[] { };
    public Color[] shuffledColors;
    //public TextMeshProUGUI incomingColorText;
    public GameLogicScript gameLogicScript;
    public ColorUtilsScript colorUtilsScript;
    public Image ball;
    private Color incomingBallColor;
    PowerBarController powerBarController;
    [SerializeField]

    private Color[] colors = new Color[]
   {
        Color.red,  // Red
        Color.yellow, // Yellow
        Color.blue,   // Blue
        Color.black,
        Color.green,   // Green
   };
    private bool isRotating = true;
    private int selectedPointerIndex;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        gameLogicScript = GameObject.FindGameObjectWithTag("Logic").GetComponent<GameLogicScript>();
        colorUtilsScript = GameObject.FindGameObjectWithTag("Logic").GetComponent<ColorUtilsScript>();

        //set pointer speed by level
        pointerSpeed = pointerSpeed + gameLogicScript.level * 2;

        Color[] shuffledIncomingColors = colorUtilsScript.ShuffleArray(colors);
        incomingColors = shuffledIncomingColors;
        incomingBallColor = incomingColors[UnityEngine.Random.Range(0, incomingColors.Length)];
        Debug.Log("incoming colors 1" + colorUtilsScript.GetColorName(incomingBallColor));
        //if (elapsedTime < duration)
        //{
        //    elapsedTime += Time.deltaTime;
        //    float t = elapsedTime / duration;  // Calculate interpolation factor
        //    ball.color = Color.Lerp(incomingBallColor, incomingBallColor, t);  // Gradually change the color
        //}
        ball.color = incomingBallColor;
        //incomingColorText.text = "Incoming Ball : "+ colorUtilsScript.GetColorName(incomingBallColor).ToString();
        Debug.Log("incoming colors 2" + colorUtilsScript.GetColorName(incomingBallColor));
        shuffledColors = colorUtilsScript.ShuffleArray(colors);
        float totalColor = shuffledColors.Length;
        powerBarSingle.maxValue = totalColor;
        float temptValue = totalColor/2;
        int index = 0;
        foreach (Color color in shuffledColors)
        {
            Slider clonePowerBarSingle = Instantiate(powerBarSingle,transform);
            clonePowerBarSingle.interactable = false;
            Transform slideArea = clonePowerBarSingle.transform.Find("Handle Slide Area");
            Transform background = clonePowerBarSingle.transform.Find("Background");
            if (index != totalColor-1)
            {
                slideArea.gameObject.SetActive(false);
                background.gameObject.SetActive(false);
                //Destroy(slideArea.gameObject);
            }
            else
            {
                //slideArea.position = new Vector3(0,0,0);

                powerBarSingleWithHandle = clonePowerBarSingle;
            }

            if (clonePowerBarSingle.fillRect != null)
            {
                Image fillImage = clonePowerBarSingle.fillRect.GetComponent<Image>();

                if (fillImage != null)
                {
                    // Set the color of the fill
                    fillImage.color = color;
                }
            }
      
           
            clonePowerBarSingle.value = temptValue;
            Debug.Log("value"+ temptValue);
            temptValue = temptValue - 0.5f;
            index++;
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        float maxZ = -90f;
        float minZ = 90f;
        powerBarController = powerBarSingleWithHandle.GetComponent<PowerBarController>();

        if (isRotating)
        {
            // Rotate object (example rotation)
            float zPosition = Mathf.PingPong(Time.time* pointerSpeed, minZ - maxZ) +maxZ  ;
            powerBarController.handleSlideArea.transform.localEulerAngles = new Vector3(0, 0, zPosition);
            //powerBarSingleWithHandle.handleRect.transform.localEulerAngles = new Vector3(0, 0, zPosition);
        }
        
        if (Input.anyKeyDown)
        {
            isRotating = false;
            // z rotation value needed to be -90 to 90
            float zRotation = parseEulerZ(powerBarController.handleSlideArea.transform.localEulerAngles.z);
            setSelectedIndex(zRotation);
            Color selectedColor = colorUtilsScript.getSelectedColor(selectedPointerIndex,shuffledColors);
            bool isSameColor = colorUtilsScript.checkIsSameColor(incomingBallColor, selectedColor);
            //incomingColorText.text = "Hit status :  " + isSameColor; 
            if (!isSameColor)
            {
                Debug.Log("GAme over");
                //gameObject.SetActive(false); 
                gameLogicScript.gameOver();
            }
            else {
                gameLogicScript.hitSuccess(10);
                isRotating = true;
            }
        }
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    isRotating = true;
        //}
    }

    float[] getPointerPositionArray(float start, float decrement, int length)
    {
        float[] array = new float[length+1];
        for (int i = 0; i < length+1; i++)
        {
            array[i] = start + i * decrement;
        }
        return array;
    }
    void setSelectedIndex(float pointerZ )
    {
        float startValue = -90f; // Starting value
        int count = colors.Length; // Number of elements in the array
        float increment = 180f/count; // Decrement value
        float[] array = getPointerPositionArray(startValue, increment, count);
        Debug.Log("colors positions" + string.Join(", ", array));
        int index = 0;
        
        foreach (float i in array)
        {
                if (pointerZ >= array[index])
                {
                    selectedPointerIndex = index ;
                    Debug.Log("selectedPointerIndex" + selectedPointerIndex + "and array item" + array[index]);
                }
                index++;
        }    
    }
    float parseEulerZ(float localEulerZ)
    {
        if (localEulerZ >= 270 && localEulerZ <= 360)
        {
            return localEulerZ - 360;
        }
        else return localEulerZ;
    }
    public void Reset()
    {
        
    }
}
