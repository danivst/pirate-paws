using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class DropsManager<T> where T: IChance
{
    public static T Drop(List<T> drops)
    {
        drops.OrderBy(i => i.Rarity);

        int random = Random.Range(1, (int)drops.Last().Rarity);

        Rarity rarity = Rarity.Common;

        foreach (T item in drops)
        {
            if ((int)item.Rarity >= random)
            {
                rarity = item.Rarity;
                break;
            }
        }

        List<T> items = drops.Where(item => item.Rarity == rarity).ToList(); 

        int randomItem = Random.Range(0, items.Count);
        
        return items[randomItem];
    }
}
public class DropsManager
{
    public static bool Dropped(int chance)
    {
        int random = Random.Range(1, 100);

        if (chance >= random)
        {
            return true;
        }

        return false;
    }
}