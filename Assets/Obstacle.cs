using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    // Damage amount to be inflicted on the player
    public int damageAmount = 10;

    private void OnTriggerEnter(Collider other)
    {
        
        // Check if the collision is with the player
        if (other.CompareTag("Player"))
        {
          
            // Get the HealthSystem component attached to the player
            HealthSystem playerHealth = other.GetComponent<HealthSystem>();
            
            // If the player has a HealthSystem component, apply damage
            if (playerHealth != null)
            {
                // Call the TakeDamage function to apply damage to the player
                playerHealth.TakeDamage(damageAmount);
               
            }
        }
    }
}
