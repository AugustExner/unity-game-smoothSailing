using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogTreeFollower : MonoBehaviour
{
    public GameObject player;  // Reference to the player object
    public float dragDistanceThreshold = 3f;  // Maximum distance for dragging to be allowed
    private bool isDragging = false;  // To track if the log is being dragged

    private void Update()
    {
        // Calculate the distance between the player and the log
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

        // Check if the player is within the allowed proximity to drag
        if (distanceToPlayer <= dragDistanceThreshold)
        {
            // Allow dragging if the mouse button is held down and within the threshold
            if (Input.GetMouseButtonDown(0))
            {
                isDragging = true;  // Start dragging
            }

            if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;  // Stop dragging
            }
        }
        else
        {
            isDragging = false;  // Prevent dragging if outside of proximity
        }

        // If dragging, make the log follow the player's position
        if (isDragging)
        {
            FollowPlayer();
        }
    }

    private void FollowPlayer()
    {
        // Attach the log to the player with an offset (adjust this as necessary)
        transform.position = player.transform.position + new Vector3(0, 0, -2);
    }
}



