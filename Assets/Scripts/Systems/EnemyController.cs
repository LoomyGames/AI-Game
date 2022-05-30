using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int health = 100; //the enemy plane's stats
    public float rotationSpeed = 2f;
    public float movementSpeed = 10f;
    public float followRange = 100f;
    public float shootRange = 50f;
    public float launchVelocity = 700;
    public GameObject projectile; //the bullet it shoots
    public GameObject shootingPoint; //the spawn location of the bullet
    public float fireRate = 6; //the firing rate
    PlaneController player; //the player reference

    Fortress fortress; //the fortress object

    float lastFired; //time last fired
    float distanceToPlayer; //the distance to the player 
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlaneController>(); //get a reference to the player and the fortress
        fortress = GameObject.FindWithTag("Fortress").GetComponent<Fortress>();
        health += fortress.fortressHealth / 3; //adjust the health of the plane based on the fortress health buff
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position); //get the distance to the player 
        if(health <= 0) //if the enemy health is 0, destroy it
        {
            ProvideDeath();
        }

        if (distanceToPlayer <= shootRange) //if the player is in shooting range, shoot
        {
            ShootPlayer();
        }

        if (distanceToPlayer <= followRange) //if the player is in follow range, keep moving towards it
        {
            MoveTowardsPlayer();

        } else //normally, just don't do anything
        {
            IdleAround();
        }

    }

    void IdleAround()
    {
        //Do nothing
    }

    void MoveTowardsPlayer() //method for moving towards the player constantly
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.transform.position 
            - transform.position), rotationSpeed * Time.deltaTime); //rotate towards the player 
        transform.position += movementSpeed * Time.deltaTime * transform.forward; //translate towards the player
    }

    void ShootPlayer()
    {
        if(Time.time - lastFired > 1 / fireRate) //if the agent can fire
        {
            lastFired = Time.time; //it just fired
            GameObject ball = Instantiate(projectile, shootingPoint.transform.position, shootingPoint.transform.rotation);
            ball.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 0, launchVelocity));//create bullet and add forward force to it
        }
    }

    void ProvideDeath() //method for dying
    {
        if(player.health < 100) //if the player's health is not full, then increase it per each kill
        {
            player.health += 2;
        }
        player.ammo += 100; //add player ammo and increase kill count
        player.kills++;
        fortress.fortressPlanes--; //reduce the number of planes in the fortress
        fortress.isPlane = false; //there is no plane anymore 
        Destroy(gameObject); //destroy the object
    }
}
