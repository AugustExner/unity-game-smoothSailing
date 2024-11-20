using UnityEngine;

public class Axe : InteractableBase
{
    protected override void Start()
    {
        base.Start();
        interactionMessage = "Press E to pick up the axe";
    }
}
