using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipableR : MonoBehaviour
{
    public GameObject player;
    public float equippingDistanceThreshold = 3f;

    private Transform rightHand;
    private Transform axeBelt;
    public GameObject currentEquipable;
    public bool isEquipped = false;
    private CarryLogs carryLogsScript; // Reference to CarryLogs script

    void Start()
    {
        // Get the right hand and axe belt transforms
        rightHand = GameObject.FindGameObjectWithTag("RightHand").transform;
        //axeBelt = GameObject.FindGameObjectWithTag("AxeBelt").transform;

        //// Get the CarryLogs script from the player
        //carryLogsScript = player.GetComponent<CarryLogs>();

        //if (carryLogsScript == null)
        //{
        //    Debug.LogError("CarryLogs script not found on the player. Ensure the player GameObject has a CarryLogs component.");
        //}
    }

    void Update()
    {
        //// Check if the player is carrying a log, and set axe position accordingly
        //if (isEquipped && carryLogsScript != null)
        //{
        //Debug.Log("carryLogsScript is assigned: " + (carryLogsScript != null));
        //Debug.Log("isCarryingLog status: " + carryLogsScript.isCarryingLog);
        //    if (carryLogsScript.isCarryingLog)
        //    {
        //        MoveAxeToBelt();
        //    }
        //    else
        //    {
        //        MoveAxeToRightHand();
        //    }
        //}

        if (Input.GetKeyDown(KeyCode.E))
        {
            TryToggleEquipable();
        }
    }

    public bool IsEquipped()
    {
        return isEquipped;
    }

    private void TryToggleEquipable()
    {
        if (!isEquipped)
        {
            TryEquipEquipable();
        }
    }

    private void TryEquipEquipable()
    {
        GameObject axe = GameObject.Find("Axe_06");

        if (axe != null && Vector3.Distance(player.transform.position, axe.transform.position) <= equippingDistanceThreshold)
        {
            EquipEquipable(axe);
        }
    }

private void EquipEquipable(GameObject equipable)
{
    // Remove Rigidbody if it exists
    Rigidbody equipableRigidbody = equipable.GetComponent<Rigidbody>();
    if (equipableRigidbody != null) Destroy(equipableRigidbody);

    isEquipped = true;
    currentEquipable = equipable;

    // Set currentInteractable to null before disabling the Axe script
    Axe axeScript = equipable.GetComponent<Axe>();
    if (axeScript != null)
    {
        axeScript.UnregisterAsInteractable();
        axeScript.enabled = false;
    }

    // Position the axe based on whether the player is carrying a log
    if (carryLogsScript != null && carryLogsScript.isCarryingLog)
    {
        MoveAxeToBelt();
    }
    else
    {
        MoveAxeToRightHand();
    }
}



    private void MoveAxeToRightHand()
    {
        if (currentEquipable != null && rightHand != null)
        {
            currentEquipable.transform.SetParent(rightHand);
            currentEquipable.transform.localPosition = new Vector3(0.1f, 0f, -0.02f);
            currentEquipable.transform.localRotation = Quaternion.Euler(-5.7f, -94.65f, -83.87f);
            Debug.Log("Moved axe to right hand.");
        }
    }

    private void MoveAxeToBelt()
    {
        if (currentEquipable != null && axeBelt != null)
        {
            currentEquipable.transform.SetParent(axeBelt);
            currentEquipable.transform.localPosition = Vector3.zero;
            currentEquipable.transform.localRotation = Quaternion.identity;
            Debug.Log("Moved axe to belt.");
        }
    }
}
