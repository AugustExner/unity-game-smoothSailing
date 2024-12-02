using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    public TextMeshProUGUI mText;
    private Transform player;

    string[] tutorials = { "Escape the island by building a boat. Collect coconuts for the trip!. \r\n\nMove on 'WASD'. \r\n\nUse 'E' to interact with objects.",
                           "Press 'W' or 'S' to trim your sails. \r\nTrim them tighter when sailing close to the wind, and ease them out as you sail further away to catch more wind and increase speed.",
                           "The wind direction just changed, be aware! \r\nKeep an eye on the wind indicator beneath your healthbar. ",
                           "Wow! The shark just targeted the turtle instead of the boat!",
                           "Gather coconuts for your trip, you dont know how far you have!",
                           "Use 'G' to drop a coconut behind the boat. \r\nThis can be used to stun an enemy. \r\nUse them wisely!",
                           "You can eat a coconut on 'F' to increase your health"
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
        player = GameObject.FindWithTag("Player").transform;
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