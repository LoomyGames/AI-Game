using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameDirector : MonoBehaviour
{
    PlaneController player; //player reference

    public int playerHealth = 100; //player stats
    public int playerKills = 0;
    public int playerAmmo = 1000;
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlaneController>(); //find the player 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) //if the user presses P, then go back to the menu
        {
            SceneManager.LoadScene(0);
        }
    }

    public void GetPlayerInfo()
    {
        //get the player's current stats
        playerHealth = player.health;
        playerKills = player.kills;
        playerAmmo = player.ammo;
    }
}
