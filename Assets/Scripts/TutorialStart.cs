using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialStart : MonoBehaviour
{
    public TutorialScript tutorialScript;
    public int tutorialNumber;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        tutorialScript.SetTutorial(tutorialNumber);
    }

    // Update is called once per frame
    void Update()
    {

    }
}