using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSwitcher : MonoBehaviour
{
    public GameObject fallenPalmTree;  // The fallen tree asset

    void Start()
    {
        fallenPalmTree.SetActive(false);  // Start with the fallen tree inactive
    }

    void OnMouseDown()
    {
        // Switch from standing palm tree to fallen palm tree
        this.gameObject.SetActive(false);  // Disable standing tree
        fallenPalmTree.SetActive(true);    // Activate fallen tree
    }
}


