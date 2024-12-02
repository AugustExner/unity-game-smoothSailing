using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayResetThings : MonoBehaviour
{
    public void ResetTutorials()
    {
        PlayerPrefs.SetInt("tutorialNumber", 0);
        PlayerPrefs.SetInt("CoconutCounter", 0);
        PlayerPrefs.SetInt("playerHealth", 8);
        PlayerPrefs.SetInt("firstCoconut", 0);
        Time.timeScale = 1;
    }
}
