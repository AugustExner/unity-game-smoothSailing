using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallenTreeSwitcher : MonoBehaviour
{
    public GameObject logPalmTree;  // The log tree asset
    public float interactDistance = 3f;  // Define how close the player must be to interact
    private GameObject player;           // Reference to the player object

    void Start()
    {
        logPalmTree.SetActive(false);  // Ensure the log tree is inactive at the start
        player = GameObject.FindWithTag("Player"); // Find the player in the scene (assuming the player is tagged as "Player")
    }

    void Update()
    {
        // Check if the player is within interaction distance
        if (Vector3.Distance(player.transform.position, transform.position) <= interactDistance)
        {
            // Check if the "E" key is pressed
            if (Input.GetKeyDown(KeyCode.E))
            {
                SwitchToLogTree();
            }
        }
    }

    private void SwitchToLogTree()
    {
        // Switch from fallen palm tree to log palm tree
        this.gameObject.SetActive(false);  // Disable fallen tree
        logPalmTree.SetActive(true);       // Activate log tree
    }
}
