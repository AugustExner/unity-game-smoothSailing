using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    private int windChangesAmount = 0;

    public TutorialScript tutorialScript;
    private bool coconutTutorial = false;

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
        changeWindBasedOnPlayer();
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

    void changeWindBasedOnPlayer()
    {
        if (!coconutTutorial && player.position.x > 110)
        {
            tutorialScript.SetTutorial(5);
            coconutTutorial = true;
        }

        if (player.position.x > 210 && windChangesAmount < 1)
        {
            targetWindDirectionAngle = 90;
            windChangesAmount++;
            tutorialScript.SetTutorial(2);
        }

        if (player.position.x > 380 && windChangesAmount < 2)
        {
            targetWindDirectionAngle = 320;
            windChangesAmount++;
        }

        if (player.position.x > 440 && windChangesAmount < 3)
        {
            targetWindDirectionAngle = 45;
            windChangesAmount++;
        }

        if (player.position.x > 538 && windChangesAmount < 4)
        {
            targetWindDirectionAngle = 135;
            windChangesAmount++;
        }

        if (player.position.x > 620 && windChangesAmount < 5)
        {
            targetWindDirectionAngle = 300;
            windChangesAmount++;
        }

    }
}