using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannonball : MonoBehaviour
{
    public float lifeAfterCollision = 3.0f; // Time in seconds before the cannonball gets destroyed after collision with non-enemy objects
    public float enemyDestroyDelay = 0.0f; // Time in seconds before the enemy gets destroyed after collision with the cannonball

    private bool hasCollided = false; // Flag to track if the cannonball has collided

    AudioManager audioManager; // Calls on the AudioManager

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!hasCollided)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                // Plays a random enemy death sound
                audioManager.PlayRandomEnemyDeathSound();

                // Disable colliders for all objects tagged "Enemy"
                DisableEnemyColliders();

                // Play the death animation (assuming the enemy has an Animator component)
                Animator enemyAnimator = collision.gameObject.GetComponent<Animator>();
                if (enemyAnimator != null)
                {
                    enemyAnimator.SetTrigger("Death"); // Assumes there is a "Death" trigger in the Animator
                }

                // Destroy the enemy GameObject after a delay
                StartCoroutine(DestroyEnemyAfterDelay(collision.gameObject));

                // Set the flag to true to indicate that the cannonball has collided
                hasCollided = true;
            }
            else if (collision.gameObject.CompareTag("Obstacle"))
            {
                // Play a random wood impact sound
                audioManager.PlayRandomWoodImpactSound();

                // Set the flag to true to indicate that the cannonball has collided
                hasCollided = true;
            }

            // Destroy the cannonball
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Destroy the cannonball after the specified time if it hasn't already collided with an enemy or obstacle
        Destroy(gameObject, lifeAfterCollision);
    }

    private void DisableEnemyColliders()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Collider enemyCollider = enemy.GetComponent<Collider>();
            if (enemyCollider != null)
            {
                enemyCollider.enabled = false;
            }
        }
    }

    private IEnumerator DestroyEnemyAfterDelay(GameObject enemy)
    {
        yield return new WaitForSeconds(enemyDestroyDelay);
        Destroy(enemy);
    }
}
