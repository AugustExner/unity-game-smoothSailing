using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSwitcher : MonoBehaviour
{
    public GameObject standingPalmTree;  // Assign the standing tree object in the inspector
    public GameObject fallenPalmTree;    // Assign the fallen tree object in the inspector

    void Start()
    {
        fallenPalmTree.SetActive(false);  // Ensure the fallen tree is inactive at the start
    }

    void OnMouseDown()
    {
        // Check if the standing tree is active
        if (standingPalmTree.activeInHierarchy)
        {
            standingPalmTree.SetActive(false);  // Disable standing tree
            fallenPalmTree.SetActive(true);     // Activate fallen tree
        }
    }
}

