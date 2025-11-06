using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FishingRodUi : MonoBehaviour
{
    public int RodId;
    public TMP_Text ButtonText;
    public TMP_Text Multiplier;
    public TMP_Text Name;

    public Image Icon;
    public Image Rarity;

    public bool UpdateOnStart = false;
    void Start()
    {
        if (!UpdateOnStart)
        {
            return;
        }
        UpdateInfo();
    }
    public void UpdateInfo()
    {
        ItemInfo info = InventoryManager.main.GetItemInfo(RodId);
        if (UpdateOnStart && FishingRodsManager.main.currentRod == RodId)
        {
            ButtonText.text = "Equipped";
            ButtonText.color = Color.red;
        }
        Multiplier.text = FishingRodsManager.RarityToMultiplier(info.Rarity) + "x";
        Name.text = info.Name;
        Icon.sprite = info.Icon;
        Rarity.color = InventoryUi.main.raritiesMaterials.Find(item => item.Rarity == info.Rarity).Material.color;
    }
    public void Equip()
    {
        FishingRodsManager.main.Equip(RodId);
    }
}
