using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drifting : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float speed = 10.0f;
    public float rotationAngle = 30.0f;
    public float driftSpeedMultiplier = 2.0f;
    public float driftRotationIncrease = 37.0f;
    public float rotationSmoothing = 6.0f;
    public float driftRotationSmoothing = 12.0f;

    private Quaternion originalRotation;
    private Quaternion targetRotation;

    void Start()
    {
        originalRotation = transform.rotation;
        targetRotation = originalRotation;
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.World);

        float moveHorizontal = Input.GetAxis("Horizontal");
        bool isDrifting = Input.GetKey(KeyCode.LeftControl);

        float currentMoveSpeed = moveSpeed;
        float currentRotationAngle = rotationAngle;
        float currentRotationSmoothing = rotationSmoothing;

        if (isDrifting)
        {
            currentMoveSpeed *= driftSpeedMultiplier;
            currentRotationAngle += driftRotationIncrease;
            currentRotationSmoothing = driftRotationSmoothing;
        }

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, 0.0f);
        transform.Translate(movement * currentMoveSpeed * Time.deltaTime, Space.World);

        if (moveHorizontal < 0)
        {
            targetRotation = Quaternion.Euler(0, currentRotationAngle, 0) * originalRotation;
        }
        else if (moveHorizontal > 0)
        {
            targetRotation = Quaternion.Euler(0, -currentRotationAngle, 0) * originalRotation;
        }
        else
        {
            targetRotation = originalRotation;
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * currentRotationSmoothing);
    }
}
