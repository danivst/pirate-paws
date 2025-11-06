using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Drop: IChance
{
    public int ID;

    [SerializeField]
    private Rarity rarity = Rarity.Common; 

    public Rarity Rarity
    {
        get { return rarity; }
        set { rarity = value; }
    }
}

public class Drops : MonoBehaviour
{
    public List<Drop> dropItems;

    public int ChanceToDropExtra = 15;

    public void Drop()
    {
        if (DropsManager.Dropped(ChanceToDropExtra))
        {
            Drop droppedItem = DropsManager<Drop>.Drop(dropItems);

            InventoryManager.main.StoreItem(new Item(droppedItem.ID, 1));
        }
    }
}
