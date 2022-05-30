using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int health = 100;
    public float rotationSpeed = 2f;
    public float movementSpeed = 10f;
    public float followRange = 100f;
    public float shootRange = 50f;
    public float launchVelocity = 700;
    public GameObject projectile;
    public GameObject shootingPoint;
    public float fireRate = 6;
    PlaneController player;

    Fortress fortress;

    float lastFired;
    float distanceToPlayer;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlaneController>();
        fortress = GameObject.FindWithTag("Fortress").GetComponent<Fortress>();
        health += fortress.fortressHealth / 3;
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if(health <= 0)
        {
            ProvideDeath();
        }

        if (distanceToPlayer <= shootRange)
        {
            ShootPlayer();
        }

        if (distanceToPlayer <= followRange)
        {
            MoveTowardsPlayer();

        } else
        {
            IdleAround();
        }

    }

    void IdleAround()
    {
        //Do nothing
    }

    void MoveTowardsPlayer()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.transform.position 
            - transform.position), rotationSpeed * Time.deltaTime);
        transform.position += movementSpeed * Time.deltaTime * transform.forward;
    }

    void ShootPlayer()
    {
        if(Time.time - lastFired > 1 / fireRate)
        {
            lastFired = Time.time;
            GameObject ball = Instantiate(projectile, shootingPoint.transform.position, shootingPoint.transform.rotation);
            ball.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 0, launchVelocity));
        }
    }

    void ProvideDeath()
    {
        if(player.health < 100)
        {
            player.health += 2;
        }
        player.ammo += 100;
        player.kills++;
        fortress.fortressPlanes--;
        fortress.isPlane = false;
        Destroy(gameObject);
    }
}
