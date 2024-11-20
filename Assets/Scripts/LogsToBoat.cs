using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class LogsToBoat : MonoBehaviour
{
    public GameObject Wood_BoatV1;               // Assign your Wood_BoatV1 prefab here
    public GameObject player;                    // Reference to the player object
    public GameObject boatSpawner;               // Reference to the BoatSpawner object
    public float spawnProximityThreshold = 10f;   // Distance within which logs and player must be close to the BoatSpawner
    public Transform boatSpawnLocation;          // Specific location inside BoatSpawner where the boat will spawn
    private static bool isBoatSpawned = false;   // Static to ensure only one boat is spawned
    private BuildLog buildLogScript;             // Reference to BuildLog script for hiding interaction text
    public GameObject TransparentBoat;

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
        // Calculate the distance between the player and the BoatSpawner
        if (boatSpawner != null)
        {
            float distanceToPlayer = Vector3.Distance(player.transform.position, boatSpawner.transform.position);
            float distanceToLog = Vector3.Distance(transform.position, boatSpawner.transform.position);

            // Log distances for debugging
            Debug.Log($"Distance to Player: {distanceToPlayer}, Distance to Log: {distanceToLog}");

            // Check if both the player and logs are within proximity of the BoatSpawner
            if (distanceToPlayer <= spawnProximityThreshold && distanceToLog <= spawnProximityThreshold && AreLogsNearby(out List<GameObject> nearbyLogs))
            {
                Debug.Log("Player and logs are within proximity, and logs are nearby. Attempting to spawn boat.");
                SpawnBoat(nearbyLogs);
            }
            else
            {
                Debug.Log("Conditions not met. Player or logs are not in proximity.");
            }
        }
    }

    private bool AreLogsNearby(out List<GameObject> nearbyLogs)
    {
        nearbyLogs = new List<GameObject>();
        // Find all objects tagged as "Carriable"
        GameObject[] logs = GameObject.FindGameObjectsWithTag("Carriable");

        // Check how many logs are within proximity of this log
        foreach (GameObject log in logs)
        {
            if (log != gameObject) // Ignore self
            {
                float distanceToLog = Vector3.Distance(transform.position, log.transform.position);
                if (distanceToLog <= spawnProximityThreshold)
                {
                    nearbyLogs.Add(log);
                }
            }
        }

        // Include this log in the total count for proximity check
        nearbyLogs.Add(gameObject);

        // Check if there are at least 2 logs nearby (including the current one)
        bool logsNearby = nearbyLogs.Count >= 1;
        Debug.Log(logsNearby ? "Logs are nearby." : "Not enough logs nearby.");
        return logsNearby; // Must have at least 2 logs nearby
    }

    private void SpawnBoat(List<GameObject> nearbyLogs)
    {
        if (isBoatSpawned)
        {
            Debug.Log("Boat has already been spawned.");
            return; // Prevent spawning if a boat has already been spawned
        }

        // Log that the boat is being spawned
        Debug.Log("Spawning the boat.");

        // Use the boatSpawnLocation (within the BoatSpawner) for spawning the boat
        Vector3 spawnPosition = boatSpawnLocation.position;

        // Define the desired rotation for the boat (adjust these values as needed)
        Quaternion boatRotation = Quaternion.Euler(0, 110, 0); // Example: Rotates the boat 90 degrees on the Y-axis

        // Instantiate the boat prefab at the specific spawn position within the BoatSpawner
        GameObject boat = Instantiate(Wood_BoatV1, spawnPosition, boatRotation);

        // Destroy the current log and all nearby logs
        Destroy(gameObject); // Destroy this log
        foreach (GameObject log in nearbyLogs)
        {
            Destroy(log); // Destroy each nearby log
        }
        GameObject transparentBoat = GameObject.FindGameObjectWithTag("TransparentBoat");
        Destroy(transparentBoat);

        // Set the boat's parent to the BoatSpawner (optional, to keep hierarchy clean)
        boat.transform.SetParent(boatSpawner.transform);

       

        isBoatSpawned = true; // Update state to indicate a boat has been spawned
    }
}
