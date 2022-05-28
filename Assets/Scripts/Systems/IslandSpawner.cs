using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandSpawner : MonoBehaviour
{
    GameObject player;
    public GameObject islandPrefab;

    GameObject spawnedIsland;

    float distanceToIsland;
    bool isSpawned = true;

    public float maxDistanceToIsland = 1500f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        SpawnIsland();
    }

    // Update is called once per frame
    void Update()
    {
        if(spawnedIsland != null)
        {
            distanceToIsland = Vector3.Distance(player.transform.position, spawnedIsland.transform.position);
        }
        if(distanceToIsland > maxDistanceToIsland && isSpawned)
        {
            Destroy(spawnedIsland);
            StartCoroutine(IslandCooldown());
            isSpawned = false;
        }
    }

    void SpawnIsland()
    {
        spawnedIsland = Instantiate(islandPrefab);
        spawnedIsland.transform.position = player.transform.position + player.transform.forward * 200;
        isSpawned = true;
    }

    IEnumerator IslandCooldown()
    {
        yield return new WaitForSeconds(1f);
        SpawnIsland();
    }

}
