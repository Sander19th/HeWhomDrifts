using System.Collections;
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

    public float driftDuration = 1.0f;
    public float driftCooldown = 3.0f;
    public float speedBoost = 20.0f;
    public float speedBoostDuration = 2.0f;

    private Quaternion originalRotation;
    private Quaternion targetRotation;
    private bool isDrifting = false;
    private bool canDrift = true;
    private bool isSpeedBoosted = false;

    void Start()
    {
        originalRotation = transform.rotation;
        targetRotation = originalRotation;
    }

    void Update()
    {
        float currentSpeed = speed;

        if (isSpeedBoosted)
        {
            currentSpeed = speedBoost;
        }

        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime, Space.World);

        float moveHorizontal = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.LeftControl) && canDrift)
        {
            StartCoroutine(Drift());
        }

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

    IEnumerator Drift()
    {
        isDrifting = true;
        canDrift = false;
        StartCoroutine(SpeedBoost());
        yield return new WaitForSeconds(driftDuration);
        isDrifting = false;
        yield return new WaitForSeconds(driftCooldown);
        canDrift = true;
    }

    IEnumerator SpeedBoost()
    {
        isSpeedBoosted = true;
        yield return new WaitForSeconds(speedBoostDuration);
        isSpeedBoosted = false;
    }
}
