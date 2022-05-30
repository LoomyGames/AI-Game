using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShooting : MonoBehaviour
{
    public Transform shootingPoint;
    public GameObject projectile;
    public float rotationSpeed = 2;
    public float launchVelocity = 1000f;
    public float shootingRange = 200f;
    GameObject player;
    public float fireRate = 6;
    float lastFired;
    private float distanceToPlayer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }
    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.transform.position
            - transform.position), rotationSpeed * Time.deltaTime);
        if(distanceToPlayer <= shootingRange)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if (Time.time - lastFired > 1 / fireRate)
        {
            lastFired = Time.time;
            GameObject ball = Instantiate(projectile, shootingPoint.transform.position, shootingPoint.transform.rotation);
            ball.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 0, launchVelocity));
        }
    }
}
