using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryLogs : MonoBehaviour
{
    public GameObject player;                 // Reference to the player object
    public float carryDistanceThreshold = 3f; // Distance within which the player can carry the log

    private Transform leftHand;               // Reference to the left hand transform of the player
    private GameObject currentCarriable;      // Currently carried object
    private bool isCarryingLog = false;       // Track whether the player is currently carrying an object

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

    private void TryCarryCarriable()
    {
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
        }
        else
        {
            Debug.Log("No carriable objects within range.");
        }
    }

    private void CarryCarriable(GameObject carriable)
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

        carriable.transform.position = leftHand.position;         // Set position to left hand
        carriable.transform.rotation = leftHand.rotation;         // Set rotation to match the left hand
        carriable.transform.SetParent(leftHand);                  // Attach the object to the left hand

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
