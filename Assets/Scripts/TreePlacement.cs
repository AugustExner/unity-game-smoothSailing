using UnityEngine;

public class TreePlacement : MonoBehaviour
{
    void Start()
    {
        AdjustHeightToTerrain();
    }

    void AdjustHeightToTerrain()
    {
        // Get the current position of the tree
        Vector3 pos = transform.position;

        // Get the terrain height at the tree's X, Z coordinates
        pos.y = Terrain.activeTerrain.SampleHeight(transform.position);

        // Set the tree's position to match the terrain height
        transform.position = pos;
    }
}
