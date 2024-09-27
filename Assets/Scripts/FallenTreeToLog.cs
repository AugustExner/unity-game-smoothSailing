using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallenTreeSwitcher : MonoBehaviour
{
    public GameObject logPalmTree;  // The log tree asset

    void Start()
    {
        logPalmTree.SetActive(false);  // Start with the log tree inactive
    }

    void OnMouseDown()
    {
        // Switch from fallen palm tree to log palm tree
        this.gameObject.SetActive(false);  // Disable fallen tree
        logPalmTree.SetActive(true);       // Activate log tree
    }
}

