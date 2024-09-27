using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogsToBoat : MonoBehaviour
{
    public GameObject Wood_BoatV1;      // Assign your Wood_BoatV1 prefab here
    public GameObject player;             // Reference to the player object
    public float spawnDistanceThreshold = 3f;  // Distance within which the player can spawn the boat
    private bool isBoat = false;

    void Update()
    {
        // Check for right mouse button click
        if (Input.GetMouseButtonDown(1)) // Right-click
        {
            TrySpawnBoat();
        }
    }

    private void TrySpawnBoat()
    {
        // Calculate the distance between the player and the log
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

        // Check if the player is within the allowed proximity to spawn the boat
        if (distanceToPlayer <= spawnDistanceThreshold)
        {
            SpawnBoat();
        }
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

        isBoat = true; // Update state
    }
}
