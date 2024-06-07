using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewEnemyAI : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public GameObject projectile; // Reference to the projectile prefab
    public Transform shootPoint; // The point from where the projectile will be shot
    public float attackRange = 15f; // The range within which the enemy can attack the player
    public float shootInterval = 2f; // Time between each shot
    public float projectileSpeed = 10f; // Speed of the projectile

    public int damageAmount = 10; // Damage amount to be inflicted on the player

    private float lastShootTime = 0f; // The last time the enemy shot a projectile

    private AudioManager audioManager; // Reference to the AudioManager
    private Animator animator; // Reference to the Animator component

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        // Find the Animator component on the child GameObject
        animator = GetComponentInChildren<Animator>();

        // Check if the Animator component is attached
        if (animator == null)
        {
            Debug.LogError("Animator component not found on the enemy GameObject or its children");
        }
    }

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
        GameObject projectileInstance = Instantiate(projectile, shootPoint.position, shootPoint.rotation);
        Rigidbody rb = projectileInstance.GetComponent<Rigidbody>();

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

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collision is with the player
        if (other.CompareTag("Player"))
        {
            // Play the enemy attack sound
            audioManager.PlaySFX(audioManager.enemyAttack1);

            // Check if Animator is attached before triggering the attack animation
            if (animator != null)
            {
                animator.SetTrigger("Attack");
            }

            Debug.Log("Ouch! Player collision detected!");

            // Get the HealthSystem component attached to the player
            HealthSystem playerHealth = other.GetComponent<HealthSystem>();

            // If the player has a HealthSystem component, apply damage
            if (playerHealth != null)
            {
                // Call the TakeDamage function to apply damage to the player
                playerHealth.TakeDamage(damageAmount);
            }
            else
            {
                Debug.LogWarning("HealthSystem component not found on player");
            }
        }
    }
}
