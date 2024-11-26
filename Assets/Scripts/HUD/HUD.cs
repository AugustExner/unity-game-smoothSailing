using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class HUD : MonoBehaviour
{
    public GameObject tutorial;
    public GameObject pauseScreen;
    public GameObject interactionBox;
    public GameObject music;

    private BoatController boatController;
    private bool isPaused = false;

    private void Start()
    {
        // Locate the "Wood_BoatV1" child and get the BoatController component
        GameObject woodBoat = GameObject.Find("Wood_BoatV1");
        if (woodBoat != null)
        {
            boatController = woodBoat.GetComponent<BoatController>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            tutorial.SetActive(false);
            Debug.Log("Enter");
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Continue();
            }
            else
            {
                Pause();
                isPaused = true;
            }
        }
    }

    public void Pause()
    {
        Cursor.visible = true;
        pauseScreen.SetActive(true);
        if (interactionBox)
        {
            interactionBox.SetActive(false);
        }
        Time.timeScale = 0;


        //Deactivate BoatController
        if (boatController != null)
        {
            boatController.enabled = false;
        }
        music.SetActive(false);
    }

    public void Continue()
    {
        isPaused = false;
        pauseScreen.SetActive(false);
        if (interactionBox)
        {
            interactionBox.SetActive(true);
        }
        Time.timeScale = 1;
        Cursor.visible = true;
        music.SetActive(true);

        // Reactivate BoatController 
        if (boatController != null)
        {
            boatController.enabled = true;
        }
    }
}