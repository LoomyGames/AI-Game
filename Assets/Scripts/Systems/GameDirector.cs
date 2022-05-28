using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    PlaneController player;

    public int playerHealth = 100;
    public int playerKills = 0;
    public int playerAmmo = 1000;
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlaneController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetPlayerInfo()
    {
        //get the player's data
        playerHealth = player.health;
        playerKills = player.kills;
        playerAmmo = player.ammo;
    }
}
