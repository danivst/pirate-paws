using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUiItem : MonoBehaviour
{
    public Image Icon;
    public Image RarityBackground;
    public TMP_Text Count;
    public TMP_Text Price;
    public TMP_Text Name;

    public GameObject Bought;

    public Rarity Rarity;
    public int Id;

    public void OnClick()
    {
        ShopManager.main.Buy(transform.GetSiblingIndex());
        //Sound
        SoundManager.main.BuySound();
    }

}
