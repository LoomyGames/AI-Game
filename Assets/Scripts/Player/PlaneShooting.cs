using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneShooting : MonoBehaviour
{
    public Transform shootingPoint; //variables needed for the shooting logic
    public GameObject projectile;
    public float launchVelocity = 700f;
    PlaneController player; //player reference
    public float fireRate = 6;
    float lastFired;

    void Start()
    {
        player = GetComponent<PlaneController>(); //find the player
    }

    void Update()
    {
        if (Input.GetButton("Fire1")) //if the user clicks the button (or holds it down for automatic fire) 
        {
            if (player.ammo > 0) //if there is still ammunition
            {
                if(Time.time - lastFired > 1 / fireRate) // if we can fire 
                {
                    lastFired = Time.time; //we fired
                    GameObject ball = Instantiate(projectile, shootingPoint.transform.position, shootingPoint.transform.rotation);
                    ball.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 0, launchVelocity)); //create bullet and add force
                    player.ammo--; //reduce ammo by 1
                }
            }
        }
    }
    
}
