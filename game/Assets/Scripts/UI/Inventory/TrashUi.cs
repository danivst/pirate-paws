using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using System;
using System.Linq;

public class TrashUi : MonoBehaviour
{
    public static TrashUi main;

    public List<GameObject> uiElements; // Items to enable/disable to open/close it
    public TMP_InputField inputField;
    public TMP_Text placeholder;
    public float EffectDuration = 0.05f;

    Item current;
    public int count = 1;
    public int Count
    {
        get { return count; }
        set
        {
            count = value;
            inputField.text = value.ToString();
        }
    }
    private void Awake()
    {
        main = this;
    }

    public void Open(Item item)
    {
        if (current != null)
        {
            return;
        }
        current = item;
        placeholder.text = "1 - " + current.amount;

        foreach (GameObject obj in uiElements)
        {
            obj.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
            obj.SetActive(true);
            obj.transform.DOScale(new Vector3(1, 1, 1), EffectDuration);
        }
    }
    public void Close()
    {
        current = null;

        foreach (GameObject obj in uiElements)
        {
            obj.transform.DOScale(new Vector3(0, 0, 0), EffectDuration).onComplete = () => obj.SetActive(false);
        }
    }
    public void Plus()
    {
        Count = Mathf.Clamp(Count + 1, 1, current.amount);
    }
    public void Minus()
    {
        Count = Mathf.Clamp(Count - 1, 1, current.amount);
    }
    public void Delete()
    {
        ItemInfo info = InventoryManager.main.GetItemInfo(current.ID);
        Item item = InventoryManager.main.GetItem(current.ID);

        if (item.amount == 1 && info.itemType == ItemType.FishingRod && InventoryManager.main.Items.Count(item => InventoryManager.main.GetItemInfo(item.ID).itemType == ItemType.FishingRod) == 1)
        {
            PopUpManager.main.Display("You can't delete your only fishing rod!");
            return;
        }

        InventoryManager.main.RemoveItem(new Item(current.ID, Count));
        Close();
        Count = 1;
        inputField.text = "1";
        // Sound
        SoundManager.main.TrashSound();
    }
    public void Cancel()
    {
        Close();
        Count = 1;
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
            if (count > current.amount)
            {
                throw new Exception("Larger Value!");
            }
            Count = count;
        }
        catch (Exception)
        {
            inputField.text = current.amount.ToString();
        }
    }
}
