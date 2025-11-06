using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FishingRodMultiplier // in %
{
    Common = 100,
    Uncommon = 110,
    Rare = 120, 
    Epic = 150,
    Legendary = 500,
    Mythic = 1000
}
public class FishingRodsManager : MonoBehaviour
{
    public static FishingRodsManager main;

    public int currentRod = 4;

    public int CurrentRod
    {
        get
        {
            if (InventoryManager.main.GetItem(currentRod) == null)
            {
                currentRod = -1;
            }
            return currentRod;
        }
        set
        {
            currentRod = value;
            if (value == -1)
            {
                currentRodMultiplier = 1f;
                return;
            }
            ItemInfo info = InventoryManager.main.GetItemInfo(value);
            if (info.itemType != ItemType.FishingRod)
            {
                currentRod = -1;
                currentRodMultiplier = 1f;
                return;
            }
            currentRodMultiplier = RarityToMultiplier(info.Rarity);


        }
    }
    public float currentRodMultiplier = 1f;
    // Start is called before the first frame update
    public static float RarityToMultiplier(Rarity rarity)
    {
        switch (rarity)
        {
            case Rarity.Common:
                return (float)FishingRodMultiplier.Common / 100f;
            case Rarity.Uncommon:
                return (float)FishingRodMultiplier.Uncommon / 100f;
            case Rarity.Rare:
                return (float)FishingRodMultiplier.Rare / 100f;
            case Rarity.Epic:
                return (float)FishingRodMultiplier.Epic / 100f;
            case Rarity.Legendary:
                return (float)FishingRodMultiplier.Legendary / 100f;
            case Rarity.Mythic:
                return (float)FishingRodMultiplier.Mythic / 100f;
        }
        return 1;
    }

    void Awake()
    {
        main = this;
    }
    public void Equip(int RodId)
    {
        if (RodId == currentRod)
        {
            PopUpManager.main.Display("Already equipped!");
            return;
        }
        if (InventoryManager.main.GetItem(RodId) != null)
        {
            CurrentRod = RodId;
        }
        else
        {
            PopUpManager.main.Display("You donw have this fishing rod!");
        }
        UpgradeUi.main.UpdateRods();
    }
 
}
