using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FishType
{
    TropicalFish = 1,
    Herring = 2,
    Sardine = 3,
    Perch = 9,
    Salmon = 10,
    Shark = 11,
}

[Serializable]
public class Fish : MonoBehaviour, ICatchable
{
    public int Amount { get; set; }
    [Header("Fish Info")]
    public RegionType regionType;
    public float timeToCatchOne;
    public FishType fishType;

    public RegionType RegionType
    { 
        get { return regionType; } 
    }

    public float TimeLeft { get; set; }

    public float TimeToCatch { get; set; }

    public Drops Drops { get; set; }

    
    public bool Caught { get; set; }


    public void Awake()
    {
        TimeToCatch = (timeToCatchOne * (Amount + 1)) / 2;
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

        string res = InventoryManager.main.StoreItem(new Item((int)fishType, Amount));
        if (res!= null)
        {
            Caught = false;
            ResetTime();
            return;
        }
        Drops.Drop();
        CatchableVisual fishVisual = gameObject.GetComponent<CatchableVisual>();

        fishVisual.CaughtEffect();
    }
}
