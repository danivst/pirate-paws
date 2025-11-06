using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesManager : MonoBehaviour
{
    public static UpgradesManager main;

    public float startUpgradeCost = 50;
    public float upgradeCostChange = 100;

    void Awake()
    {
        main = this;
    }

    public int BuyUpgrade(UpgradeType upgradeType)
    {
        float cost = NextUpgradeCost(upgradeType);

        if (CurrencyManager.main.Money - cost < 0)
        {
            return -1;
        }

        int level = 1;

        switch (upgradeType)
        {
            case UpgradeType.MoveSpeed:
                level += StatsManager.main.speedLevel;
                break;

            case UpgradeType.CatchingSpeed:
                level += StatsManager.main.catchingLevel;
                break;

            case UpgradeType.StorageSize:
                level += StatsManager.main.storageLevel;
                break;

            case UpgradeType.Health:
                level += StatsManager.main.healthLevel;
                break;
        }

        CurrencyManager.main.Money -= cost;
        StatsManager.main.SetLevel(upgradeType, level);

        return 0;
    }

    public float NextUpgradeCost(UpgradeType upgradeType)
    {
        int level = 0;

        switch (upgradeType)
        {
            case UpgradeType.MoveSpeed:
                level = StatsManager.main.speedLevel;
                break;

            case UpgradeType.CatchingSpeed:
                level = StatsManager.main.catchingLevel;
                break;

            case UpgradeType.StorageSize:
                level = StatsManager.main.storageLevel;
                break;

            case UpgradeType.Health:
                level = StatsManager.main.healthLevel;
                break;
        }

        return level * upgradeCostChange + startUpgradeCost;
    }
}
