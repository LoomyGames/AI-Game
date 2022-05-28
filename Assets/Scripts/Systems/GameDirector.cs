using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    PlaneController player;
    IslandSpawner islandSpawner;

    [Range(0,2)]
    public float difficultyCoefficient = 1f;

    float playerHealth = 100f;
    int playerKills = 0;
    int playerAmmo = 1000;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlaneController>();
        islandSpawner = GameObject.FindWithTag("GameManager").GetComponent<IslandSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GetPlayerInfo()
    {
        //get the player's data
        playerHealth = player.health;
        playerKills = player.kills;
        playerAmmo = player.ammo;
    }

    public void SetFortressDifficulty()
    {
        float weightedSum = playerHealth / 2 + playerKills + playerAmmo / 10;
        difficultyCoefficient = weightedSum / (playerHealth + playerKills + playerAmmo);
    }
}
