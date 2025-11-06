using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public enum Rarity
{
    // Percent to drop
    Common = 50,
    Uncommon = 25,
    Rare = 15,
    Epic = 5,
    Legendary = 3,
    Mythic = 2
}

public enum ItemType
{
    Trash,
    Fish,
    FishingRod
}

public interface IChance
{
    public Rarity Rarity { get; set; }   
}

[System.Serializable]

public class Item
{
    public int ID;
    public int amount;
    public Item(int id, int amount)
    {
        this.ID = id;
        this.amount = amount;
    }
}

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager main;

    public List<ItemInfo> availableItems;

    public List<Item> Items;

    public int ItemCount { 
        get {
            return Items.Sum(item => item.amount);
        } 
    }
    public int RemainingSpace
    {
        get
        {
            return StatsManager.main.StorageSize - ItemCount;
        }
    }
    public bool IsFull
    {
        get
        {
            return StatsManager.main.StorageSize - ItemCount <= 0;
        }
    }
    public void Awake()
    {
        main = this;
    }

    public string StoreItem(Item item)
    {
 
        if (RemainingSpace < item.amount)
        {
            item.amount = RemainingSpace;
        }

        if (RemainingSpace == 0)
        {
            return "Inventory is full! Please sell or delete some items!";
        }

        Item foundItem = GetItem(item.ID);
        if (foundItem != null)
        {
            foundItem.amount += item.amount;
            InventoryUi.main.UpdateItem(item.ID);
            ShopUi.main.UpdateItem(item.ID);
        }
        else
        {
            Items.Add(item);
            InventoryUi.main.Add(item);
            ShopUi.main.Add(item);
        }
        InventoryUi.main.ShowPickUp(item.ID, item.amount);
        return null;
    }

    public void RemoveItem(Item item)
    {
        Item foundItem = GetItem(item.ID);
        if (foundItem != null)
        {
            
            item.amount = Mathf.Clamp(item.amount, 0, foundItem.amount);

            foundItem.amount -= item.amount;

            if (foundItem.amount <= 0)
            {
                Items.RemoveAt(Items.FindIndex(item1 => item1.ID == item.ID));
                InventoryUi.main.Remove(item.ID);
                ShopUi.main.Remove(item.ID);

                FishingRodsManager.main.CurrentRod = Items.First(item => InventoryManager.main.GetItemInfo(item.ID).itemType == ItemType.FishingRod).ID;
            }
            else
            {
                InventoryUi.main.UpdateItem(item.ID);
                ShopUi.main.UpdateItem(item.ID);
            }
        }
    }

    public Item GetItem(int id)
    {
        return Items.Find(item => item.ID == id);
    }

    public ItemInfo GetItemInfo(int id)
    {
        return availableItems.Find(item => item.Id == id);
    }

    public int GetIdFromName(string name)
    {
        return availableItems.Find(item => item.Name == name).Id;
    }
}
