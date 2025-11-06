using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFloat : MonoBehaviour
{
    [Header("Float Position")]
    public float positionSpeed = 1;
    public float waveSize = 1;
    public float horizon = 0.63f;

    [Header("Float Rotation (Wobble)")]
    public float rotationSpeed = 1;
    public float rotationMax = 15;

    void FixedUpdate()
    {
        // sin ( (x + time + z + time) * speed)
        Vector3 pos = transform.localPosition;

        float y = horizon + Mathf.Sin((pos.x + pos.z + Time.unscaledTime * 2) * positionSpeed) * waveSize;

        float rotX = Mathf.Sin((pos.x + pos.z + Time.unscaledTime * 2 * rotationSpeed) * rotationSpeed) * rotationMax;
        float roxZ = Mathf.Sin((pos.x + pos.z + Time.unscaledTime * 4 * rotationSpeed) * rotationSpeed) * rotationMax;

        transform.localEulerAngles = new Vector3(rotX, 0, roxZ);
        transform.localPosition = new Vector3(pos.x, y, pos.z);
    }
}
