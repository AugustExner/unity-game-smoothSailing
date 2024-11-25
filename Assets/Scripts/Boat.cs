using UnityEngine;
using UnityEngine.SceneManagement;

public class Boat : InteractableBase
{
    private CoconutCounter coconutCounter;

    protected override void Start()
    {
        base.Start();
        interactionMessage = "Press E to sail the boat";
        coconutCounter = GameObject.FindGameObjectWithTag("CoconutCounter").GetComponent<CoconutCounter>();
    }

    protected override void Update()
    {
        IsEnoughCoconuts();
    }


    void IsEnoughCoconuts()
    {
        if (coconutCounter.GetCoconuts() >= 2)
        {
            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
            if (distanceToPlayer <= interactionDistance)
            {
                interactionMessage = "Press E to sail the boat";
                // If no interactable is active or this one is closer, update the current interactable
                if (currentInteractable == null || distanceToPlayer < Vector3.Distance(player.transform.position, currentInteractable.transform.position))
                {
                    currentInteractable = this;
                    ShowInteractionText();
                }
            }
            else if (currentInteractable == this)
            {
                // Clear the current interactable if the player moves out of range
                HideInteractionText();
                currentInteractable = null;
            }
        }
        else
        {
            if (currentInteractable == null)
            {
                HideInteractionText();
                currentInteractable = null;
            }
        }
    }
}

