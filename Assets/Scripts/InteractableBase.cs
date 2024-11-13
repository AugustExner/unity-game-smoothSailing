using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using UnityEngine.UI;
using Microsoft.Unity.VisualStudio.Editor; // Only if using TextMeshPro, otherwise use UnityEngine.UI for standard Text

public class InteractableBase : MonoBehaviour
{
    public string interactionMessage = "Press E to interact";  // Custom message for each item

    private static TextMeshProUGUI interactionText; // Static to ensure only one instance controls the UI
    private static UnityEngine.UI.Image interactionImage; // Static to ensure only one instance controls the UI

    public float interactionDistance = 3f;  // Distance to show prompt
    private static InteractableBase currentInteractable; // Track the current interactable in range
    public GameObject player;  // Reference to the player object

    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        

        // Initialize the InteractionText UI if not already done
        if (interactionText == null)
        {
            interactionText = GameObject.FindWithTag("InteractionText")?.GetComponentInChildren<TextMeshProUGUI>();
            if (interactionText == null)
            {
                Debug.LogError("InteractionText UI element not found in Canvas!");
            }
            else
            {
                interactionText.gameObject.SetActive(false);
            }
        }


        // Initialize the InteractionImage UI if not already done
        if (interactionImage == null)
        {
            interactionImage = GameObject.FindWithTag("InteractionText")?.GetComponentInChildren<UnityEngine.UI.Image>();
            if (interactionImage == null)
            {
                Debug.LogError("InteractionText UI element not found in Canvas!");
            }
            else
            {
                interactionImage.gameObject.SetActive(false);
            }
        }



    }

    protected virtual void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

        if (distanceToPlayer <= interactionDistance)
        {
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

    public void ShowInteractionText()
    {
        if (interactionText != null)
        {
            interactionText.text = interactionMessage; // Set the message
            interactionText.gameObject.SetActive(true); // Show the UI element

            interactionImage.gameObject.SetActive(true);
        }
    }

    public void HideInteractionText()
    {
        if (interactionText != null)
        {
            interactionText.gameObject.SetActive(false); // Hide the UI element
            interactionImage.gameObject.SetActive(false);
        }
    }

        public void UnregisterAsInteractable()
    {
        // Unregister as interactable and hide interaction text if this is the current interactable
        if (currentInteractable == this)
        {
            currentInteractable = null;
            HideInteractionText();
        }
    }
}
