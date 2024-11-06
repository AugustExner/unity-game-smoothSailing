using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryLogs : MonoBehaviour
{
    public GameObject player;
    public float carryDistanceThreshold = 3f;
    public float logProximityThreshold = 3f;

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
        buildLogScript = GameObject.FindWithTag("Carriable").GetComponent<BuildLog>();

        if (buildLogScript == null)
        {
            Debug.LogError("BuildLog script not found! Make sure the 'Log' GameObject has the BuildLog script attached.");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryToggleCarriable();
        }
    }

    private void TryToggleCarriable()
    {
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
        GameObject[] carriables = GameObject.FindGameObjectsWithTag("Carriable");
        GameObject closestCarriable = null;
        float closestDistance = carryDistanceThreshold;

        foreach (GameObject carriable in carriables)
        {
            float distanceToCarriable = Vector3.Distance(player.transform.position, carriable.transform.position);
            if (distanceToCarriable < closestDistance)
            {
                closestDistance = distanceToCarriable;
                closestCarriable = carriable;
            }
        }

        if (closestCarriable != null)
        {
            CarryCarriable(closestCarriable);
        }
        else
        {
            Debug.Log("No carriable objects within range.");
        }
    }

    public void CarryCarriable(GameObject carriable)
    {
        Rigidbody logRigidbody = carriable.GetComponent<Rigidbody>();
        if (logRigidbody != null)
        {
            Destroy(logRigidbody);
        }

        carriable.transform.SetParent(leftHand);
        carriable.transform.localPosition = new Vector3(0.281763494f, -0.262958169f, 0.390331596f);
        carriable.transform.localRotation = Quaternion.Euler(313.231903f, 84.8847809f, 315.722443f);

        currentCarriable = carriable;
        isCarryingLog = true;
        Debug.Log("Carrying log: " + carriable.name + ", isCarryingLog = " + isCarryingLog);
    }

    private void DropCarriable()
    {
        if (currentCarriable != null)
        {
            currentCarriable.transform.SetParent(null);

            if (currentCarriable.GetComponent<Rigidbody>() == null)
            {
                Rigidbody logRigidbody = currentCarriable.AddComponent<Rigidbody>();
                logRigidbody.mass = 5f;
            }

            currentCarriable = null;
            isCarryingLog = false;
            Debug.Log("Dropped log, isCarryingLog = " + isCarryingLog);
        }
    }
}
