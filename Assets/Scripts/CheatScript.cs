using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheatScript : MonoBehaviour
{
    int skipCount = 0;
    private GameObject player;

    private Vector3 teleportPosition = new Vector3(0f, 0f, 0f);

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");

        if (PlayerPrefs.HasKey("tutorialNumber"))
        {
            PlayerPrefs.SetInt("tutorialNumber", 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            int y = SceneManager.GetActiveScene().buildIndex;
            if (y == 2)
            {
                PlayerPrefs.SetInt("CoconutCounter", 99);
                SceneManager.LoadScene(y + 1);
            }
            else
            {
                switch (skipCount)
                {
                    case 0:
                        // teleport 1
                        teleportPosition = new Vector3(207.800003f, 15f, 76.5999985f);

                        break;
                    case 1:
                        // teleport 2
                        teleportPosition = new Vector3(387.100006f, 15f, 95f);
                        break;
                    case 2:
                        // teleport 3
                        teleportPosition = new Vector3(579.099976f, 15f, 103.800003f);
                        break;
                    case 3:
                        // teleport 3
                        teleportPosition = new Vector3(630.299988f, 15f, 157.0f);
                        break;
                }

                if (player != null)
                {
                    player.SetActive(false);
                    player.transform.position = teleportPosition;
                    player.SetActive(true);
                }
                skipCount++;
            }
        }
    }
}
