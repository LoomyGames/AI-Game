using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandSpawner : MonoBehaviour
{
    GameObject player;
    public GameObject fortressPrefab;

    GameObject spawnedFortress;
    Fortress fortressGenerator;

    float distanceToFortress;
    bool isSpawned = true;

    public float maxDistanceToFortress = 1500f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        SpawnFortress();
    }

    // Update is called once per frame
    void Update()
    {
        if(spawnedFortress != null)
        {
            distanceToFortress = Vector3.Distance(player.transform.position, spawnedFortress.transform.position);
        }
        if(distanceToFortress > maxDistanceToFortress || fortressGenerator.isComplete && isSpawned)
        {
            Destroy(spawnedFortress);
            StartCoroutine(FortressCooldown());
            isSpawned = false;
        }
    }

    void SpawnFortress()
    {
        spawnedFortress = Instantiate(fortressPrefab);
        spawnedFortress.transform.position = player.transform.position + player.transform.forward * 200;
        fortressGenerator = spawnedFortress.GetComponent<Fortress>();
        isSpawned = true;
    }

    IEnumerator FortressCooldown()
    {
        yield return new WaitForSeconds(1f);
        SpawnFortress();
    }

}
