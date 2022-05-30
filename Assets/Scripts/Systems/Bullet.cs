using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 20; //the bullet damage
    // Start is called before the first frame update
    void Start() //as soon as the bullet is created it has a lifetime of 4 seconds for optimization
    {
        Destroy(gameObject, 4);
    }

    private void OnCollisionEnter(Collision collision) //when the bullet collides with a collider
    {
        if(collision.gameObject.CompareTag("Player")) //if that colldier belongs to the player
        {
            PlaneController player = collision.gameObject.GetComponent<PlaneController>();
            player.health -= damage / 5 + player.kills; //deal damage to the player based on its kills
        } else if (collision.gameObject.CompareTag("Enemy")) //otherwise if it's an enemy
        {
            collision.gameObject.GetComponent<EnemyController>().health -= damage; //damage it
        }
        Destroy(gameObject); //finally, destroy it upon contact
    }
}
