using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{

    public Health health;
    public RectTransform healthParent;
    public Image healthInside;
    public TMP_Text healthText;

    float timer = 0f;
    float heartBeat = 0f;

    void Update()
    {
        heartBeat = Mathf.Lerp(80, 180, 1 - (health.health / health.maxHealth)); 
        timer += Time.deltaTime;
        float time = 60 / heartBeat;
        if (heartBeat > 0 && timer > time)
        {
            timer = 0;
            healthParent.DOScale(0.85f,time / 3).onComplete = () => healthParent.DOScale(1,time / 3);

            healthInside.fillAmount = health.health / health.maxHealth;
            healthText.text = (int)((health.health / health.maxHealth) * 100) + "%";
        }
    }
}
