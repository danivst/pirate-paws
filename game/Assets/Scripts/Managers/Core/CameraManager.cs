using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraManager : MonoBehaviour
{
    public static CameraManager main;

    // Inspector stuff
    [Header("Object to follow")]
    public Transform followObject;
    public bool Enabled = true;

    [Header("Settings")]
    public float smoothTime = 1f;
    public Vector3 offSet = new Vector3(0, 18, 0);

    [Header("Effects Settings")]
    public float FOV = 60;
    public float minFOV = 40;
    public float maxFOV = 80;

    public float DefaultFOVTime = 0.3f;
    bool FovChanging = false;

    public float maxAngle = 15;
    public float CameraShakeTime = 0.05f;
    //
    Vector3 defaultRotation;
    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        defaultRotation = transform.rotation.eulerAngles;
        Camera.main.fieldOfView = FOV;
    }
    //Smooth Move
    public void SetTransform(Transform transform, float time, Ease ease = Ease.Linear)
    {
        this.transform.DOMove(transform.position, time).SetEase(ease);
        this.transform.DORotateQuaternion(transform.rotation, time).SetEase(ease);
    }
    // FOV
    public void SetFov(float fov, float duration = -1f)
    {
        if (!Camera.main || FovChanging == true && fov != FOV)
        {
            return;
        }

        if (duration < 0)
        {
            duration = DefaultFOVTime;
        }

        FovChanging = true;
        Tween tween = DOTween.To(() => Camera.main.fieldOfView, x => Camera.main.fieldOfView = x, fov, duration);
        tween.onComplete = () =>
        {
            FovChanging = false;
        };
        tween.onKill = () =>
        {
            FovChanging = false;
        };
    }

    public void SetFovFactor(float factor, float duration = -1f)
    {
        if (duration < 0)
        {
            duration = DefaultFOVTime;
        }

        if (factor < 0)
        {
            SetFov(FOV, duration);
        }
        else
        {

            SetFov(Mathf.Clamp(maxFOV * factor, minFOV, maxFOV), duration);
        }
    }

    // CameraShake 
    public void CameraShake(float duration, float strength) // TO DO Use DOTween
    {
        float startRotation = transform.localEulerAngles.y;
        float targetRotation = strength * maxAngle;

        // Use shortest path for the initial shake
        DOTween.To(() => transform.localEulerAngles.y,
                   rotation => SetRotationY(rotation),
                   GetShortRotation(startRotation, targetRotation),
                   CameraShakeTime)
        .onComplete = () =>
        {
            DOTween.To(() => transform.localEulerAngles.y,
                       rotation => SetRotationY(rotation),
                       GetShortRotation(startRotation, -targetRotation),
                       CameraShakeTime)
            .onComplete = () =>
            {
                DOTween.To(() => transform.localEulerAngles.y,
                           rotation => SetRotationY(rotation),
                           GetShortRotation(transform.localEulerAngles.y, startRotation),
                           CameraShakeTime)
                .onComplete = () =>
                {
                    duration -= CameraShakeTime * 4;

                    if (duration > 0)
                    {
                        CameraShake(duration, strength / 4);
                    }
                };
            };
        };
    }

    private float GetShortRotation(float currentRotation, float targetRotation)
    {
        return Mathf.DeltaAngle(currentRotation, targetRotation) + currentRotation;
    }

    private void SetRotationY(float y)
    {
        Vector3 angles = transform.localEulerAngles;
        angles.y = y;
        transform.localEulerAngles = angles;
    }

    void FixedUpdate()
    {
        if (Enabled == true && transform.position != followObject.transform.position + offSet)
        {
            transform.position = Vector3.Lerp(transform.position, followObject.transform.position + offSet, smoothTime * Time.fixedDeltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(defaultRotation), smoothTime * Time.fixedDeltaTime);
        }
    }
}
