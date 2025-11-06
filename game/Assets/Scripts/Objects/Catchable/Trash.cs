using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public enum TrashSize
{
    Small = 1, 
    Medium = 2, 
    Large = 3
}
[Serializable]
public class Trash : MonoBehaviour, ICatchable
{
    public TrashSize trashSize;

    public int Amount { get; set; }

    public RegionType RegionType
    { 
        get { return RegionType.TrashRegion; }
    }

    public float TimeLeft { get; set; }

    public float TimeToCatch { get; set; }

    public Drops Drops { get; set; }

    public bool Caught { get; set; }

    public void Awake()
    {
        TimeToCatch = ((int)trashSize * 3 * (Amount + 1)) / 2;
        TimeLeft = TimeToCatch;

        Drops = gameObject.GetComponent<Drops>();
    }

    public void ResetTime()
    {
        TimeLeft = TimeToCatch;
    }

    public void Catch()
    {
        if (Caught == true)
        {
            return;
        }

        Caught = true;
        InventoryManager.main.StoreItem(new Item(InventoryManager.main.GetIdFromName("Trash"), Amount));
        Drops.Drop();
        CatchableVisual fishVisual = gameObject.GetComponent<CatchableVisual>();

        fishVisual.CaughtEffect();
    }
}
