using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drifting : MonoBehaviour
{
    public float moveSpeed = 5.0f; // Base speed of movement along the X-axis
    public float speed = 10.0f; // Speed of movement along the Z-axis
    public float rotationAngle = 30.0f; // Base rotation angle for left and right movement
    public float driftSpeedMultiplier = 2.0f; // Multiplier for speed during drift
    public float driftRotationIncrease = 37.0f; // Additional rotation during drift
    public float rotationSmoothing = 6.0f; // Smoothing factor for normal rotation
    public float driftRotationSmoothing = 12.0f; // Smoothing factor for drift rotation

    private Quaternion originalRotation; // Store the original rotation
    private Quaternion targetRotation; // Target rotation

    // Start is called before the first frame update
    void Start()
    {
        // Save the original rotation
        originalRotation = transform.rotation;
        targetRotation = originalRotation;
    }

    // Update is called once per frame
    void Update()
    {
        // Move forward constantly along the Z-axis
        transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.World);

        // Get input for left and right movement (X-axis)
        float moveHorizontal = Input.GetAxis("Horizontal");

        // Check if drifting
        bool isDrifting = Input.GetKey(KeyCode.LeftControl);

        // Adjust movement speed and rotation angle during drift
        float currentMoveSpeed = moveSpeed;
        float currentRotationAngle = rotationAngle;
        float currentRotationSmoothing = rotationSmoothing;

        if (isDrifting)
        {
            currentMoveSpeed *= driftSpeedMultiplier;
            currentRotationAngle += driftRotationIncrease;
            currentRotationSmoothing = driftRotationSmoothing;
        }

        // Move left and right along the X-axis based on player input
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, 0.0f);
        transform.Translate(movement * currentMoveSpeed * Time.deltaTime, Space.World);

        // Determine the target rotation based on horizontal input and drifting
        if (moveHorizontal < 0)
        {
            // Rotate right (inverse of left movement)
            targetRotation = Quaternion.Euler(0, currentRotationAngle, 0) * originalRotation;
        }
        else if (moveHorizontal > 0)
        {
            // Rotate left (inverse of right movement)
            targetRotation = Quaternion.Euler(0, -currentRotationAngle, 0) * originalRotation;
        }
        else
        {
            // Reset to the original rotation when no horizontal input
            targetRotation = originalRotation;
        }

        // Smoothly interpolate towards the target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * currentRotationSmoothing);
    }
}
