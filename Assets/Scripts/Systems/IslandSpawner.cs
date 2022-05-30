using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IslandSpawner : MonoBehaviour
{
    GameObject player; //player reference
    public GameObject fortressPrefab; //fortress base prefab (that has the fortress script on it)

    GameObject spawnedFortress; //the actual spawned fortress 
    Fortress fortressGenerator; //the fortress script on the fortress object

    float distanceToFortress; //the distance of the player to the fortress
    bool isSpawned = true; //is the fortress spawned

    public float maxDistanceToFortress = 1500f; //maximum distance of the player to the fortress before it despawns
    public Text destroyText; //the text confirming the destruction of the fortress
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player"); //find the player
        SpawnFortress(); //spawn the fortress
    }

    // Update is called once per frame
    void Update()
    {
        if(spawnedFortress != null) //if there is a fortress object
        {
            distanceToFortress = Vector3.Distance(player.transform.position, spawnedFortress.transform.position); //get the distance to the player
            if (distanceToFortress > maxDistanceToFortress || fortressGenerator.isComplete && isSpawned) //if the distance is too large or the fortress 
            {                                                                                            //has been completed, destroy it
                if (fortressGenerator.isComplete) //only if the fortress is completed, then give the player some health back
                {
                    player.GetComponent<PlaneController>().health += 20;
                }
                Destroy(spawnedFortress.GetComponent<Fortress>().currentPlane); //otherwise just destroy the currently flying enemy plane
                Destroy(spawnedFortress); //destroy the fortress object
                destroyText.text = "Fortress Despawned or Completed... Respawning at player's location"; //show the confirmation text
                StartCoroutine(FortressCooldown()); //start a cooldown before spawning a new fortress
                isSpawned = false;
            }
        }
    }

    void SpawnFortress() //method for instantiating the fortress
    {
        spawnedFortress = Instantiate(fortressPrefab); //create the fortress and set its location in front of the player
        spawnedFortress.transform.position = player.transform.position + player.transform.forward * 200;
        fortressGenerator = spawnedFortress.GetComponent<Fortress>(); //get the fortress script component
        isSpawned = true;
    }

    IEnumerator FortressCooldown() //the cooldown script for spawning a new fortress
    {
        yield return new WaitForSeconds(1f); //wait 1 second
        SpawnFortress(); //spawn a new fortress
        destroyText.text = ""; //set the confirmation text to nothing
    }

}
