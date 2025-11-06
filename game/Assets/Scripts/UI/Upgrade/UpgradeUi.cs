using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[Serializable]
public class UpgradeItem
{
    public UpgradeType upgradeType;

    public Image image;
    public Button button;

    public TMP_Text levelLabel;
    public TMP_Text labelMultiplier;
    public TMP_Text costLabel;
}
public class UpgradeUi : MonoBehaviour
{
    public static UpgradeUi main;

    public List<GameObject> uiElements; // Items to enable/disable to open/close it

    public GameObject UpgradesPanel;
    public GameObject RodsPanel;
    [Header("Upgrades")]
    public List<UpgradeItem> imageUpgrades;
    [Header("Fish rods")]
    public GameObject rodItemUi;
    public Transform rodsParent;
    public FishingRodUi currentRod;
    [Header("Effects")]
    public float EffectDuration = 0.05f;
    public float LevelEffectDurration = 1f;
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
    //
    public void UpgradeButton(int upgradeType)
    {
        string res = StatsManager.main.UpgradeStat((UpgradeType)upgradeType);
        UpdateUi();
        if (res != null)
        {
            PopUpManager.main.Display(res);
        }
        else
        {
            //Sound
            SoundManager.main.LevelUpSound();
        }


    }
    public void UpdateUi()
    {
        foreach(UpgradeItem imageUpgrade in imageUpgrades)
        {
            int level = StatsManager.main.GetLevel(imageUpgrade.upgradeType);
            int maxLevel = StatsManager.main.maxLevel;

            if(level == maxLevel)
            {
                imageUpgrade.levelLabel.text = maxLevel + " lvl. (MAX)";
                imageUpgrade.costLabel.text = "NaN";
                imageUpgrade.button.enabled = false;
            }
            else
            {
                imageUpgrade.levelLabel.text = level + " lvl.";
                imageUpgrade.costLabel.text = Utils.FormatNumber(StatsManager.main.GetUpgradeCost(level)).ToString();
                imageUpgrade.button.enabled = true;
            }

            if (imageUpgrade.upgradeType == UpgradeType.StorageSize)
            {
                imageUpgrade.labelMultiplier.text = (level * StatsManager.main.levelChangeStorage + StatsManager.main.startStorageSize).ToString();
            }else if (imageUpgrade.upgradeType == UpgradeType.MoveSpeed)
            {
                imageUpgrade.labelMultiplier.text = (level * StatsManager.main.levelChangeSpeed + 1).ToString("0.##") + " x";
            }
            else
            {
                imageUpgrade.labelMultiplier.text = (level * StatsManager.main.levelChange + 1).ToString("0.##") + " x";
            }

            imageUpgrade.image.DOFillAmount((float)level / (float) maxLevel, LevelEffectDurration);
        }
    }
    //
    List<GameObject> rodsObjs = new List<GameObject>();
    public void UpdateRods()
    {
        // Update Current Rod
        currentRod.RodId = FishingRodsManager.main.CurrentRod;
        currentRod.UpdateInfo();
        //
        foreach(GameObject obj in rodsObjs)
        {
            Destroy(obj);
        }
        foreach (Item item in InventoryManager.main.Items)
        {
            ItemInfo info = InventoryManager.main.GetItemInfo(item.ID);
            if (info.itemType != ItemType.FishingRod)
            {
                continue;
            }
            GameObject clone = Instantiate(rodItemUi, rodsParent);
            FishingRodUi fishingRodUi = clone.GetComponent<FishingRodUi>();
            fishingRodUi.RodId = info.Id;
            rodsObjs.Add(clone);
        }
    }
    //
    public void SwitchRods()
    {
        UpdateRods();
        RodsPanel.SetActive(true);
        UpgradesPanel.SetActive(false);
    }
    public void SwitchUpgrades()
    {
        RodsPanel.SetActive(false);
        UpgradesPanel.SetActive(true);
        UpdateUi();
    }

    public void Open()
    {
        SetGameplay(false);
        foreach (GameObject obj in uiElements)
        {
            obj.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
            obj.SetActive(true);
            obj.transform.DOScale(new Vector3(1, 1, 1), EffectDuration);
        }
        foreach (UpgradeItem imageUpgrade in imageUpgrades)
        {
            imageUpgrade.image.fillAmount = 0;
        }
        UpdateUi();
        currentRod.UpdateInfo();
    }
    public void CloseButton()
    {
        SetGameplay(true);
        foreach (GameObject obj in uiElements)
        {
            obj.transform.DOScale(new Vector3(0, 0, 0), EffectDuration).onComplete = () => obj.SetActive(false);
        }
    }

    
}
