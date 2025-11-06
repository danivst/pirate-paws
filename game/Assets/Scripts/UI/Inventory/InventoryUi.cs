using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using DG.Tweening;
using UnityEngine.UI;

[Serializable]
public class RarityMaterials
{
    public Rarity Rarity;
    public Material Material;
}
public class InventoryUi : MonoBehaviour
{
    public static InventoryUi main;

    public List<RarityMaterials> raritiesMaterials;

    public List<GameObject> uiElements; // Items to enable/disable to open/close it

    public Transform inventoryContent;
    public GameObject inventoryItem;
    public List<TMP_Text> capacityLabels;
    [Header("Effects")]
    public GameObject ItemPickUpUi;
    public float EffectDuration = 0.05f;

    Dictionary<int, InventoryItemUi> items = new Dictionary<int, InventoryItemUi>();

    private void Awake()
    {
        main = this;
    }
    public void ShowPickUp(int id, int amount)
    {
        GameObject clone = Instantiate(ItemPickUpUi, ItemPickUpUi.transform.parent);

        clone.SetActive(true);

        clone.transform.DOMove(RandomUtils.RandomVector3Distance(50f) + clone.transform.position, 0.2f);
        clone.transform.DOScale(1.25f, 0.2f);

        ItemInfo info = InventoryManager.main.GetItemInfo(id);

        Transform rarity = clone.transform.GetChild(0);
        rarity.GetComponent<Image>().color = raritiesMaterials.Find(item => item.Rarity == info.Rarity).Material.color;
        rarity.GetChild(0).GetComponent<Image>().sprite = info.Icon;
        rarity.GetChild(1).GetComponent<TMP_Text>().text = amount + "x";

        clone.transform.DOMove(new Vector3(-5,700,0), 1).SetDelay(0.1f);
        clone.transform.DOScale(0, 1).SetDelay(0.2f).onComplete = () => Destroy(clone);
        
    }
    public void UpdateLabels()
    {
        foreach (TMP_Text label in capacityLabels)
        {
            label.text = InventoryManager.main.ItemCount + "/" + StatsManager.main.StorageSize;
            if (InventoryManager.main.ItemCount == StatsManager.main.StorageSize)
            {
                label.color = Color.red;
            }
            else if (InventoryManager.main.ItemCount >= StatsManager.main.StorageSize / 2)
            {
                label.color = new Color(255, 138, 0);
            }
            else
            {
                label.color = Color.white;
            }
        }
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
        UpdateLabels();
    }
    public void SortItems()
    {
        List<InventoryItemUi> ordered = items.Select(item => item.Value).ToList().OrderBy(item => (int)item.Rarity).ToList();
        for (int i = 0; i < ordered.Count; i++)
        {
            ordered[i].gameObject.transform.SetSiblingIndex(i);
        }
    }
    public void Add(Item item)
    {
        GameObject itemClone = Instantiate(inventoryItem, inventoryContent);

        ItemInfo info = InventoryManager.main.GetItemInfo(item.ID);

        InventoryItemUi inventoryItemUi = itemClone.GetComponent<InventoryItemUi>();
        inventoryItemUi.Name.text = info.Name;
        inventoryItemUi.Id = info.Id;
        inventoryItemUi.Count.text = item.amount.ToString() + "x";

        inventoryItemUi.Rarity = info.Rarity;
        inventoryItemUi.Icon.sprite = info.Icon;
        inventoryItemUi.RarityBackground.color = raritiesMaterials.Find(item => item.Rarity == info.Rarity).Material.color;

        items.Add(item.ID, inventoryItemUi);

        SortItems();
        UpdateLabels();
    }
    public void Remove(int id)
    {
        
        if (items.ContainsKey(id))
        {
            Destroy(items.GetValueOrDefault(id).gameObject);
            items.Remove(id);
            SortItems();
        }
        UpdateLabels();
    }
    public void UpdateItem(int id)
    {
        UpdateLabels();
        if (items.ContainsKey(id))
        {
            InventoryItemUi inventoryItemUi = items.GetValueOrDefault(id);
            Item item = InventoryManager.main.GetItem(id);
            inventoryItemUi.Count.text = item.amount.ToString();
        }
    }
    public void OpenButton()
    {
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
}
