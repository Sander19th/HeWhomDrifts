using UnityEngine;
using UnityEngine.UI;

public class CrosshairController : MonoBehaviour
{
    public Transform playerTransform; // Reference to the player transform
    public Drifting playerDriftingScript; // Reference to the Drifting script
    public float maxOffset = 50.0f; // Maximum offset distance for the crosshair

    private RectTransform crosshairRectTransform; // RectTransform of the crosshair

    void Start()
    {
        // Get the RectTransform component of the crosshair
        crosshairRectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        // Get the horizontal input from the player
        float moveHorizontal = Input.GetAxis("Horizontal");

        // Calculate the offset based on the rotation angle and horizontal input
        float currentRotationAngle = playerDriftingScript.rotationAngle;
        float offset = -moveHorizontal * currentRotationAngle / playerDriftingScript.rotationAngle * maxOffset;

        // Update the crosshair's position
        crosshairRectTransform.anchoredPosition = new Vector2(offset, crosshairRectTransform.anchoredPosition.y);
    }
}
