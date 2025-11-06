using Assets.Scripts;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchableMovement : MonoBehaviour
{
    [Header("Struggle when being catched")]
    public bool struggle = false;
    public float struggleSpeedMultiplier = 2f;
    [Header("Radius within which the object moves")]

    public bool radiusEnabled = false;
    public float radius = 5;
    [Header("Random radius settings")]
    public bool randomRadius = false;
    public int minRandomRadius = 1;
    public int maxRandomRadius = 5;

    private int randomRange;

    [Header("Direction")]
    public bool fixedDirection = false;
    public Vector3 direction;

    [Header("Settings")]
    public float moveSpeed = 1;
    public float interval = 1;
    public float rotationTime = 0.5f;
    [Header("Data")]
    public Rigidbody rb;
    public Transform catchableModels;
    public ICatchable catchable;
    public Vector3 spawnPoint;

    private float timer = 0f;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        catchable = gameObject.GetComponent<ICatchable>();

        spawnPoint = transform.position;

        if (radiusEnabled && randomRadius)
        {
            randomRange = UnityEngine.Random.Range(minRandomRadius, maxRandomRadius);
        }
    }

    public Vector3 GetDirection()
    {
        // Return FixedDirection
        if (fixedDirection)
        {
            return direction;
        }
        // Get the correct radius
        float radius = this.radius;
        if (randomRadius)
        {
            radius = randomRange;
        }
        // Handle if outside of radius
        float distanceFromCenter = (transform.position - spawnPoint).magnitude;
        if (radiusEnabled && distanceFromCenter > radius)
        {
            return -(transform.position - spawnPoint).normalized;
        }
        Vector2 randomDirection = RandomUtils.RandomVector2(1).normalized;

        float angle = Mathf.Rad2Deg * UnityEngine.Random.Range(0f, Mathf.PI * 2f);

        if (radiusEnabled)
        {
            return (new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius).normalized;
        }
        // Return random Movement

        return new Vector3(randomDirection.x,0, randomDirection.y);
    }

    public float GetSpeed()
    {
        if (struggle && catchable.TimeLeft <= catchable.TimeToCatch)
        {
            return moveSpeed * struggleSpeedMultiplier;
        }
        return moveSpeed;
    }

    void FixedUpdate()
    {
        if (timer >= interval && catchable.TimeLeft > 0 && catchableModels != null)
        {
            float speed = GetSpeed();

            Vector3 targetDirection = GetDirection() * speed;

            rb.velocity = new Vector3(targetDirection.x, rb.velocity.y, targetDirection.z);
         
            catchableModels.DOLookAt(transform.position + rb.velocity, rotationTime);
       
            timer = 0f;
        }
        timer += Time.fixedDeltaTime;
    }
}