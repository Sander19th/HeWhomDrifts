using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannonball : MonoBehaviour
{
    public float lifeAfterCollision = 3.0f; // Time in seconds before the cannonball gets destroyed after collision with non-enemy objects
    public float enemyDestroyDelay = 2.0f; // Time in seconds before the enemy gets destroyed after collision with the cannonball

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

                // Disable all colliders on the specific collided enemy, including the parent and child colliders
                DisableColliders(collision.gameObject);

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

                // Destroy the cannonball
                Destroy(gameObject);
            }
            else if (collision.gameObject.CompareTag("Obstacle"))
            {
                // Play a random wood impact sound
                audioManager.PlayRandomWoodImpactSound();

                // Set the flag to true to indicate that the cannonball has collided
                hasCollided = true;

                // Destroy the cannonball
                Destroy(gameObject);
            }
        }
    }

    void Start()
    {
        // Destroy the cannonball after the specified time if it hasn't already collided with an enemy or obstacle
        Destroy(gameObject, lifeAfterCollision);
    }

    private void DisableColliders(GameObject enemy)
    {
        // Disable colliders on the enemy GameObject itself
        DisableAllColliders(enemy);

        // Disable colliders on the parent if the enemy is a child object
        Transform parent = enemy.transform.parent;
        while (parent != null)
        {
            if (parent.CompareTag("Enemy"))
            {
                DisableAllColliders(parent.gameObject);
            }
            parent = parent.parent;
        }
    }

    private void DisableAllColliders(GameObject obj)
    {
        Collider[] colliders = obj.GetComponents<Collider>();
        foreach (Collider collider in colliders)
        {
            Debug.Log($"Disabling collider: {collider.name} on {collider.gameObject.name}");
            collider.enabled = false;
        }

        Collider[] childColliders = obj.GetComponentsInChildren<Collider>();
        foreach (Collider collider in childColliders)
        {
            Debug.Log($"Disabling child collider: {collider.name} on {collider.gameObject.name}");
            collider.enabled = false;
        }

        // If the object has a CharacterController, disable it as well
        CharacterController characterController = obj.GetComponent<CharacterController>();
        if (characterController != null)
        {
            Debug.Log($"Disabling CharacterController on {obj.name}");
            characterController.enabled = false;
        }
    }

    private IEnumerator DestroyEnemyAfterDelay(GameObject enemy)
    {
        yield return new WaitForSeconds(enemyDestroyDelay);
        Debug.Log($"Destroying enemy: {enemy.name}");
        Destroy(enemy);
    }
}

