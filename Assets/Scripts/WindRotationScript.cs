using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindRotationScript : MonoBehaviour
{


    // Wind direction angle in degrees (0 to 360)
    public float windDirectionAngle = 0f;

    // Time until the next random wind change
    private float timeUntilNextChange = 0f;

    // Minimum and maximum time between wind changes
    public float minChangeInterval = 2f;
    public float maxChangeInterval = 5f;

    // Speed of the wind change (how quickly it transitions to the new direction)
    public float windChangeSpeed = 30f;

    // Target angle for random wind change
    private float targetWindDirectionAngle;

    void Start()
    {
        // Initialize with a random target wind direction
        SetRandomWindDirection();
    }

    void Update()
    {
        // Change wind direction gradually towards the target
        ChangeWindDirectionRandomly();

        // Rotate the object to reflect the current wind direction
        RotateWindIndicator(windDirectionAngle);
    }

    void ChangeWindDirectionRandomly()
    {
        // Check if it's time to change wind direction
        timeUntilNextChange -= Time.deltaTime;

        // If it's time, pick a new random direction and reset the interval
        if (timeUntilNextChange <= 0)
        {
            SetRandomWindDirection();
        }

        // Gradually rotate towards the target wind direction
        windDirectionAngle = Mathf.LerpAngle(windDirectionAngle, targetWindDirectionAngle, windChangeSpeed * Time.deltaTime);
    }

    void SetRandomWindDirection()
    {
        // Pick a random new wind direction
        targetWindDirectionAngle = Random.Range(0f, 360f);

        // Set a random interval for the next change
        timeUntilNextChange = Random.Range(minChangeInterval, maxChangeInterval);
    }


    public void RotateWindIndicator(float angle)
    {
        // Rotate the RectTransform to the specified angle
        // Z-axis is used because we're working in 2D (HUD)
        transform.rotation = Quaternion.Euler(0, 0, -angle);
    }

    public float getWindDirectionAngle()
    {
        return windDirectionAngle;
    }
}
