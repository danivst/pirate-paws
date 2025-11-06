using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public enum UpgradeType
{
    MoveSpeed = 1,
    CatchingSpeed = 2,
    StorageSize = 3,
    Health = 4,
    FishValue = 5
}

public class StatsManager : MonoBehaviour
{
    public static StatsManager main;

    [Header("Stats")]
    public float MoveSpeed = 1f;
    public float CatchingSpeed = 1f;
    public int StorageSize = 30;
    public float Health = 1f;
    public float FishValue = 1f;

    [Header("Levels")]
    public int speedLevel = 0;
    public int catchingLevel = 0;
    public int storageLevel = 0;
    public int healthLevel = 0;
    public int fishValueLevel = 0;

    [Header("Settings")]
    public int maxLevel = 100;

    public float startMoveSpeed = 1f;
    public float startCatchingSpeed = 1f;
    public int startStorageSize = 30;
    public float startHealth = 1f;
    public float startFishValue = 1f;

    public float levelChange = 0.05f; // for catching speed, health and fish value
    public float levelChangeSpeed = 0.025f; // for speed
    public int levelChangeStorage = 5; // for speed, catching speed and health

    public float upgradeCostPerLevel = 50f;

    void Awake()
    {
        main = this;
    }
    //Stats Stuff
    private void UpdateStats()
    {
        MoveSpeed = startMoveSpeed + speedLevel * levelChangeSpeed;

        CatchingSpeed = startCatchingSpeed + catchingLevel * levelChange;

        StorageSize = startStorageSize + storageLevel * levelChangeStorage;

        Health = startHealth + healthLevel * levelChange;

        FishValue = startFishValue + fishValueLevel * levelChange;
    }
    public int GetLevel(UpgradeType upgradeType)
    {
        switch (upgradeType)
        {
            case UpgradeType.MoveSpeed:
                return speedLevel;

            case UpgradeType.CatchingSpeed:
                return catchingLevel;
            case UpgradeType.StorageSize:
                return storageLevel;

            case UpgradeType.Health:
                return healthLevel;
            case UpgradeType.FishValue:
                return fishValueLevel;
        }
        return -1;
    }
    public void SetLevel(UpgradeType upgradeType, int level)
    {
        switch (upgradeType)
        {
            case UpgradeType.MoveSpeed:
                speedLevel = level; 
                break;

            case UpgradeType.CatchingSpeed:
                catchingLevel = level;
                break;

            case UpgradeType.StorageSize:
                InventoryUi.main.UpdateLabels();
                storageLevel = level;
                break;

            case UpgradeType.Health:
                healthLevel = level;
                break;
            case UpgradeType.FishValue:
                fishValueLevel = level;
                break;
        }

        UpdateStats();
    }

    //Upgrade Stats
    public float GetUpgradeCost(int currentLevel)
    {
        return upgradeCostPerLevel * (currentLevel + 1);
    }
    public string UpgradeStat(UpgradeType type)
    {
        int currentLevel = GetLevel(type);
        if (currentLevel >= maxLevel)
        {
            return "Already maxed out!";
        }
        float cost = GetUpgradeCost(currentLevel);
     
        if (CurrencyManager.main.Money >= cost)
        {
            CurrencyManager.main.Money -= cost;
            SetLevel(type, currentLevel + 1);
            return null;
        }
        return "Not enough coins!";
    }
}
