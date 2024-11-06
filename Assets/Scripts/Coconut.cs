using UnityEngine;

public class Coconut : InteractableBase
{
    protected override void Start()
    {
        base.Start();
        interactionMessage = "Press E to pick up the coconut";
    }
}
