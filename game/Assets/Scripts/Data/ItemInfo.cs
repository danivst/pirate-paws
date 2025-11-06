using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class ItemInfo : IChance
{
    public string Name;
    public int Id;

    public Sprite Icon;
    public Rarity rarity = Rarity.Common;
    public Rarity Rarity { get { return rarity; } set { rarity = value; } }

    public ItemType itemType = ItemType.Fish;

    public float Price = 0;
    public bool canBeSold = true;

    public ItemInfo(string name, int id, Rarity rarity, float price, bool canBeSold)
    {
        Name = name;
        this.Id = id;
        Rarity = rarity;
        Price = price;
        this.canBeSold = canBeSold;
    }
}