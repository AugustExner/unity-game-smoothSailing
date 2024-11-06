using UnityEngine;
using UnityEngine.UIElements;

public class ToggleUIDocument : MonoBehaviour
{
    private UIDocument uiDocument;
    private bool isVisible = false;

    void Start()
    {
        // Get the UIDocument component
        uiDocument = GetComponent<UIDocument>();

        // Ensure UI is hidden at start
        ToggleVisibility(false);
    }

    void Update()
    {
        // Check if the ESC key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isVisible = !isVisible;
            ToggleVisibility(isVisible);
        }
    }

    private void ToggleVisibility(bool show)
    {
        if (uiDocument != null)
        {
            uiDocument.rootVisualElement.style.display = show ? DisplayStyle.Flex : DisplayStyle.None;
        }
    }
}
