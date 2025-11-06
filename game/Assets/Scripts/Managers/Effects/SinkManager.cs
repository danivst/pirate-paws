using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Assets.Scripts;

public class SinkManager : MonoBehaviour
{
    public static SinkManager main;

    public WaterFloat floater;

    public bool IsPlayer = false;
    public float SinkTime = 3;

    public GameObject[] DisableOnSink;

    void Awake()
    {
        WaterFloat floater = gameObject.GetComponent<WaterFloat>();

        if (this.floater == null && floater != null)
        {
            this.floater = floater;
        }

        if (!IsPlayer)
        {
            return;
        }

        main = this;
    }
 
    public void RespawnEffect()
    {
        foreach (GameObject obj in DisableOnSink)
        {
            obj.SetActive(true);
        }
        transform.localPosition = Vector3.zero;
        floater.enabled = true;
    }

    public IEnumerator SinkEffect()
    {
        foreach (GameObject obj in DisableOnSink)
        {
            obj.SetActive(false);
        }
        floater.enabled = false;
        yield return transform.DOLocalMoveY(-10, SinkTime).WaitForCompletion();
    }
}
