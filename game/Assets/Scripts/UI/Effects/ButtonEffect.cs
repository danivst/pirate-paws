using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
using UnityEngine.XR;

public class ButtonEffect : MonoBehaviour
{
    public UnityEvent OnComplete;

    public bool Enabled = true;
    private float ScaleMin = 0.85f;
    private float MaxRotationChange = 10f;

    public float EffectsLength = 0.35f;
    public void OnClick() 
    {
        SoundManager.main.UiClick();

        if (!Enabled && OnComplete != null){
            OnComplete.Invoke();
            return;
        }
        if (!Enabled){
            return;
        }
        
        //Scale
        transform.DOScale(new Vector3(ScaleMin, ScaleMin, ScaleMin),EffectsLength/2).SetEase(Ease.InOutElastic).onComplete = () => {
            transform.DOScale(new Vector3(1, 1, 1), EffectsLength / 2).SetEase(Ease.InOutElastic);
        };
        //Rotation
        transform.DORotate(new Vector3(0,0, -MaxRotationChange),EffectsLength/3).SetEase(Ease.OutBack).onComplete = () => {
            transform.DORotate(new Vector3(0,0, MaxRotationChange),EffectsLength/3).SetEase(Ease.OutBack).onComplete = () => {
                transform.DORotate(new Vector3(0,0,0), EffectsLength / 3).SetEase(Ease.OutBack);
            };
        };
        if (OnComplete != null){
            OnComplete.Invoke();
        }
        
    }
}
