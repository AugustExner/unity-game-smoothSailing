using UnityEngine;

public class LogTreeFollower : MonoBehaviour
{
    public GameObject player;              // Reference to the player object
    public float dragDistance = 2f;        // Distance to maintain while dragging
    public float dragForce = 5f;           // Force applied to the log when dragging
    private bool isDragging = false;       // Indicates whether the log is currently being dragged

    void Update()
    {
        // Check if "E" key is pressed and player is close enough
        if (Input.GetKeyDown(KeyCode.E) && IsPlayerCloseEnough())
        {
            isDragging = !isDragging; // Toggle dragging state
            if (isDragging)
            {
                AttachLogToPlayer(); // Attach log to player when starting to drag
            }
            else
            {
                DetachLogFromPlayer(); // Detach log from player when stopping drag
            }
        }

        // If dragging, move the log
        if (isDragging)
        {
            DragLog();
        }
    }

    private bool IsPlayerCloseEnough()
    {
        // Check distance between player and log
        return Vector3.Distance(transform.position, player.transform.position) < 3f; // Adjust as necessary
    }

    private void AttachLogToPlayer()
    {
        // Make the log a child of the player and set its position in front of the player
        transform.SetParent(player.transform);
        transform.localPosition = new Vector3(0, 0, dragDistance);
    }

    private void DetachLogFromPlayer()
    {
        // Detach the log from the player
        transform.SetParent(null);
    }

    private void DragLog()
    {
        // Calculate the desired position in front of the player
        Vector3 targetPosition = player.transform.position + player.transform.forward * dragDistance;

        // Smoothly move the log to the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * dragForce); // Adjust speed as needed
    }
}
