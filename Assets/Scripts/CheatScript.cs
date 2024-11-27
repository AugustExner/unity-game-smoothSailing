using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheatScript : MonoBehaviour
{
    int skipCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        
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
                        break;
                    case 1:
                        // teleport 2
                        break;
                    case 3: 
                        // teleport 3
                        break;
                }
            }
        }
    }
}
