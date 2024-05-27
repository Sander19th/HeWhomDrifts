using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting;


public class HealthSystem : MonoBehaviour
{
 // Reference to the Game Over script
    public GameOverScript gameOverScript;

    // Health variable to track player's health
    public int maxHealth = 5;
    public int currentHealth;

    void start(){
        currentHealth = maxHealth;
    }

    // Function to reduce player's health
    public void TakeDamage(int damage)
    {

        currentHealth -= damage;
        
        
        // Check if player's health is zero or less
        if (currentHealth <= 0)
        {
        
            // Call the GameOverScript's setup method
            gameOverScript.setup();

               // Disable player movement
                gameObject.SetActive(false);
        }
    }
}
