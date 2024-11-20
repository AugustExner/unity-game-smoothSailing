using UnityEngine;

public class Tree : InteractableBase
{
    private bool isTreeFallen = false;

    protected override void Start()
    {
        base.Start();
        interactionMessage = "Press E to cut down the tree";
    }

    // Method to handle tree fall logic and interaction text visibility
    public void SwitchTreeState()
    {
        if (!isTreeFallen)
        {
            // Change tree state to fallen
            isTreeFallen = true;
            HideInteractionText(); // Hide the interaction text when the tree falls
            // Optionally deactivate the tree, or any other logic you want to apply
            gameObject.SetActive(false); // Or any other logic for switching the state
            Debug.Log("Tree has fallen, interaction text hidden.");
        }
    }
}
