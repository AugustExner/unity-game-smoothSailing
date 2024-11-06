using UnityEngine;

public class Coconut : InteractableBase
{
    public float interactionDistanceThreshold = 3f;

    protected override void Start()
    {
        base.Start();
        interactionMessage = "Press E to pick up the coconut";
    }

    public void SwitchCoconutState(bool isCarryingCoconut)
    {
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        if(distanceToPlayer <= interactionDistanceThreshold)
        {
            if (isCarryingCoconut)
            {
                // If the player is carrying the log, hide the interaction text
                HideInteractionText(); // This hides the interaction text
                Debug.Log("Coconut is being carried, interaction text hidden.");
            }
            else
            {
                // Optionally, you can show the interaction text again if needed
                ShowInteractionText(); // Assuming you have a method to show interaction text
                Debug.Log("Coconut is not being carried, interaction text visible.");
            }
        }
        else
        {
            HideInteractionText();
            Debug.Log("Player is too far from the coconut, interaction text hidden");
        }
    }
}
