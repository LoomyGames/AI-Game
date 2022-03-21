using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Island : MonoBehaviour
{
    private int difficulty = 3;
    private IslandSpawner isp;
    bool islandComplete = false;
    bool spawnedIsland = false;
    float maxPlayerDistance = 1000f;
    float currentDistanceToPlayer = 0f;
    Transform player;
    // Start is called before the first frame update
    void Start()
    {
        SetupIsland();
        isp = GameObject.FindWithTag("Game Director").GetComponent<IslandSpawner>();
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        currentDistanceToPlayer = Vector3.Distance(player.transform.position, gameObject.transform.position);
        if(islandComplete || currentDistanceToPlayer > maxPlayerDistance)
        {
            if (!spawnedIsland)
            {
                spawnedIsland = true;
                isp.GetIslandStatus();
            }
            //Destroy(gameObject);
        }
    }

    public void SetDifficulty(int dif)
    {
        difficulty = dif;
    }

    void SetupIsland()
    {
        if(difficulty == 1)
        {
            Debug.Log("Easy Island Spawned");
        } else if(difficulty == 2)
        {
            Debug.Log("Normal Island Spawned");
        } else
        {
            Debug.Log("Hard Island Spawned");
        }
    }
}
