using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    public MovementManager movementManager;

    public Vector3 position;

    public Vector3 centerJoystick = new Vector3(0, -0.625f);
    public float joystickRadius = 0.1f;

    private float width;
    private float height;

    [Header("Data")]
    public Vector3 directionVector = new Vector3();
    public float distance = 0f;

    public static InputManager main;

    void Awake()
    {
        width = (float)Screen.width / 2.0f;
        height = (float)Screen.height / 2.0f;

        position = new Vector3(0.0f, 0.0f, 0.0f);

        main = this;
    }

 

    void Update()
    {
        // Handle screen touches
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (EventSystem.current.currentSelectedGameObject != null)
            {
                if (!EventSystem.current.currentSelectedGameObject.name.Contains("Joystick"))
                {
                    return;
                }
            }
            // Update the position if the finger is touching the screen
            if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Stationary)
            {
                UpdateDirectionVector(touch);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                directionVector = Vector3.zero;
                distance = 0;
                movementManager.HandleMovement();
            }
        }
    }

    private void FixedUpdate()
    {
        if (directionVector != Vector3.zero && distance > 0)
        {
            movementManager.HandleMovement();
        }
    }

    void UpdateDirectionVector(Touch touch)
    {
        Vector2 pos = touch.position;
        pos.x = (pos.x - width) / width;
        pos.y = (pos.y - height) / height;
        position = new Vector3(-pos.x, pos.y, 0.0f);

        Vector3 directionVector = (position - centerJoystick).normalized;
        this.directionVector = directionVector;

        distance = (centerJoystick - position).magnitude;
    }
}
