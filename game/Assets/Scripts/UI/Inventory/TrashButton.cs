using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashButton : MonoBehaviour
{
    private int Id;
    private void Start()
    {
        Id = gameObject.GetComponent<InventoryItemUi>().Id;
    }
    public void OnClick()
    {
        TrashUi.main.Open(InventoryManager.main.GetItem(Id));
    }
}
