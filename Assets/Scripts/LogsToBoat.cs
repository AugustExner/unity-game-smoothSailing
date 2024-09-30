using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogsToBoat : MonoBehaviour
{
    public GameObject Wood_BoatV1;         // Assign your Wood_BoatV1 prefab here
    public GameObject player;              // Reference to the player object
    public float spawnDistanceThreshold = 3f;  // Distance within which the player can spawn the boat
    public float logProximityThreshold = 3f;  // Distance within which logs must be close to each other
    private bool isBoat = false;

    void Update()
    {
        // Check if the "E" key is pressed
        if (Input.GetKeyDown(KeyCode.E)) // Interact with "E"
        {
            TrySpawnBoat();
        }
    }

    private void TrySpawnBoat()
    {
        // Calculate the distance between the player and the log
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

        // Check if the player is within the allowed proximity to spawn the boat
        if (distanceToPlayer <= spawnDistanceThreshold && AreTwoLogsNearby())
        {
            SpawnBoat();
        }
    }

    private bool AreTwoLogsNearby()
    {
        // Find all objects tagged as "logPalmTree"
        GameObject[] logs = GameObject.FindGameObjectsWithTag("logPalmTree");

        int nearbyLogsCount = 0;

        // Check how many logs are within the proximity of this logPalmTree
        foreach (GameObject log in logs)
        {
            if (log != gameObject) // Ignore self
            {
                float distanceToLog = Vector3.Distance(transform.position, log.transform.position);
                if (distanceToLog <= logProximityThreshold)
                {
                    nearbyLogsCount++;
                }
            }
        }

        // Return true if two or more logs are nearby
        return nearbyLogsCount >= 1; // 1 because it’s counting the other log (not itself)
    }

    private void SpawnBoat()
    {
        if (isBoat)
            return; // Prevent spawning if already a boat

        // Define the desired rotation for the boat (adjust these values as needed)
        Quaternion boatRotation = Quaternion.Euler(0, 90, 0); // Example: Rotates the boat 90 degrees on the Y-axis

        // Instantiate the boat prefab at the current position and the desired rotation
        GameObject boat = Instantiate(Wood_BoatV1, transform.position, boatRotation);

        // Optionally destroy the logPalmTree or deactivate it
        Destroy(gameObject);

        // Set the boat's parent to keep the hierarchy clean (optional)
        boat.transform.SetParent(transform.parent);

        isBoat = true; // Update state to indicate the log has been turned into a boat
    }
}
