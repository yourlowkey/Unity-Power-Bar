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

        Color[] shuffledIncomingColors = colorUtilsScript.ShuffleArray(colors);
        incomingColors = shuffledIncomingColors;
        incomingBallColor = incomingColors[UnityEngine.Random.Range(0, incomingColors.Length)];
        Debug.Log("incoming colors 1" + colorUtilsScript.GetColorName(incomingBallColor));
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
            if(index != 0)
            {
                slideArea.gameObject.SetActive(false);
                //Destroy(slideArea.gameObject);
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
      
            if(index == 0)
            {
                powerBarSingleWithHandle = clonePowerBarSingle;
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

        if (isRotating)
        {
            // Rotate object (example rotation)
            float zPosition = Mathf.PingPong(Time.time* pointerSpeed, minZ - maxZ) +maxZ  ;
            powerBarSingleWithHandle.handleRect.transform.localEulerAngles = new Vector3(0, 0, zPosition);
        }
        
        if (Input.anyKeyDown)
        {
            isRotating = false;
            // z rotation value needed to be -90 to 90
            float zRotation = parseEulerZ(powerBarSingleWithHandle.handleRect.transform.localEulerAngles.z);
            setSelectedIndex(zRotation);
            Color selectedColor = colorUtilsScript.getSelectedColor(selectedPointerIndex,shuffledColors);
            bool isSameColor = colorUtilsScript.checkIsSameColor(incomingBallColor, selectedColor);
            //incomingColorText.text = "Hit status :  " + isSameColor; 
            if (!isSameColor)
            {
                Debug.Log("GAme over");
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
