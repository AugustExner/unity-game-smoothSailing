using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogsToBoat : MonoBehaviour
{
    public GameObject Wood_BoatV1;         // Assign your Wood_BoatV1 prefab here
    public GameObject player;              // Reference to the player object
    public float spawnDistanceThreshold = 3f;  // Distance within which the player can spawn the boat
    public float logProximityThreshold = 3f;  // Distance within which logs must be close to each other
    private static bool isBoatSpawned = false;  // Static to ensure only one boat is spawned

    void Update()
    {
        // Check if the "E" key is pressed and the boat hasn't been spawned yet
        if (Input.GetKeyDown(KeyCode.E) && !isBoatSpawned) // Interact with "E"
        {
            TrySpawnBoat();
        }
    }

    private void TrySpawnBoat()
    {
        // Calculate the distance between the player and the log
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

        // Check if the player is within the allowed proximity to spawn the boat
        if (distanceToPlayer <= spawnDistanceThreshold && AreTwoLogsNearby(out GameObject nearbyLog))
        {
            SpawnBoat(nearbyLog);
        }
    }

    private bool AreTwoLogsNearby(out GameObject nearbyLog)
    {
        // Find all objects tagged as "logPalmTree"
        GameObject[] logs = GameObject.FindGameObjectsWithTag("logPalmTree");

        // Check how many logs are within the proximity of this logPalmTree
        foreach (GameObject log in logs)
        {
            if (log != gameObject) // Ignore self
            {
                float distanceToLog = Vector3.Distance(transform.position, log.transform.position);
                if (distanceToLog <= logProximityThreshold)
                {
                    nearbyLog = log; // Found a nearby log
                    return true;
                }
            }
        }

        nearbyLog = null; // No nearby log found
        return false;
    }

    private void SpawnBoat(GameObject nearbyLog)
    {
        if (isBoatSpawned)
            return; // Prevent spawning if a boat has already been spawned

        // Define the desired rotation for the boat (adjust these values as needed)
        Quaternion boatRotation = Quaternion.Euler(0, 90, 0); // Example: Rotates the boat 90 degrees on the Y-axis

        // Instantiate the boat prefab at the current position and the desired rotation
        GameObject boat = Instantiate(Wood_BoatV1, transform.position, boatRotation);

        // Destroy both the current log and the nearby log
        Destroy(gameObject);     // Destroy this log
        Destroy(nearbyLog);      // Destroy the nearby log

        // Set the boat's parent to keep the hierarchy clean (optional)
        boat.transform.SetParent(transform.parent);

        isBoatSpawned = true; // Update state to indicate a boat has been spawned
    }
}
