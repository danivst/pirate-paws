using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager main;

    public List<TMP_Text> labels;

    private float money = 0f;

    public float Money { 
        get
        { 
            return money;
        } 
        set 
        {
            money = value;
            foreach(TMP_Text label in labels)
            {
                label.text = Utils.FormatNumber(money);
            }
        } 
    }


    void Awake()
    {
        main = this;
    }
    private void Start()
    {
        Money = money;
    }
    public void SellItem(int id, int amount, bool bypassCanBeSold = false)
    {
        ItemInfo itemInfo = InventoryManager.main.GetItemInfo(id);
        if (itemInfo != null && (itemInfo.canBeSold == true || bypassCanBeSold == true))
        {
            Item item = InventoryManager.main.GetItem(id);
            if (item.amount < amount)
            {
                return;
            }
            float multy = 1f;
            if (itemInfo.itemType == ItemType.Fish)
            {
                multy = StatsManager.main.FishValue;
            }
            Money += Mathf.Clamp(amount, 0, item.amount) * itemInfo.Price * multy;

            InventoryManager.main.RemoveItem(new Item(id, amount));
        }
    }

    public string BuyItem(int id, int amount)
    {
        ItemInfo itemInfo = InventoryManager.main.GetItemInfo(id);
        if (InventoryManager.main.RemainingSpace < amount)
        {
            return "Inventory is full!";
        }
        if (Money >= itemInfo.Price * amount)
        {
            Money -= itemInfo.Price * amount;
            Item item = new Item(id, amount);
            string res = InventoryManager.main.StoreItem(item);
            return res;
        }
        else
        {
            return "Insufficient funds!";
        }
    }
    public string BuyItem(int id, int amount,float price)
    {
        ItemInfo itemInfo = InventoryManager.main.GetItemInfo(id);
        if (InventoryManager.main.RemainingSpace < amount)
        {
            return "Inventory is full!";
        }
        if (Money >= price)
        {
            Money -= price;
            Item item = new Item(id, amount);
            string res = InventoryManager.main.StoreItem(item);
            return res;
        }
        else
        {
            return "Insufficient funds!";
        }
    }
}
