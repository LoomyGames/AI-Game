using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneShooting : MonoBehaviour
{
    public Transform shootingPoint;
    public GameObject projectile;
    public float launchVelocity = 700f;
    PlaneController player;
    public float fireRate = 6;
    float lastFired;

    void Start()
    {
        player = GetComponent<PlaneController>();
    }

    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            if (player.ammo > 0)
            {
                if(Time.time - lastFired > 1 / fireRate)
                {
                    lastFired = Time.time;
                    GameObject ball = Instantiate(projectile, shootingPoint.transform.position, shootingPoint.transform.rotation);
                    ball.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 0, launchVelocity));
                    player.ammo--;
                }
            }
        }
    }
    
}
