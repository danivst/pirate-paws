using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractionType
{
    Shop, Fish, Trash, Upgrade, Recycle
}

public class InteractionManager : MonoBehaviour
{
    public static InteractionManager main;

    private Health health;

    private void Awake()
    {
        main = this;
        health = gameObject.GetComponent<Health>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (health.IsAlive == false)
        {
            return;
        }

        Interactable interactable = other.gameObject.GetComponent<Interactable>();
        if (interactable != null)
        {
            switch (interactable.type)
            {
                case InteractionType.Fish:
                    other.GetComponent<CatchableVisual>().OpenBillboard();
                    CatchingManager.main.StartCatching(other.gameObject.GetComponent<Fish>());
                    break;
                case InteractionType.Trash: 
                    other.GetComponent<CatchableVisual>().OpenBillboard();
                    CatchingManager.main.StartCatching(other.gameObject.GetComponent<Trash>());
                    break;
                case InteractionType.Shop:
                    ShopUi.main.Open();
                    break;
                case InteractionType.Upgrade:
                    UpgradeUi.main.Open();
                    break;
                case InteractionType.Recycle:
                    RecycleUi.main.Open();
                    break;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Interactable interactable = other.gameObject.GetComponent<Interactable>();
        if (interactable != null)
        {
            switch (interactable.type)
            {
                case InteractionType.Fish:
                    other.GetComponent<CatchableVisual>().CloseBillboard();
                    CatchingManager.main.StopCatching(other.gameObject.GetComponent<Fish>());
                    break;
                case InteractionType.Trash:
                    other.GetComponent<CatchableVisual>().CloseBillboard();
                    CatchingManager.main.StopCatching(other.gameObject.GetComponent<Trash>());
                    break;
            }
        }
    }
}
