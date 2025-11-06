using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class ShopItem
{
    public int Id;
    public int Amount;
    public float Price;
    public bool IsBought;


    public ShopItem(int Id, int Amount, float Price, bool IsBought)
    {
        this.Id = Id;
        this.Amount = Amount;
        this.Price = Price;
        this.IsBought = IsBought;
    }
}
[Serializable]
public class ShopItemRarity
{
    public int Id;
    public int appearChance;
    public int maxAmount = 1;
    public int discountChance;
    public int maxDiscount;
}
public class ShopManager : MonoBehaviour
{
    public static ShopManager main;

    public List<ShopItemRarity> availableItems;
    [Header("Data")]
    public List<ShopItem> items;

    public static long updateTime;

    
    void Awake()
    {

        main = this;
    }
    private void Start()
    {
   
        availableItems = availableItems.OrderBy(item => item.appearChance).ToList();

        UpdateShop();
    }
    float count = 0;
    private void Update()
    {
        count += Time.deltaTime;
        if (count > 1)
        {
            UpdateShop();
            count = 0;
        }
    }
    public void UpdateShop()
    {
        ShopUi.main.UpdateTimeLeft();
        bool isDone = Utils.IsPreviousDay(updateTime);
        if (!isDone)
        {
            return;
        }
        updateTime = Utils.GetTimestamp();
        //Clear objs
        items.Clear();
        //
        for (int itemIndex = 0; itemIndex < 4; itemIndex++)
        {
            for (int i = 0; i < availableItems.Count; i++)
            {
                ShopItemRarity info = availableItems[i];
                if (DropsManager.Dropped((int)info.appearChance))
                {
                    items.Add(PrepareItem(info));
                    break;
                }else if (i == availableItems.Count - 1)
                {
                    items.Add(PrepareItem(info));
                }
            }
        }
        //Visuals
        ShopUi.main.UpdateShopItems();
        
    }
    public ShopItem PrepareItem(ShopItemRarity info)
    {
        int amount = UnityEngine.Random.Range(1, info.maxAmount);
        float price = InventoryManager.main.GetItemInfo(info.Id).Price * amount;
        
        if (DropsManager.Dropped(info.discountChance))
        {
            price *= (100 - (float)UnityEngine.Random.Range(1, info.maxDiscount)) / 100;
        }
      
        return new ShopItem(info.Id, amount, price, false);
    }
    public void Buy(int index)
    {
        ShopItem shopItem = items[index];
        if (shopItem.IsBought == true)
        {
            return;
        }
        string res =  CurrencyManager.main.BuyItem(shopItem.Id, shopItem.Amount, shopItem.Price);
        if (res == null)
        {
            shopItem.IsBought = true;
        }
        else
        {
            PopUpManager.main.Display(res);
        }
        ShopUi.main.UpdateShopItems();
    }
    
}
