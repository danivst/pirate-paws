using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class RecycleUi : MonoBehaviour
{
    public static RecycleUi main;

    public TMP_Text countLabel;
    public List<GameObject> uiElements;

    [Header("Effects")]
    public float EffectDuration = 0.05f;
    private void Awake()
    {
        main = this;
    }
    void SetGameplay(bool active)
    {
        MovementManager.main.canMove = active;
        CameraManager.main.Enabled = active;
        JoyStick.main.Active = active;
    }
    public void UpdateLabel()
    {
        Item item = InventoryManager.main.GetItem(0);
        if (item != null)
        {
            countLabel.text = item.amount.ToString();
        }
        else
        {
            countLabel.text = "0";
        }
    }
    public void Recycle()
    {
        Item item = InventoryManager.main.GetItem(0);
        if (item != null)
        {
            CurrencyManager.main.SellItem(item.ID, item.amount, true);
        }
        UpdateLabel();
        //Sound
        SoundManager.main.RecycleSound();
    }
    public void Open()
    {
        foreach (GameObject obj in uiElements)
        {
            obj.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
            obj.SetActive(true);
            obj.transform.DOScale(new Vector3(1, 1, 1), EffectDuration);
        }
        SetGameplay(false);
        UpdateLabel();
    }
    public void Close()
    {
        SetGameplay(true);
        foreach (GameObject obj in uiElements)
        {
            obj.transform.DOScale(new Vector3(0, 0, 0), EffectDuration).onComplete = () => obj.SetActive(false);
        }
    }
}
