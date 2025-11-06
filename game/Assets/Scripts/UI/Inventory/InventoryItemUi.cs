using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryItemUi : MonoBehaviour
{
    public Image Icon;
    public Image RarityBackground;
    public TMP_Text Count;
    public TMP_Text Name;

    public Rarity Rarity;
    public int Id;
}
