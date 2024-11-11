using UnityEngine;
using UnityEngine.UI;

public class ToggleTutorials : MonoBehaviour
{
    public GameObject sailingTutorialCanvas; // Reference to the Canvas
    public Toggle toggle;                    // Reference to the Toggle component

    private void Start()
    {
        // Initialize canvas visibility based on the toggle's initial state
        sailingTutorialCanvas.SetActive(toggle.isOn);

        // Add a listener to call the method when the toggle is changed
        toggle.onValueChanged.AddListener(ToggleCanvas);
    }

    private void ToggleCanvas(bool isOn)
    {
        sailingTutorialCanvas.SetActive(isOn);
    }

    private void OnDestroy()
    {
        // Remove the listener to prevent potential memory leaks
        toggle.onValueChanged.RemoveListener(ToggleCanvas);
    }
}
