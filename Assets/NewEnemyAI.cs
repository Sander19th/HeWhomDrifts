using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewEnemyAI : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public GameObject Projetile; // Reference to the projectile prefab
    public Transform shootPoint; // The point from where the projectile will be shot
    public float attackRange = 15f; // The range within which the enemy can attack the player
    public float shootInterval = 2f; // Time between each shot
    public float projectileSpeed = 10f; // Speed of the projectile

    private float lastShootTime = 0f; // The last time the enemy shot a projectile

    void Update()
    {
        if (player == null)
        {
            Debug.LogError("Player not assigned!");
            return;
        }

        // Calculate the distance to the player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // If the player is within attack range, look at the player and shoot
        if (distanceToPlayer <= attackRange)
        {
            // Rotate to face the player
            Vector3 direction = (player.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

            // Adjust rotation to match model's forward direction if necessary
            transform.rotation *= Quaternion.Euler(0, 180, 0); // This might be necessary depending on the model's orientation

            // Shoot at intervals
            if (Time.time >= lastShootTime + shootInterval)
            {
                Shoot();
                lastShootTime = Time.time;
            }
        }
    }

    void Shoot()
    {
        // Instantiate the projectile at the shoot point
        GameObject projectile = Instantiate(Projetile, shootPoint.position, shootPoint.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        // Calculate the direction to shoot the projectile
        Vector3 shootDirection = (player.position - shootPoint.position).normalized;

        // Set the velocity of the projectile
        rb.velocity = shootDirection * projectileSpeed;

        // Optional: Add any additional logic for the projectile
        Debug.Log("Projectile shot towards player");
    }

    void OnDrawGizmosSelected()
    {
        // Draw the attack range in the editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
