using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    public TextMeshProUGUI mText;
    private int mIndex = 0;

    string[] tutorials = { "Escape the island by building a boat. Collect coconuts for the trip!. \r\n\nMove on 'WASD'. \r\n\nUse 'E' to interact with objects.",
                           "Press 'A' or 'S' to trim your sails. \r\nTrim them tighter when sailing close to the wind, and ease them out as you sail further away to catch more wind and increase speed.",
                           "Unity",
                           "C#",
                           "TextMeshPro"
    };

    public GameObject tutorial;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("tutorialNumber"))
        {
            SetText(PlayerPrefs.GetInt("tutorialNumber"));
        }
        else
        {
            PlayerPrefs.SetInt("tutorialNumber", 0);
        }


    }

    // Update is called once per frame
    void Update()
    {
        SetText(PlayerPrefs.GetInt("tutorialNumber"));

    }

    void SetText(int index)
    {
        mText.text = tutorials[index];
    }


    public void SetTutorial(int index)
    {
        tutorial.SetActive(true);
        PlayerPrefs.SetInt("tutorialNumber", index);
    }
}