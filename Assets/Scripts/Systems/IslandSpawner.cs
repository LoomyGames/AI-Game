using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandSpawner : MonoBehaviour
{

    public int difficulty = 3;
    public float islandDistance = 1500f;
    private Player player;
    private int playerHealth = 0;
    private Object[] easyIslands;
    private Object[] normalIslands;
    private Object[] hardIslands;

    float timer = 5f;
    float currentTime = 0f;

    GameObject currentIsland;
    GameDirector gameDirector;

    bool spawnedIsland = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        playerHealth = player.GetHealth();
        easyIslands = Resources.LoadAll("Prefabs/Easy Islands", typeof(GameObject));
        normalIslands = Resources.LoadAll("Prefabs/Normal Islands", typeof(GameObject));
        hardIslands = Resources.LoadAll("Prefabs/Hard Islands", typeof(GameObject));
        gameDirector = GameObject.FindWithTag("Game Director").GetComponent<GameDirector>();
    }

    // Update is called once per frame
    void Update()
    {
        playerHealth = player.GetHealth();
      if (currentTime < timer && !spawnedIsland)
        {
            currentTime += Time.deltaTime;
        }
        else if(currentTime >= timer && !spawnedIsland)
        {
            if (currentIsland != null)
            {
                Destroy(currentIsland);
            }
            playerHealth = player.GetHealth();
            currentTime = 0;
            spawnedIsland = true;
            SetWorldDifficulty(new Vector3(0,0,0));
        }
        //TODO Make island spawn after player leaves one already (I.e. enters another area)
        
    }

    void SpawnIsland(Vector3 islandPos)
    {
        GameObject spawnedIsland;
        if(difficulty == 1)
        {
            spawnedIsland = (GameObject)Instantiate(easyIslands[0]);
            spawnedIsland.GetComponent<Island>().SetDifficulty(1);
            currentIsland = spawnedIsland;
            gameDirector.currentIsland = spawnedIsland.transform;
            gameDirector.OnIslandSpawn();
        }
        else if(difficulty == 2)
        {
            spawnedIsland = (GameObject)Instantiate(normalIslands[0]);
            spawnedIsland.GetComponent<Island>().SetDifficulty(2);
            currentIsland = spawnedIsland;
            gameDirector.currentIsland = spawnedIsland.transform;
            gameDirector.OnIslandSpawn();
        }
        else if(difficulty == 3)
        {
            spawnedIsland = (GameObject)Instantiate(hardIslands[0]);
            spawnedIsland.GetComponent<Island>().SetDifficulty(3);
            currentIsland = spawnedIsland;
            gameDirector.currentIsland = spawnedIsland.transform;
            gameDirector.OnIslandSpawn();
        }
        else
        {
            Debug.Log("Difficulty out of bounds");
            spawnedIsland = (GameObject)Instantiate(normalIslands[0]);
            spawnedIsland.GetComponent<Island>().SetDifficulty(2);
            gameDirector.OnIslandSpawn();
        }

        spawnedIsland.transform.position = islandPos;
        
    }

    void SetWorldDifficulty(Vector3 islandP)
    {
        if (playerHealth < 30)
        {
            difficulty = 1;
        }
        else if(playerHealth > 30 && playerHealth < 80)
        {
            difficulty = 2;
        } 
        else
        {
            difficulty = 3;
        }
        
        SpawnIsland(islandP);
    }

    public void GetIslandStatus()
    {
        Debug.Log("Player is far or has completed island");
        Vector3 spawnPos = player.transform.position + player.transform.forward * islandDistance;
        SetWorldDifficulty(spawnPos);
    }

}
