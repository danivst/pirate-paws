using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchableSpawn : MonoBehaviour
{
    public float radius;
    public RegionType regionType;
    public Vector3 position;

    public float interval = 1f;
    public int maxFishes = 5;
    public int spawnRadius = 20;
    public float despawnRadius = 35;

    [Header("Fish Settings")]
    public float spawnHeight = -0.61f;
    public int maxAmount = 4;

    public List<GameObject> catchables = new List<GameObject>();

  
    private void Start()
    {
        position = transform.position;
    }

    public void Spawn(GameObject catchable)
    {
        CleanCaughtCatchables();

        if (catchables.Count < maxFishes && (MovementManager.main.transform.position - position).magnitude < despawnRadius)
        {
            GameObject cloned = Instantiate(catchable);
            cloned.SetActive(false);
            Vector2 randomPos = RandomUtils.RandomVector2(spawnRadius);
          
            Func<float, float, Vector3> getPos = (float x, float y) => new Vector3(x, spawnHeight, y) + position;

            Vector3 pos = getPos(randomPos.x, randomPos.y);

            int attempt = 0;
            while ((pos - MovementManager.main.transform.position).magnitude >= despawnRadius)
            {
                attempt++;

                randomPos = RandomUtils.RandomVector2(spawnRadius);
                pos = getPos(randomPos.x, randomPos.y);

                if (attempt > 50)
                {
                    break;
                }
            }
            cloned.transform.position = pos;

           
            Fish fishData = cloned.GetComponent<Fish>();
            if (fishData == null)
            {
                cloned.GetComponent<Trash>().trashSize = (TrashSize)UnityEngine.Random.Range(1, 3);
                cloned.GetComponent<Trash>().Amount = UnityEngine.Random.Range(1, maxAmount);
            }
            else
            {
                fishData.Amount = UnityEngine.Random.Range(1, maxAmount);
            }
            


            cloned.SetActive(true);

            // Visual stuff
            cloned.GetComponent<CatchableVisual>().SpawnModels();

            catchables.Add(cloned);
        }
    }

    public void CleanCaughtCatchables()
    {
        for (int i = catchables.Count - 1; i >= 0; i--)
        {
            if (catchables[i] == null)
            {
                catchables.RemoveAt(i); 
            }
        }
    }

    public void RemoveDistantCatchables()
    {
        for (int i = catchables.Count - 1; i >= 0; i--)
        {
            if (catchables[i] == null)
            {
                catchables.RemoveAt(i);
                continue;
            }
            if ((catchables[i].transform.position - MovementManager.main.transform.position).magnitude >= despawnRadius)
            {
                Destroy(catchables[i]);
                catchables.RemoveAt(i);
            }
        }
    }

  

    float cooldown = 0;
   
    private void Update()
    {
        if (cooldown >= interval)
        {
            cooldown = 0;
        }
        else
        {
            cooldown += Time.deltaTime;
            return;
        }
        RemoveDistantCatchables();
    }
}
