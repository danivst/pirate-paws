using UnityEngine;

public class Fps : MonoBehaviour
{
    [Range(1, 120)] // Create a slider in the Inspector with values from 1 to 120
    public int fps = 60; // Default FPS value
  

    private void Update()
    {
        Application.targetFrameRate = fps;
    }
}
