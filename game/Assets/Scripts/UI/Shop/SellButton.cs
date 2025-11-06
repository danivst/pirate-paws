using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellButton : MonoBehaviour
{
    private int Id;
    private void Start()
    {
        Id = gameObject.GetComponent<ShopUiItem>().Id;
    }
    public void OnClick()
    {
        SellUi.main.Open(InventoryManager.main.GetItem(Id));
    }
}
