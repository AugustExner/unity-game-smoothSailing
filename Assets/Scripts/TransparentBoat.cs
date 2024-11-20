using UnityEngine;

public class TransparentBoat : InteractableBase
{
    protected override void Start()
    {
        base.Start();
        interactionMessage = "6 logs are required to build the boat";
    }
}
