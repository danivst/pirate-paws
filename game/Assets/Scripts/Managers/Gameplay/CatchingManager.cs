using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CatchingManager : MonoBehaviour
{
    public static CatchingManager main;

    private Health health;

    void Awake()
    {
        main = this;
        health = gameObject.GetComponent<Health>();
    }

    public ICatchable currentTarget = null;

    List<ICatchable> waitingTargets = new List<ICatchable>();


    public void StartCatching(ICatchable catchable)
    {
        if (InventoryManager.main.IsFull || FishingRodsManager.main.CurrentRod == -1)
        {
            return;
        }
        if (currentTarget != null)
        {
            waitingTargets.Add(catchable);
            return;
        }
        currentTarget = catchable;

    }

    public void StopCatching(ICatchable catchable)
    {
        if (currentTarget == catchable)
        {
            currentTarget.ResetTime();
            currentTarget = null;
        }
        int index = waitingTargets.IndexOf(catchable);
        if (index > -1)
        {
            waitingTargets.RemoveAt(index);
        }
    }

    private void Update()
    {
        if (health.IsAlive == false || InventoryManager.main.IsFull == true || MovementManager.main.canMove == false || FishingRodsManager.main.CurrentRod == -1)
        {
            if (currentTarget != null)
            {
                currentTarget.ResetTime();
                currentTarget = null;
            }
            waitingTargets.Clear();
            return;
        }
        if (currentTarget != null)
        {
            currentTarget.TimeLeft -= Time.deltaTime * StatsManager.main.CatchingSpeed * FishingRodsManager.main.currentRodMultiplier;

            if (currentTarget.TimeLeft <= 0)
            {
                currentTarget.Catch();
                currentTarget = null;
                //Sound
                SoundManager.main.CatchSound();
            }
        }

        if (waitingTargets.Count > 0 && currentTarget == null)
        {
            int lastIndex = waitingTargets.Count - 1;

            ICatchable closestTarget = waitingTargets[lastIndex];

            waitingTargets.RemoveAt(lastIndex);

            currentTarget = closestTarget;
        }

    }
}
