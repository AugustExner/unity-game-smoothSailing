using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipableR : MonoBehaviour
{
    public GameObject player;                    // Reference to the player object
    public float equippingDistanceThreshold = 3f; // Distance within which the player can carry the equipable
    public float toolProximityThreshold = 3f;    // Distance within which objects must be close to equip

    private Transform rightHand;                 // Reference to the right hand transform of the player
    private GameObject currentEquipable;         // Currently carried object
    private bool isEquipped = false;             // Track whether the player is currently carrying an object

    // Offsets for correct placement of the equipped object
    public Vector3 positionOffset = new Vector3(0.1f, 0f, -0.02f);    // Adjust this for position offset
    public Vector3 rotationOffset = new Vector3(-5.7f, -94.65f, -83.87f);    // Adjust this for rotation offset

    void Start()
    {
        // Find the right hand GameObject of the player
        rightHand = GameObject.FindGameObjectWithTag("RightHand").transform;

        if (rightHand == null)
        {
            Debug.LogError("Right hand transform not found! Make sure your player has a 'RightHand' GameObject.");
        }
        else
        {
            Debug.Log("Right hand transform found: " + rightHand.name);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E key pressed, attempting to equip or drop object...");
            TryToggleEquipable();
        }
    }

    private void TryToggleEquipable()
    {
        if (isEquipped)
        {
            DropEquipable();  // Drop the object if the player is already carrying it
        }
        else
        {
            TryEquipEquipable();  // Try to pick up an object if the player isn't carrying one
        }
    }

    private void TryEquipEquipable()
    {
        // Find the closest object tagged as "Equipable"
        GameObject[] equipables = GameObject.FindGameObjectsWithTag("Equipable");
        GameObject closestEquipable = null;
        float closestDistance = equippingDistanceThreshold;

        foreach (GameObject equipable in equipables)
        {
            float distanceToEquipable = Vector3.Distance(player.transform.position, equipable.transform.position);
            if (distanceToEquipable < closestDistance)
            {
                closestDistance = distanceToEquipable;
                closestEquipable = equipable; // Update closest carriable object
            }
        }

        // If an equipable object is found within the threshold, equip it
        if (closestEquipable != null)
        {
            Debug.Log("Found equipable object: " + closestEquipable.name);
            EquipEquipable(closestEquipable);
        }
        else
        {
            Debug.Log("No equipable objects within range.");
        }
    }

    private void EquipEquipable(GameObject equipable)
    {
        // Remove the Rigidbody from the equipable object
        Rigidbody equipableRigidbody = equipable.GetComponent<Rigidbody>();
        if (equipableRigidbody != null)
        {
            Destroy(equipableRigidbody);
            Debug.Log("Rigidbody removed from " + equipable.name);
        }

        // Move and rotate the object to the player's right hand position with offsets
        equipable.transform.SetParent(rightHand);                   // Attach the object to the right hand
        equipable.transform.localPosition = positionOffset;         // Apply the position offset
        equipable.transform.localRotation = Quaternion.Euler(rotationOffset); // Apply the rotation offset

        currentEquipable = equipable;  // Track the current equipable object
        isEquipped = true;             // Update the state to reflect that the player is now carrying the object
    }

    private void DropEquipable()
    {
        if (currentEquipable != null)
        {
            // Detach the object from the player's hand
            currentEquipable.transform.SetParent(null);  // Unparent the object from the hand
            Debug.Log("Dropped the object from right hand.");

            // Add a Rigidbody component to the object so it falls to the ground
            if (currentEquipable.GetComponent<Rigidbody>() == null)
            {
                Rigidbody equipableRigidbody = currentEquipable.AddComponent<Rigidbody>();
                equipableRigidbody.mass = 5f;  // You can adjust the mass as needed
                Debug.Log("Rigidbody added to " + currentEquipable.name + " for dropping.");
            }

            currentEquipable = null;  // Reset current equipable object
            isEquipped = false;       // Update the state to reflect that the player is no longer carrying an object
        }
    }
}
