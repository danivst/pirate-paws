using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class SaveData
{
    public float Money;

    //Levels
    public int SpeedLevel;
    public int CatchingLevel;
    public int StorageLevel;
    public int HealthLevel;
    public int FishValueLevel;
    //

    //Inventory
    public Item[] Items = new Item[] { new Item(4,1)};
    //Shop
    public long UpdateTime;
    public ShopItem[] ShopItems;
    //Fishing rod
    public int FishingRod;
    //Settings
    public int QualityLevel = 2;
    public int MasterVolume = 100;
    public int MusicVolume = 100;

    public SaveData(float Money, int SpeedLevel, int CatchingLevel, int StorageLevel, int HealthLevel, int FishValueLevel, Item[] Items,ShopItem[] ShopItems,long UpdateTime, int FishingRod,int QualityLevel, int MasterVolume, int MusicVolume)
    {
        this.Money = Money;

        this.SpeedLevel = SpeedLevel;
        this.CatchingLevel = CatchingLevel;
        this.StorageLevel = StorageLevel;
        this.HealthLevel = HealthLevel;
        this.FishValueLevel = FishValueLevel;

        this.Items = Items;
        this.ShopItems = ShopItems;
        this.UpdateTime = UpdateTime;
        this.FishingRod = FishingRod;

        this.QualityLevel = QualityLevel;
        this.MasterVolume = MasterVolume;
        this.MusicVolume = MusicVolume;
    }

    public static SaveData GenerateSaveData()
    {
        return new SaveData(
            CurrencyManager.main.Money,
            StatsManager.main.speedLevel,
            StatsManager.main.catchingLevel,
            StatsManager.main.storageLevel,
            StatsManager.main.healthLevel,
            StatsManager.main.fishValueLevel,

            InventoryManager.main.Items.ToArray(),

            ShopManager.main.items.ToArray(),
            ShopManager.updateTime,
            FishingRodsManager.main.CurrentRod,

            SettingsUi.main.qualityDropdown.value,
            (int)SettingsUi.main.masterVolume.value,
            (int)SettingsUi.main.musicVolume.value
        );
    }

    public void LoadData(){
        CurrencyManager.main.Money = Money;

        StatsManager.main.SetLevel(UpgradeType.MoveSpeed,SpeedLevel);
        StatsManager.main.SetLevel(UpgradeType.CatchingSpeed,CatchingLevel);
        StatsManager.main.SetLevel(UpgradeType.StorageSize,StorageLevel);
        StatsManager.main.SetLevel(UpgradeType.Health,HealthLevel);
        StatsManager.main.SetLevel(UpgradeType.FishValue, FishValueLevel);

        InventoryManager.main.Items = this.Items.ToList();

        ShopManager.main.items = this.ShopItems.ToList();
        ShopManager.updateTime = this.UpdateTime;

        FishingRodsManager.main.CurrentRod = this.FishingRod;

        SettingsUi.main.qualityDropdown.value = this.QualityLevel;
        SettingsUi.main.UpdateQuality(this.QualityLevel);

        SettingsUi.main.musicVolume.value = this.MusicVolume;
        SettingsUi.main.masterVolume.value = this.MasterVolume;
    }
}

public class PlayerPrefsManager : MonoBehaviour
{
    public float AutoSaveInterval = 30f;
    SaveData data;

    public bool HomeScreen = false;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("SavedData"))
        {
            string savedInfo = PlayerPrefs.GetString("SavedData");
            data = JsonUtility.FromJson<SaveData>(savedInfo);
        }
    }
    void Start()
    {
        if (HomeScreen)
        {
            if (data != null)
            {
                SoundManager.main.SetVolume(data.MasterVolume, data.MusicVolume);

                SettingsUi.main.qualityDropdown.value = data.QualityLevel;
                SettingsUi.main.UpdateQuality(data.QualityLevel);

                SettingsUi.main.musicVolume.value = data.MusicVolume;
                SettingsUi.main.masterVolume.value = data.MasterVolume;
            }
            return;
        }
        if (data != null)
        {
            data.LoadData();
            ShopUi.main.Populate();
            UpgradeUi.main.UpdateUi();
        }
        else
        {
            SaveGameData();
            InventoryUi.main.UpdateLabels();
        }
        InventoryUi.main.Populate();
    }

    void SaveGameData()
    {
        if (HomeScreen)
        {
            if (data == null)
            {
                return;
            }
            data.QualityLevel = SettingsUi.main.qualityDropdown.value;
            data.MusicVolume = (int)SettingsUi.main.musicVolume.value;
            data.MasterVolume = (int)SettingsUi.main.masterVolume.value;


            string saveInfo = JsonUtility.ToJson(data);
            PlayerPrefs.SetString("SavedData", saveInfo);
        }
        else
        {
            string saveInfo = JsonUtility.ToJson(SaveData.GenerateSaveData());
            PlayerPrefs.SetString("SavedData", saveInfo);
        }
    }

    float time = 0;

    void Update()
    {
        time += Time.deltaTime;
        if (time >= AutoSaveInterval)
        {
            SaveGameData();
            time = 0;
        }
    }

    private void OnApplicationQuit()
    {
        SaveGameData();
    }
    private void OnLevelWasLoaded(int level)
    {
        SaveGameData();
    }
  
    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            SaveGameData();
        }
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        {
            SaveGameData();
        }
    }
}
