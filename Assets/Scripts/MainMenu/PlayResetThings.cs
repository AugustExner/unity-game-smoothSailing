using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayResetThings : MonoBehaviour
{
    public void ResetTutorials()
    {
        PlayerPrefs.SetInt("tutorialNumber", 0);
    }
}
