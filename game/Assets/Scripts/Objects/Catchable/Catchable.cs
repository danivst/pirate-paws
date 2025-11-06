using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICatchable
{
    public int Amount { get; set; }

    public RegionType RegionType { get; }

    public float TimeLeft { get; set; }

    public float TimeToCatch {  get; set; }

    public Drops Drops { get; set; }

    public bool Caught { get; set; }

    public void Awake();

    public void ResetTime();

    public void Catch();
}

