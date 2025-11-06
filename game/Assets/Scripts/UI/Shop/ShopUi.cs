using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ShopUi : MonoBehaviour
{
    public static ShopUi main;

    public List<GameObject> uiElements; // Items to enable/disable to open/close it

    public GameObject SellTab;
    public GameObject ShopTab;

    public TMP_Text ShopResetTime;

    public Transform shopContent;
    public GameObject shopItem;
    [Header("Effects")]
    public float EffectDuration = 0.05f;

    Dictionary<int, ShopUiItem> items = new Dictionary<int, ShopUiItem>(); // sell thing
    public List<ShopUiItem> shopItems = new List<ShopUiItem>(); // buy

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

    public void Populate()
    {
        foreach (Item item in InventoryManager.main.Items)
        {
            Add(item);
        }
    }
    public void SortItems()
    {
        List<ShopUiItem> ordered = items.Select(item => item.Value).ToList().OrderBy(item => (int)item.Rarity).ToList();
        for (int i = 0; i < ordered.Count; i++)
        {
            ordered[i].gameObject.transform.SetSiblingIndex(i);
        }
    }
    public void Add(Item item)
    {
        ItemInfo info = InventoryManager.main.GetItemInfo(item.ID);
        if (info.itemType != ItemType.Fish)
        {
            return;
        }
        GameObject itemClone = Instantiate(shopItem, shopContent);

        ShopUiItem shopItemUi = itemClone.GetComponent<ShopUiItem>();
        shopItemUi.Name.text = info.Name;
        shopItemUi.Id = info.Id;
        shopItemUi.Count.text = item.amount.ToString() + "x";

        shopItemUi.Rarity = info.Rarity;
        shopItemUi.Icon.sprite = info.Icon;
        shopItemUi.RarityBackground.color = InventoryUi.main.raritiesMaterials.Find(item => item.Rarity == info.Rarity).Material.color;

        items.Add(item.ID, shopItemUi);

        SortItems();
    }
    public void Remove(int id)
    {
        if (items.ContainsKey(id))
        {
            Destroy(items.GetValueOrDefault(id).gameObject);
            items.Remove(id);
            SortItems();
        }
    }
    public void UpdateItem(int id)
    {
        if (items.ContainsKey(id))
        {
            ShopUiItem inventoryItemUi = items.GetValueOrDefault(id);
            Item item = InventoryManager.main.GetItem(id);
            inventoryItemUi.Count.text = item.amount.ToString();
        }
    }
    public void Open()
    {
        UpdateShopItems();

        SetGameplay(false);
        foreach (GameObject obj in uiElements)
        {
            obj.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
            obj.SetActive(true);
            obj.transform.DOScale(new Vector3(1, 1, 1), EffectDuration);
        }
    }
    public void CloseButton()
    {
        SetGameplay(true);
        foreach (GameObject obj in uiElements)
        {
            obj.transform.DOScale(new Vector3(0, 0, 0), EffectDuration).onComplete = () => obj.SetActive(false);
        }
    }

    public void SwitchShop()
    {
        UpdateShopItems();

        SellTab.SetActive(false);
        ShopTab.SetActive(true);
    }
    public void SwitchSell()
    {
        SellTab.SetActive(true);
        ShopTab.SetActive(false);
    }

    // Shop
    public void UpdateTimeLeft()
    {
        long ticksLeft = Utils.TicksTillEndOfDay();
        TimeSpan timeLeft = TimeSpan.FromTicks(ticksLeft);

        ShopResetTime.text = "New Shop In "
            + timeLeft.Hours.ToString("00") + ":"
            + timeLeft.Minutes.ToString("00") + ":"
            + timeLeft.Seconds.ToString("00");
    }

    public void UpdateShopItems()
    {
        for (int i = 0; i < shopItems.Count; i++)
        {
            ShopItem item = ShopManager.main.items[i];
            ShopUiItem itemUi = shopItems[i];

            ItemInfo itemInfo = InventoryManager.main.GetItemInfo(item.Id);


            itemUi.Id = itemInfo.Id;
            itemUi.Name.text = itemInfo.Name;

            itemUi.Count.text = item.Amount + "x";
            itemUi.Price.text = Utils.FormatNumber(item.Price);

            itemUi.Icon.sprite = itemInfo.Icon;
            itemUi.Rarity = itemInfo.Rarity;
            itemUi.RarityBackground.color = InventoryUi.main.raritiesMaterials.Find(item => item.Rarity == itemInfo.Rarity).Material.color;
            
            if (item.IsBought == true)
            {
                itemUi.Bought.SetActive(true);
            }
            else
            {
                itemUi.Bought.SetActive(false);
            }
        }
        
    }
}
