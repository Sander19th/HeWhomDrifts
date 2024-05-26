using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannonball : MonoBehaviour
{
    public float lifeAfterCollision = 3.0f; // Time in seconds before the cannonball gets destroyed after collision with non-enemy objects

    private bool hasCollided = false; // Flag to track if the cannonball has collided

    void OnCollisionEnter(Collision collision)
    {
        // Check if the cannonball has not already collided and if the collided object has the tag "Enemy"
        if (!hasCollided && collision.gameObject.CompareTag("Enemy"))
        {
            // Destroy the collided object
            Destroy(collision.gameObject);

            // Destroy the cannonball
            Destroy(gameObject);

            // Set the flag to true to indicate that the cannonball has collided
            hasCollided = true;
        }
    }

    void Start()
    {
        // Destroy the cannonball after the specified time if it hasn't already collided with an enemy
        Destroy(gameObject, lifeAfterCollision);
    }
}

