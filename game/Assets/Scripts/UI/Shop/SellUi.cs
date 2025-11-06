using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SellUi : MonoBehaviour
{
    public static SellUi main;

    public List<GameObject> uiElements; // Items to enable/disable to open/close it
    public TMP_InputField inputField;
    public TMP_Text placeholder;
    public TMP_Text rewardAmount;
    public float EffectDuration = 0.05f;

    ItemInfo itemInfo;

    Item current;
    Item Current
    {
        set
        {
            current = value;
            if (value != null)
            {
                itemInfo = InventoryManager.main.GetItemInfo(value.ID);
            }
            else
            {
                itemInfo = null;
            }
        }
        get { return current; }
    }
    public int count = 1;
    public int Count
    {
        get { return count; }
        set
        {
            count = value;
            inputField.text = value.ToString();
           
            rewardAmount.text = value + " X " + Utils.FormatNumber(itemInfo.Price * StatsManager.main.FishValue) + " = " + Utils.FormatNumber((float)value * itemInfo.Price * StatsManager.main.FishValue);
        }
    }
    private void Awake()
    {
        main = this;
    }

    public void Open(Item item)
    {
        if (Current != null)
        {
            return;
        }
        Current = item;
        Count = 1;
        placeholder.text = "1 - " + Current.amount;

        foreach (GameObject obj in uiElements)
        {
            obj.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
            obj.SetActive(true);
            obj.transform.DOScale(new Vector3(1, 1, 1), EffectDuration);
        }
    }
    public void Close()
    {
        Current = null;

        foreach (GameObject obj in uiElements)
        {
            obj.transform.DOScale(new Vector3(0, 0, 0), EffectDuration).onComplete = () => obj.SetActive(false);
        }
    }
    public void Plus()
    {
        Count = Mathf.Clamp(Count + 1, 1, Current.amount);
    }
    public void Minus()
    {
        Count = Mathf.Clamp(Count - 1, 1, Current.amount);
    }
    public void Sell()
    {
        CurrencyManager.main.SellItem(Current.ID, Count);
        Count = 1;
        Close();
      
        inputField.text = "1";
        //Sound
        SoundManager.main.SellSound();
    }
    public void Cancel()
    {
        Close();
        Count = 1;
        Current = null;
        inputField.text = "1";
    }
    public void ValidateInput(string input)
    {
        input = inputField.text;
        if (input == null)
        {
            inputField.text = "";
            return;
        }
        try
        {
            int count = int.Parse(inputField.text);
            if (count > Current.amount)
            {
                throw new Exception("Larger Value!");
            }
            Count = count;
        }
        catch (Exception)
        {
            inputField.text = Current.amount.ToString();
        }
    }
}
