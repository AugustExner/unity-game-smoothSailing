using UnityEngine;

public class Tree : InteractableBase
{
    protected override void Start()
    {
        base.Start();
        interactionMessage = "Press E to pick up the tree";
    }
}
