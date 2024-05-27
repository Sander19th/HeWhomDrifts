using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthStatusBar : MonoBehaviour
{
    public HealthSystem healthSystem; // Reference to the player's health script
    public Image fillImage; // Reference to the image used to fill the healthbar
    private Slider slider; // Reference to the slider

    void Awake()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        //The slider will leave a small block, and this code simply removes the image from the scene to resolve this issue
        if (slider.value <= slider.minValue){
            fillImage.enabled = false;
        }
        // Calculate the fill value
        float fillValue = (float)healthSystem.currentHealth / healthSystem.maxHealth;

        // Set the slider value
        slider.value = fillValue;
    }
}
