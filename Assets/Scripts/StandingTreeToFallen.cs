using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandingTreeToFallenTree : MonoBehaviour
{
    public GameObject standingPalmTree;  // Assign the standing tree object in the inspector
    public GameObject trunkPalmTree;      // Assign the fallen tree object in the inspector
    public GameObject buildLog;           // Assign the log object in the inspector
    public float interactDistance = 3f;   // Define how close the player must be to interact
    private GameObject player;            // Reference to the player object
    private EquipableR equipableScript;   // Reference to the EquipableR script on Axe_06

    void Start()
    {
        trunkPalmTree.SetActive(false);    // Ensure the trunk is initially inactive
        buildLog.SetActive(false);          // Ensure the build log is initially inactive
        player = GameObject.FindWithTag("Player"); // Find the player in the scene

        // Find the Axe_06 object and get its EquipableR script
        GameObject axe = GameObject.FindWithTag("Equipable");
        if (axe != null)
        {
            equipableScript = axe.GetComponent<EquipableR>();
        }
        else
        {
            Debug.LogWarning("Axe_06 not found in the scene.");
        }
    }

    void Update()
    {
        // Check if the player is within interaction distance
        if (Vector3.Distance(player.transform.position, transform.position) <= interactDistance)
        {
            // Check if the "E" key is pressed and Axe_06 is equipped
            if (Input.GetKeyDown(KeyCode.E) && equipableScript != null && equipableScript.IsEquipped())
            {
                SwitchTreeState(); // Directly call the method to switch the tree state
            }
        }
    }

    public void SwitchTreeState()
    {
        // Check if the standing tree is active
        if (standingPalmTree.activeInHierarchy)
        {
            standingPalmTree.SetActive(false); // Disable standing tree
            trunkPalmTree.SetActive(true);      // Activate fallen tree
            buildLog.SetActive(true);           // Activate the log
            Debug.Log("Tree state switched to fallen.");
        }
    }
}
