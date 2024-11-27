using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindRotationScript : MonoBehaviour
{


    // Wind direction angle in degrees (0 to 360)
    private float windDirectionAngle = 0f;
    // Speed of the wind change (how quickly it transitions to the new direction)
    private float windChangeSpeed = 1f;
    // Target angle for random wind change
    public float targetWindDirectionAngle;

    private Transform player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {

        // Change wind direction gradually towards the target
        ChangeWindDirection();

        // Rotate the object to reflect the current wind direction
        RotateWindIndicator(windDirectionAngle);

        //Change wind based on player position
        changeWindBasedOnPlayer(player);
    }

    void ChangeWindDirection()
    {
        // Gradually rotate towards the target wind direction
        windDirectionAngle = Mathf.LerpAngle(windDirectionAngle, targetWindDirectionAngle, windChangeSpeed * Time.deltaTime);
    }

    public void SetWindDirectionAngle(float windAngle)
    {
        targetWindDirectionAngle = windAngle;
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

    void changeWindBasedOnPlayer(Transform player)
    {
        if (player.position.x > 195) {
            targetWindDirectionAngle = 90;
        }

        if (player.position.x > 440)
        {
            targetWindDirectionAngle = 45;
        }

        if (player.position.x > 538)
        {
            targetWindDirectionAngle = 135;
        }

    }
}