using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarryCoconut : MonoBehaviour
{
    public GameObject player;                 // Reference to the player object
    public float carryDistanceThreshold = 1f; // Distance within which the player can carry the coconut
    public float boatProximityThreshold = 3f; // Distance within which the player can interact with the boat

    private Transform leftHand;               // Reference to the left hand transform of the player
    public GameObject currentCarriable;       // Currently carried object
    public bool isCarryingCoconut = false;    // Track whether the player is currently carrying an object
    private Coconut coconutScript;            // Reference to the Coconut script
    private GameObject boat;                  // Reference to the boat GameObject

    public CoconutCounter coconutCounter;

    void Start()
    {
        // Find the left hand GameObject of the player
        leftHand = GameObject.FindGameObjectWithTag("LeftHand").transform;
        if (leftHand == null)
        {
            Debug.LogError("Left hand transform not found! Make sure your player has a 'LeftHand' GameObject.");
        }

        // Find the Coconut script
        coconutScript = GameObject.FindWithTag("Coconut").GetComponent<Coconut>();
        if (coconutScript == null)
        {
            Debug.LogError("Coconut script not found! Make sure the 'Coconut' GameObject has the Coconut script attached.");
        }

        // Find the boat GameObject
        boat = GameObject.FindWithTag("Boat");
        if (boat == null)
        {
            Debug.LogError("Boat GameObject not found! Make sure the 'Boat' GameObject is present in the scene.");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E key pressed, attempting to carry, drop, or interact with boat...");
            TryToggleCarriable();
        }
    }

    private void TryToggleCarriable()
    {
        if (isCarryingCoconut && IsNearBoat())  // Ensure IsNearBoat() is called here
        {
            Debug.Log("Coconut destroyed near the boat.");
            currentCarriable = null;
            isCarryingCoconut = false;
            coconutCounter.IncrementCoconuts();
            Destroy(gameObject);  // Destroy the coconut if near the boat

        }
        else if (isCarryingCoconut)
        {
            DropCarriable();  // Drop the object if the player is already carrying it
        }
        else if (!isCarryingCoconut && coconutCounter.GetCoconuts() >= 2)
        {
            PlayerPrefs.SetInt("CoconutCounter", coconutCounter.GetCoconuts());
            SceneManager.LoadScene(3);
        }
        else
        {
            TryCarryCarriable();  // Try to pick up an object if the player isn't carrying one
        }

        // Ensure SwitchCoconutState is called
        if (coconutScript != null)
        {
            coconutScript.SwitchCoconutState(isCarryingCoconut);
        }
        else
        {
            Debug.LogError("Coconut script is missing or not properly referenced.");
        }

    }


    private bool IsNearBoat()
    {
        // Attempt to find the boat if it's not already assigned
        if (boat == null)
        {
            boat = GameObject.FindWithTag("Boat");

            if (boat == null)
            {
                Debug.LogWarning("Boat object not found in the scene.");  // Log if boat is still null
                return false;
            }
            else
            {
                Debug.Log("Boat object found and assigned.");
            }
        }

        float distanceToBoat = Vector3.Distance(transform.position, boat.transform.position);
        Debug.Log("Distance to boat: " + distanceToBoat);  // Log the actual distance

        if (distanceToBoat <= boatProximityThreshold)
        {
            Debug.Log("Player is within range of the boat.");  // Log if within proximity
            return true;
        }
        else
        {
            Debug.Log("Player is NOT within range of the boat.");  // Log if out of proximity
            return false;
        }
    }




    public void TryCarryCarriable()
    {
        // Find the closest object tagged as "Coconut"
        GameObject[] carriables = GameObject.FindGameObjectsWithTag("Coconut");
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
            Debug.Log("isCarryingCoconut is " + isCarryingCoconut);
        }
        else
        {
            Debug.Log("No carriable objects within range.");
        }
    }

    public void CarryCarriable(GameObject carriable)
    {
        // Remove the Rigidbody from the carriable object
        Rigidbody coconutRigidbody = carriable.GetComponent<Rigidbody>();
        if (coconutRigidbody != null)
        {
            Destroy(coconutRigidbody);
            Debug.Log("Rigidbody removed from " + carriable.name);
        }

        // Move the object to the player's left hand position
        Debug.Log("Carrying object in left hand...");
        carriable.transform.position = leftHand.position;         // Set position to left hand
        carriable.transform.rotation = leftHand.rotation;         // Set rotation to match the left hand
        carriable.transform.SetParent(leftHand);                  // Attach the object to the left hand

        currentCarriable = carriable;  // Track the current carriable object
        isCarryingCoconut = true;      // Update the state to reflect that the player is now carrying the object
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
                Rigidbody coconutRigidbody = currentCarriable.AddComponent<Rigidbody>();
                coconutRigidbody.mass = 5f;  // You can adjust the mass as needed
                Debug.Log("Rigidbody added to " + currentCarriable.name + " for dropping.");
            }

            currentCarriable = null;  // Reset current carriable object
            isCarryingCoconut = false; // Update the state to reflect that the player is no longer carrying an object
        }
    }
}
