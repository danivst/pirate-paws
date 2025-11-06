using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public enum RegionType
{
    ShallowWaters = 1,
    DeepOcean = 2,
    DangerousWaters = 3,
    TrashRegion = 4
}

public class CatchableManager : MonoBehaviour
{
    public static CatchableManager main;

    public List<CatchableSpawn> spawners;
    public List<GameObject> catchables;

    private List<RegionType> regions = new List<RegionType>();

    private Dictionary<int,RegionType> catchableRegions = new Dictionary<int, RegionType>();

    [Header("Settings")]

    float lastSpawnedTime = 0.0f;

    float spawnInterval = 2f;


    private void Awake()
    {
        main = this;

        // Index to region association
        for (int i = 0; i < catchables.Count; i++ )
        {
            GameObject obj = catchables[i];
            RegionType regionType = obj.GetComponent<ICatchable>().RegionType;
            if (!regions.Exists(item => item == regionType))
            {
                regions.Add(regionType);
            }
            catchableRegions.Add(i, regionType);
        }
    }
    
    private void Update()
    {
        lastSpawnedTime += Time.deltaTime;
        if (lastSpawnedTime >= spawnInterval)
        {
            lastSpawnedTime = 0;
            foreach (CatchableSpawn spawn in spawners)
            {
                spawn.Spawn(GetRandomCatchable(spawn.regionType));
            }
        }
    }

    private GameObject GetRandomCatchable(RegionType regionType)
    {
        int[] indexes = catchableRegions.Where(item => item.Value == regionType).Select(item => item.Key).ToArray();
       
        if (indexes.Length == 0) 
        {
            return null;
        }

        return catchables[indexes[Random.Range(0, indexes.Length)]];
    }
   
}
