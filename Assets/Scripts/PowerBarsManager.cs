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
    public Color[] incomingColors = new Color[] { Color.red};
    public Color[] shuffledColors;
    public TextMeshProUGUI incomingColorText;
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
        Color[] shuffledIncomingColors = ShuffleArray(colors);
        incomingColors = shuffledIncomingColors;
        incomingBallColor = incomingColors[UnityEngine.Random.Range(0, incomingColors.Length)];
        //GameObject singleObject = GameObject.FindWithTag("BallLog");
        //incomingColorText = singleObject;
        incomingColorText.text = "Incoming Ball : "+ GetColorName(incomingBallColor).ToString();
        Debug.Log("incoming colors" + GetColorName(incomingBallColor));
        shuffledColors = ShuffleArray(colors);
        float totalColor = shuffledColors.Length;
        powerBarSingle.maxValue = totalColor;
        //powerBarSingle.value = 1;
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
            Color selectedColor = getSelectedColor(selectedPointerIndex,shuffledColors);
            bool isSameColor = checkIsSameColor(incomingBallColor, selectedColor);
            incomingColorText.text = "Hit status :  " + isSameColor;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isRotating = true;
        }
           
    }


    Color[] ShuffleArray(Color[] array)
    {
        System.Random random = new System.Random(); // Use System.Random for consistency
        for (int i = array.Length - 1; i > 0; i--)
        {
            int j = random.Next(i + 1); // Random index between 0 and i
            // Swap elements at i and j
            var temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
        return array;
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
    Color getSelectedColor(int index,Color[] colorsArray) {
        return colorsArray[index];
    }
    bool checkIsSameColor(Color incomingColor, Color selectedColor) {
        return incomingColor.Equals(selectedColor);
    }
    string GetColorName(Color color)
    {
        if (color == Color.red) return "Red";
        if (color == Color.green) return "Green";
        if (color == Color.blue) return "Blue";
        if (color == Color.white) return "White";
        if (color == Color.black) return "Black";
        if (color == Color.yellow) return "Yellow";
        if (color == Color.cyan) return "Cyan";
        if (color == Color.magenta) return "Magenta";

        return "Unknown";
    }
    float parseEulerZ(float localEulerZ)
    {
        if (localEulerZ >= 270 && localEulerZ <= 360)
        {
            return localEulerZ - 360;
        }
        else return localEulerZ;
    }
}
