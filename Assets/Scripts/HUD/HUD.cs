using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class HUD : MonoBehaviour
{
    public GameObject tutorial;
    public GameObject pauseScreen;
    public GameObject interactionBox;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            tutorial.SetActive(false);
        }


        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Pause();  
        }
    }


    public void Pause()
    {
        Cursor.visible = true;
        pauseScreen.SetActive(true);
        interactionBox.SetActive(false);
        Time.timeScale = 0;
    }

    public void Continue()
    {
        pauseScreen.SetActive(false); 
        interactionBox.SetActive(true);
        Time.timeScale = 1;
        Cursor.visible = true;
    }
}
