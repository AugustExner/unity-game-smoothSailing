using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryLogs : MonoBehaviour
{
    public GameObject player;
    public float carryDistanceThreshold = 1f;
    public float logProximityThreshold = 1f;

    private Transform leftHand;
    public GameObject currentCarriable;
    public bool isCarryingLog = false;
    private BuildLog buildLogScript;

    void Start()
    {
        leftHand = GameObject.FindGameObjectWithTag("LeftHand").transform;

        if (leftHand == null)
        {
            Debug.LogError("Left hand transform not found!");
        }

        // Make sure the BuildLog script is referenced correctly
        buildLogScript = GameObject.FindWithTag("Carriable")?.GetComponent<BuildLog>();

        if (buildLogScript == null)
        {
            Debug.LogError("BuildLog script not found! Make sure the 'Log' GameObject has the BuildLog script attached.");
        }
    }

    void Update()
    {
        // Check for the 'E' key press to toggle carrying state
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E key pressed.");
            TryToggleCarriable();
        }
    }

    private void TryToggleCarriable()
    {
        // If the player is already carrying a log, drop it first.
        if (isCarryingLog)
        {
            DropCarriable();
        }
        else
        {
            TryCarryCarriable();
        }

        // Ensure SwitchLogState is called
        if (buildLogScript != null)
        {
            buildLogScript.SwitchLogState(isCarryingLog);
        }
        else
        {
            Debug.LogError("BuildLog script is missing or not properly referenced.");
        }
    }

    public void TryCarryCarriable()
    {
        if (isCarryingLog)
        {
            Debug.Log("Already carrying a log. Skipping carry attempt.");
            return;
        }

        GameObject[] carriables = GameObject.FindGameObjectsWithTag("Carriable");
        GameObject closestCarriable = null;
        float closestDistance = carryDistanceThreshold;

        // Add a debug log for carriables
        Debug.Log($"Found {carriables.Length} carriables in range.");

        foreach (GameObject carriable in carriables)
        {
            float distanceToCarriable = Vector3.Distance(player.transform.position, carriable.transform.position);
            Debug.Log($"Checking carriable: {carriable.name}, Distance: {distanceToCarriable}");

            if (distanceToCarriable < closestDistance)
            {
                closestDistance = distanceToCarriable;
                closestCarriable = carriable;
            }
        }

        if (closestCarriable != null)
        {
            // Check for proximity between multiple logs before allowing pickup
            if (CheckForNearbyLogs(closestCarriable))
            {
                CarryCarriable(closestCarriable);
            }
            else
            {
                Debug.Log("Too many logs close together to pick up more than one.");
            }
        }
        else
        {
            Debug.Log("No carriable objects within range.");
        }
    }

    private bool CheckForNearbyLogs(GameObject carriable)
    {
        GameObject[] carriables = GameObject.FindGameObjectsWithTag("Carriable");
        foreach (GameObject otherCarriable in carriables)
        {
            if (otherCarriable != carriable)  // Don't check the same log
            {
                float distanceBetweenLogs = Vector3.Distance(carriable.transform.position, otherCarriable.transform.position);
                Debug.Log($"Distance between logs: {distanceBetweenLogs}");

                /*if (distanceBetweenLogs < logProximityThreshold)
                {
                    Debug.Log("Logs are too close together.");
                    return false; // If logs are too close, prevent picking up
                }*/
            }
        }

        return true; // No other logs too close, safe to pick up
    }

    public void CarryCarriable(GameObject carriable)
    {
        // Ensure we don't carry more than one log at a time
        if (isCarryingLog)
        {
            Debug.LogWarning("Attempting to carry a log while already carrying one.");
            return; // Exit early if already carrying a log
        }

        Rigidbody logRigidbody = carriable.GetComponent<Rigidbody>();
        if (logRigidbody != null)
        {
            Destroy(logRigidbody); // Remove physics while carrying
        }

        carriable.transform.SetParent(leftHand); // Attach to left hand
        carriable.transform.localPosition = new Vector3(0.281763494f, -0.262958169f, 0.390331596f);
        carriable.transform.localRotation = Quaternion.Euler(313.231903f, 84.8847809f, 315.722443f);

        currentCarriable = carriable;
        isCarryingLog = true;
        Debug.Log($"Carrying log: {carriable.name}, isCarryingLog = {isCarryingLog}");
    }

    private void DropCarriable()
    {
        if (currentCarriable != null)
        {
            currentCarriable.transform.SetParent(null);

            // Add physics back if necessary
            if (currentCarriable.GetComponent<Rigidbody>() == null)
            {
                Rigidbody logRigidbody = currentCarriable.AddComponent<Rigidbody>();
                logRigidbody.mass = 5f;
            }

            currentCarriable = null;
            isCarryingLog = false;
            Debug.Log($"Dropped log, isCarryingLog = {isCarryingLog}");
        }
        else
        {
            Debug.LogWarning("No carriable object to drop.");
        }
    }
}
