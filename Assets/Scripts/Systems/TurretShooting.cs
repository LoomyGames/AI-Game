using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShooting : MonoBehaviour
{
    public Transform shootingPoint; //the point where the bullets spawn
    public GameObject projectile; //the bullet prefab
    public float rotationSpeed = 2; //values needed for calculations
    public float launchVelocity = 1000f;
    public float shootingRange = 200f;
    GameObject player; //player ship 
    public float fireRate = 6; //bullet spawn rate
    float lastFired; //time last fired
    private float distanceToPlayer; //distance to the player ship object

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player"); //find the player reference
    }
    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position); //calculate the distance to the player
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.transform.position
            - transform.position), rotationSpeed * Time.deltaTime); //calculate the rotation towards the player
        if(distanceToPlayer <= shootingRange) //if the player is in range, start shooting
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if (Time.time - lastFired > 1 / fireRate) //if enough time passed after the last time the turret shot
        {
            lastFired = Time.time; //it just shot now
            GameObject ball = Instantiate(projectile, shootingPoint.transform.position, shootingPoint.transform.rotation);
            ball.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 0, launchVelocity)); //create the bullet and shoot it
        }
    }
}
