using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAnchor : MonoBehaviour
{
    public GameObject Projetile; // Reference to the projectile prefab
    public Transform spawnPoint; // The point where the projectile will be spawned
    public float baseLaunchForce = 15.0f; // Base launch force
    public float maxAdditionalForce = 30.0f; // Maximum additional force when holding down the space bar
    public float chargeTime = 4.0f; // Time to reach maximum additional force
    public float shotCooldown = 2.0f; // Cooldown between shots
    private float lastShotTime; // Time when the last shot was fired

    private float currentChargeTime = 0.0f; // Time the space bar has been held down

    void Start()
    {
        // Ensure spawnPoint is assigned
        if (spawnPoint == null)
        {
            Debug.LogError("Spawn point not assigned!");
        }
        // Initialize lastShotTime to start of the game
        lastShotTime = -shotCooldown;
    }

    void Update()
    {
        if (Time.time - lastShotTime > shotCooldown) // Check if shot cooldown has passed
        {
            if (Input.GetKey(KeyCode.Space))
            {
                // Increase the charge time while the space bar is held down
                currentChargeTime += Time.deltaTime;
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                // Calculate the launch force based on the charge time
                float launchForce = baseLaunchForce + (maxAdditionalForce * (currentChargeTime / chargeTime));
                launchForce = Mathf.Clamp(launchForce, baseLaunchForce, baseLaunchForce + maxAdditionalForce);

                // Spawn and launch the projectile
                LaunchProjectile(launchForce);

                // Reset the charge time
                currentChargeTime = 0.0f;

                // Update the last shot time
                lastShotTime = Time.time;
            }
        }
    }

    void LaunchProjectile(float launchForce)
    {
        // Instantiate the projectile at the spawn point with the exact rotation of the spawn point
        GameObject projectile = Instantiate(Projetile, spawnPoint.position, spawnPoint.rotation);
        
        // Get the Rigidbody component of the projectile
        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        // Ensure that the Rigidbody is reset (no initial velocity or angular velocity)
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // Calculate launch direction based on the spawn point's forward direction
        Vector3 launchDirection = spawnPoint.forward;

        // Log debug information
        Debug.Log("Spawn Point Position: " + spawnPoint.position);
        Debug.Log("Spawn Point Rotation: " + spawnPoint.rotation.eulerAngles);
        Debug.Log("Calculated Launch Direction: " + launchDirection);
        Debug.Log("Player Position: " + transform.root.position);
        Debug.Log("Player Rotation: " + transform.root.rotation.eulerAngles);

        // Directly set the projectile's velocity
        rb.velocity = launchDirection * launchForce;

        // Log the projectile's initial velocity
        Debug.Log("Projectile Initial Velocity: " + rb.velocity);
    }
}
