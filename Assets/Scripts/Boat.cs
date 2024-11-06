using UnityEngine;

public class Boat : InteractableBase
{
    protected override void Start()
    {
        base.Start();
        interactionMessage = "Press E to sail the boat";
    }
}
