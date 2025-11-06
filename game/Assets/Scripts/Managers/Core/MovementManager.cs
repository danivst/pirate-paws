using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    public static MovementManager main;

    [Header("Properties")]
    public bool canMove = true;

    [Header("Settings")]
    public float moveSpeed = 3f;
    public float steerPower = 2f;
    public float smoothStopping = 0.1f;

    [Header("Effects")]
    public float maxSteerAngle = 7f;
    public float maxBoatMotorAngle = 5f;
    public Transform boatMotor;
    public ParticleSystem motorParticles;

    [Header("Data")]

    public Rigidbody rb;
    public Health health;

    private void Awake()
    {
        main = this;
    }

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        health = gameObject.GetComponent<Health>();
    }

    bool stopping = false;
    float stoppingProgress = 0;


    void FixedUpdate()
    {
       
        if (stopping == true)
        {
            rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, stoppingProgress);
            stoppingProgress += smoothStopping * StatsManager.main.MoveSpeed * Time.fixedDeltaTime;

            if (stoppingProgress >= 1 || rb.velocity == Vector3.zero)
            {
                stopping = false;
                stoppingProgress = 0;
            }
        }
    }

    public void HandleMovement()
    {
        var emissionMotor = motorParticles.emission;
        // Death
        if (canMove == false || health.IsAlive == false)
        {
            canMove = false;

            rb.velocity = Vector3.zero;

            boatMotor.localEulerAngles = new Vector3(-90, 0, 0);
            CameraManager.main.SetFovFactor(-1);
            emissionMotor.enabled = false;

            return;
        }

        Vector3 currentAngles = transform.localEulerAngles;

        // Stopping
        if (InputManager.main.directionVector == Vector3.zero)
        {
            stopping = true;
            currentAngles.z = 0;

            transform.localEulerAngles = currentAngles;

            boatMotor.localEulerAngles = new Vector3(-90, 0,0);
            CameraManager.main.SetFovFactor(-1);
            emissionMotor.enabled = false;

            return;
        }
        //

        if (stopping == true)
        {
            stopping = false;
            stoppingProgress = 0;
        }
        
        Vector3 inputVector = new Vector3(-InputManager.main.directionVector.x, 0, InputManager.main.directionVector.y);

        // Get the power of movement (0-1 value that is how much the user pulls the joystick)
        float moveFactor = Mathf.Clamp(InputManager.main.distance, 0, InputManager.main.joystickRadius);
        moveFactor /= InputManager.main.joystickRadius;
        moveFactor = Mathf.Pow(moveFactor, 0.5f);
        //

        // Create the FOV zoom out effect (so you *feel* the speed)
        CameraManager.main.SetFovFactor(1);

        Vector3 velocity = transform.forward * moveFactor * moveSpeed * StatsManager.main.MoveSpeed;

        Vector3 steerAngle = new Vector3(0, (steerPower * (StatsManager.main.MoveSpeed/2) * inputVector.x) * moveFactor);

        currentAngles.z = inputVector.x * maxSteerAngle * moveFactor;
        boatMotor.localEulerAngles = new Vector3(-90,0, -inputVector.x * maxBoatMotorAngle * moveFactor) ;


        emissionMotor.enabled = true;

        // Set the values
        transform.localEulerAngles = currentAngles;
        transform.localEulerAngles += steerAngle;

        rb.velocity = velocity;
    }
}
