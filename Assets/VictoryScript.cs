using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryScript : MonoBehaviour
{
    public VictoryScreen victoryScreen; // Reference to the VictoryScreen script

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player entered the collider
        if (other.CompareTag("Player"))
        {
            // Activate the victory screen
            victoryScreen.setup();

            // Disable player movement
            other.gameObject.GetComponent<Drifting>().enabled = false;

            // Optionally, disable other player components as needed
            // other.gameObject.GetComponent<OtherComponent>().enabled = false;
        }
    }
}
