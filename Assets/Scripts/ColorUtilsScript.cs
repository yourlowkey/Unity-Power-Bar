using UnityEngine;

public class ColorUtilsScript : MonoBehaviour
{
    [SerializeField]
  //  public Color[] colors = new Color[]
  //{
  //      Color.red,  // Red
  //      Color.yellow, // Yellow
  //      Color.blue,   // Blue
  //      Color.black,
  //      Color.green,   // Green
  //};
    public Color[] ShuffleArray(Color[] array)
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
    public Color getSelectedColor(int index, Color[] colorsArray)
    {
        return colorsArray[index];
    }
    public bool checkIsSameColor(Color incomingColor, Color selectedColor)
    {
        return incomingColor.Equals(selectedColor);
    }
    public string GetColorName(Color color)
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
}
