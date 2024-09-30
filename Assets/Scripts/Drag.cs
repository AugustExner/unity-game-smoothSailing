using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    public GameObject player;  // Reference to the player object
    public float dragDistanceThreshold = 3f;  // Maximum distance for dragging to be allowed
    private bool isDragging = false;  // To track if the object is being dragged

    private void Update()
    {
        // Calculate the distance between the player and the object
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

        // Check if the player is within the allowed proximity to drag
        if (distanceToPlayer <= dragDistanceThreshold)
        {
            // Allow dragging if the "E" key is pressed and within the threshold
            if (Input.GetKeyDown(KeyCode.E))
            {
                isDragging = !isDragging;  // Toggle dragging on or off when "E" is pressed
            }
        }

        // If dragging, make the object follow the player's position
        if (isDragging)
        {
            FollowPlayer();
        }
    }

    private void FollowPlayer()
    {
        // Attach the object to the player with an offset (adjust this as necessary)
        transform.position = player.transform.position + new Vector3(0, 0, -2); // Adjust the offset as needed
    }
}
