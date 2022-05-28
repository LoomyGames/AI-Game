using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneShooting : MonoBehaviour
{
    public Transform shootingPoint;
    public GameObject projectile;
    public float launchVelocity = 700f;
    PlaneController player;

    void Start()
    {
        player = GetComponent<PlaneController>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (player.ammo > 0)
            {
                GameObject ball = Instantiate(projectile, shootingPoint.transform.position, shootingPoint.transform.rotation);
                ball.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 0, launchVelocity));
                Destroy(ball, 4);
            }
        }
    }
    
}
