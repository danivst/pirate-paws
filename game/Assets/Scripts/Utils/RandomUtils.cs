using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RandomUtils
{
    public static Vector2 RandomVector2(float range)
    {
        return new Vector2(Random.Range(-range, range), Random.Range(-range, range));
    }

    public static Vector3 RandomVector3Distance(float distance)
    {
        Vector3 direction = new Vector3(Random.Range(-1, 1), Random.Range(-1, 1),Random.Range(-1, 1)).normalized;
        return direction * distance;
    }
}
