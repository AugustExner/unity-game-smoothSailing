using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisableCanvasOnButtonPress : MonoBehaviour
{
    public Toggle toggle;                    // Reference to the Toggle component

    public GameObject sailingTutorialCanvas; // Reference to the Canvas

    public void DisableCanvas()
    {
        sailingTutorialCanvas.SetActive(false); // Disable the canvas
        toggle.isOn = false;
    }


    void OnMouseOver()
    {
        //If your mouse hovers over the GameObject with the script attached, output this message
        Debug.Log("Mouse is over GameObject.");
    }
}

