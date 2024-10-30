using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryLogs : MonoBehaviour
{
    public GameObject player;                 // Reference to the player object
    public float carryDistanceThreshold = 3f; // Distance within which the player can carry the log
    public float logProximityThreshold = 3f;  // Distance within which logs must be close to each other

    private Transform leftHand;               // Reference to the left hand transform of the player
    public GameObject currentCarriable;      // Currently carried object
    public bool isCarryingLog = false;       // Track whether the player is currently carrying an object

    void Start()
    {
        // Find the left hand GameObject of the player
        leftHand = GameObject.FindGameObjectWithTag("LeftHand").transform;

        if (leftHand == null)
        {
            Debug.LogError("Left hand transform not found! Make sure your player has a 'LeftHand' GameObject.");
        }
        else
        {
            Debug.Log("Left hand transform found: " + leftHand.name);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E key pressed, attempting to carry or drop object...");
            TryToggleCarriable();
        }
    }

    private void TryToggleCarriable()
    {
        if (isCarryingLog)
        {
            DropCarriable();  // Drop the object if the player is already carrying it
        }
        else
        {
            TryCarryCarriable();  // Try to pick up an object if the player isn't carrying one
        }
    }

    public void TryCarryCarriable()
    {
        // Check if there are two logs nearby

        // Find the closest object tagged as "Carriable"
        GameObject[] carriables = GameObject.FindGameObjectsWithTag("Carriable");
        GameObject closestCarriable = null;
        float closestDistance = carryDistanceThreshold;

        foreach (GameObject carriable in carriables)
        {
            float distanceToCarriable = Vector3.Distance(player.transform.position, carriable.transform.position);
            if (distanceToCarriable < closestDistance)
            {
                closestDistance = distanceToCarriable;
                closestCarriable = carriable; // Update closest carriable object
            }
        }

        // If a carriable object is found within the threshold, carry it
        if (closestCarriable != null)
        {
            Debug.Log("Found carriable object: " + closestCarriable.name);
            CarryCarriable(closestCarriable);
            Debug.Log("isCarryingLog is " + isCarryingLog);
        }
        else
        {
            Debug.Log("No carriable objects within range.");
        }


    }

    private bool AreTwoLogsNearby()
    {
        // Find all objects tagged as "Carriable" (or specifically "logPalmTree" if needed)
        GameObject[] logs = GameObject.FindGameObjectsWithTag("Carriable");
        int nearbyLogCount = 0;

        // Check how many logs are within the proximity of the player
        foreach (GameObject log in logs)
        {
            float distanceToLog = Vector3.Distance(player.transform.position, log.transform.position);
            if (distanceToLog <= logProximityThreshold)
            {
                nearbyLogCount++;
                if (nearbyLogCount >= 2) // If two logs are found
                {
                    return true; // Indicate that there are two nearby logs
                }
            }
        }

        return false; // No two logs nearby
    }

    public void CarryCarriable(GameObject carriable)
    {
        // Remove the Rigidbody from the carriable object
        Rigidbody logRigidbody = carriable.GetComponent<Rigidbody>();
        if (logRigidbody != null)
        {
            Destroy(logRigidbody);
            Debug.Log("Rigidbody removed from " + carriable.name);
        }

        // Move the object to the player's left hand position
        Debug.Log("Carrying object in left hand...");
        carriable.transform.SetParent(leftHand);
        carriable.transform.localPosition = new Vector3(0.281763494f, -0.262958169f, 0.390331596f);         // Set position to left hand
        carriable.transform.localRotation = Quaternion.Euler(313.231903f, 84.8847809f, 315.722443f);         // Set rotation to match the left hand

        currentCarriable = carriable;  // Track the current carriable object
        isCarryingLog = true;           // Update the state to reflect that the player is now carrying the object
    }

    private void DropCarriable()
    {
        if (currentCarriable != null)
        {
            // Detach the object from the player's hand
            currentCarriable.transform.SetParent(null);  // Unparent the object from the hand
            Debug.Log("Dropped the object from left hand.");

            // Add a Rigidbody component to the object so it falls to the ground
            if (currentCarriable.GetComponent<Rigidbody>() == null)
            {
                Rigidbody logRigidbody = currentCarriable.AddComponent<Rigidbody>();
                logRigidbody.mass = 5f;  // You can adjust the mass as needed
                Debug.Log("Rigidbody added to " + currentCarriable.name + " for dropping.");
            }

            currentCarriable = null;  // Reset current carriable object
            isCarryingLog = false;     // Update the state to reflect that the player is no longer carrying an object
        }
    }
}
