using UnityEngine;

public class BuildLog : InteractableBase
{
    public float interactionDistanceThreshold = 3f;  // Distance within which interaction is allowed

    protected override void Start()
    {
        base.Start();
        interactionMessage = "Press E to pick up the log";
    }

    public void SwitchLogState(bool isCarryingLog)
    {
        // Check the distance between the player and the log
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

        if (distanceToPlayer <= interactionDistanceThreshold)  // Only show interaction if within range
        {
            if (isCarryingLog)
            {
                // If the player is carrying the log, hide the interaction text
                HideInteractionText(); // This hides the interaction text
                Debug.Log("Log is being carried, interaction text hidden.");
            }
            else
            {
                // Optionally, you can show the interaction text again if needed
                ShowInteractionText(); // This shows the interaction text
                Debug.Log("Log is not being carried, interaction text visible.");
            }
        }
        else
        {
            // Optionally, hide the interaction text when the player is out of range
            HideInteractionText(); // This ensures interaction text is hidden if out of range
            Debug.Log("Player is too far from the log, interaction text hidden.");
        }
    }
}
