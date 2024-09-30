using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogsToBoat : MonoBehaviour
{
    public GameObject Wood_BoatV1;              // Assign your Wood_BoatV1 prefab here
    public GameObject player;                     // Reference to the player object
    public float spawnDistanceThreshold = 3f;    // Distance within which the player can spawn the boat
    public float logProximityThreshold = 3f;     // Distance within which logs must be close to each other
    private static bool isBoatSpawned = false;   // Static to ensure only one boat is spawned

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
        if (distanceToPlayer <= spawnDistanceThreshold && AreLogsNearby(out List<GameObject> nearbyLogs))
        {
            SpawnBoat(nearbyLogs);
        }
    }

    private bool AreLogsNearby(out List<GameObject> nearbyLogs)
    {
        nearbyLogs = new List<GameObject>();
        // Find all objects tagged as "logPalmTree"
        GameObject[] logs = GameObject.FindGameObjectsWithTag("Carriable");

        // Check how many logs are within the proximity of this logPalmTree
        foreach (GameObject log in logs)
        {
            if (log != gameObject) // Ignore self
            {
                float distanceToLog = Vector3.Distance(transform.position, log.transform.position);
                if (distanceToLog <= logProximityThreshold)
                {
                    nearbyLogs.Add(log);
                }
            }
        }

        // Include this log in the total count for proximity check
        nearbyLogs.Add(gameObject);

        // Check if there are at least 2 logs nearby (including the current one)
        return nearbyLogs.Count >= 2; // Must have at least 2 logs nearby
    }

    private void SpawnBoat(List<GameObject> nearbyLogs)
    {
        if (isBoatSpawned)
            return; // Prevent spawning if a boat has already been spawned

        // Define the desired rotation for the boat (adjust these values as needed)
        Quaternion boatRotation = Quaternion.Euler(0, 90, 0); // Example: Rotates the boat 90 degrees on the Y-axis

        // Instantiate the boat prefab at the current position and the desired rotation
        GameObject boat = Instantiate(Wood_BoatV1, transform.position, boatRotation);

        // Destroy the current log and all nearby logs
        Destroy(gameObject); // Destroy this log
        foreach (GameObject log in nearbyLogs)
        {
            Destroy(log); // Destroy each nearby log
        }

        // Set the boat's parent to keep the hierarchy clean (optional)
        boat.transform.SetParent(transform.parent);

        isBoatSpawned = true; // Update state to indicate a boat has been spawned
    }
}
