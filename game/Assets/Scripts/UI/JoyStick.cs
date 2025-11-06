using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyStick : MonoBehaviour
{
    public static JoyStick main;
    public bool Active = true;

    public RectTransform JoystickContainer;
    public RectTransform Joystick;

    private void Awake()
    {
        main = this;
    }
    void Update()
    {
        if (Active == false)
        {
            return;
        }
        float multyFactor = (Mathf.Clamp(InputManager.main.distance, 0, InputManager.main.joystickRadius) / InputManager.main.joystickRadius) * 40;
        
        Vector3 targetVector = new Vector3(-InputManager.main.directionVector.x, InputManager.main.directionVector.y) * multyFactor;
    
        Joystick.localPosition = targetVector;
    }
}
