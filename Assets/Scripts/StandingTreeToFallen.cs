using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandingTreeToFallenTree : MonoBehaviour
{
    public GameObject standingPalmTree;  // Assign the standing tree object in the inspector
    public GameObject fallenPalmTree;    // Assign the fallen tree object in the inspector

    public float interactDistance = 3f;  // Define how close the player must be to interact
    private GameObject player;           // Reference to the player object

    void Start()
    {
        fallenPalmTree.SetActive(false);  // Ensure the fallen tree is inactive at the start
        player = GameObject.FindWithTag("Player"); // Find the player in the scene (assuming it is tagged as "Player")
    }

    void Update()
    {
        // Check if the player is within interaction distance
        if (Vector3.Distance(player.transform.position, transform.position) <= interactDistance)
        {
            // Check if the "E" key is pressed
            if (Input.GetKeyDown(KeyCode.E))
            {
                SwitchTreeState();
            }
        }
    }

    private void SwitchTreeState()
    {
        // Check if the standing tree is active
        if (standingPalmTree.activeInHierarchy)
        {
            standingPalmTree.SetActive(false);  // Disable standing tree
            fallenPalmTree.SetActive(true);     // Activate fallen tree
        }
    }
}
