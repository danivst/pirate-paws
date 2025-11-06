using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using Random = UnityEngine.Random;
using Unity.VisualScripting;

public class CatchableVisual : MonoBehaviour
{
    public GameObject CatchableModel;
    public Transform modelsParent;

    private ICatchable catchable;

    public GameObject billboard;
    public Transform billboardFill;

    public GameObject splashEffect;

    Vector3 maxFillSize = new Vector3(5,5,5);
    float maxTime = 0;

    public float maxRange = 1f;
    public float billboardRange = 20f;
    public float billboardMaxScale = 0.45f;

    void Awake()
    {
        catchable = gameObject.GetComponent<ICatchable>();
  
        if (catchable != null)
        {
            maxTime = catchable.TimeLeft;
        }
        

        billboard.transform.localScale = Vector3.zero;
    }

    private void Start()
    {
        if (billboardFill != null){
            billboardFill.localScale = new Vector3(0, 0, 0);
        }
    }
    // Visuals
    public void SpawnModels()
    {
        if (CatchableModel == null)
        {
            return;
        }

        if (modelsParent == null)
        {
            modelsParent = transform;
        }

        for (int i = 0; i < catchable.Amount; i++)
        {
            GameObject clone = Instantiate(CatchableModel, modelsParent);
            clone.SetActive(true);
            clone.transform.position += RandomUtils.RandomVector3Distance(maxRange);
        }
    }
    //
    float cooldown = 0;

   // Billboard stuff
    Tween currentTween = null;
    float tweenTime = 0.25f;
    public void CloseBillboard(Action func = null)
    {
        if (billboard.activeSelf == false){
            return;
        }
        if (currentTween != null)
        {
            currentTween.Kill();
        }
        currentTween = billboard.transform.DOScale(0, tweenTime).SetEase(Ease.OutCubic);
        currentTween.onComplete = () => {
            billboard.SetActive(false);
            if (func != null)
            {
                func.Invoke();
            }
        };
    }

    public void OpenBillboard()
    {
        if (billboard.activeSelf == true)
        {
            return;
        }
        if (currentTween != null)
        {
            currentTween.Kill();
        }
        billboard.SetActive(true);
        currentTween = billboard.transform.DOScale(billboardMaxScale, tweenTime).SetEase(Ease.InCubic);
    }
    //
    private void Update()
    {
        cooldown += Time.deltaTime;
        if (cooldown >= Time.deltaTime * 2)
        {
            cooldown = 0;
            float distance = (transform.position - MovementManager.main.transform.position).magnitude;
            if (billboard.activeSelf == true && distance > billboardRange)
            {
                CloseBillboard();
            }
            else if (billboard.activeSelf == false && distance <= billboardRange)
            {
                OpenBillboard();
            }
        }

        // Update ui
        if (billboard.activeSelf == true && catchable != null)
        {
            billboardFill.localScale = maxFillSize * ((maxTime - MathF.Abs(catchable.TimeLeft)) / maxTime);
        }
    }

    public void CaughtEffect()
    {
        currentTween.Kill();

        modelsParent.gameObject.SetActive(false); // Make models not visible

        GameObject splash = Instantiate(splashEffect);
        splash.transform.position = transform.position;
        splash.SetActive(true);
        Destroy(gameObject);
    }
    private void OnDestroy()
    {
        currentTween?.Kill();
    }
}
