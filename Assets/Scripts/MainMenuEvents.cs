using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuEvents : MonoBehaviour
{
    private UIDocument _document;
    private Button _button;

    private List<Button> _menuButtons = new List<Button>();
    private AudioSource _audioSource;

    private bool isVisible = false;
    private bool isPaused = false;

    // Reference to the BoatController script
    private BoatController _boatController;

    private void Start()
    {
        // Ensure UI is hidden at start
        ToggleVisibility(false);
    }

    private void Awake()
    {
        // Ensure UI is hidden at start
        ToggleVisibility(false);

        _audioSource = GetComponent<AudioSource>();
        _document = GetComponent<UIDocument>();

        _button = _document.rootVisualElement.Q("StartGameButton") as Button;
        _button.RegisterCallback<ClickEvent>(onPlayGameClick);

        _menuButtons = _document.rootVisualElement.Query<Button>().ToList();

        foreach (var button in _menuButtons)
        {
            button.RegisterCallback<ClickEvent>(onAllButtonsClick);
        }

        // Locate the "Wood_BoatV1" child and get the BoatController component
        GameObject woodBoat = GameObject.Find("Wood_BoatV1");
        if (woodBoat != null)
        {
            _boatController = woodBoat.GetComponent<BoatController>();
        }
    }

    private void TogglePause()
    {
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    private void ResumeGame()
    {
        Time.timeScale = 1;
        isPaused = false;

        // Reactivate BoatController script
        if (_boatController != null)
        {
            _boatController.enabled = true;
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        isPaused = true;

        // Deactivate BoatController script
        if (_boatController != null)
        {
            _boatController.enabled = false;
        }
    }

    private void Update()
    {
        // Check if the ESC key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isVisible = !isVisible;
            ToggleVisibility(isVisible);

            // Only pause when showing the document
            if (isVisible)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }

    private void OnDisable()
    {
        _button.UnregisterCallback<ClickEvent>(onPlayGameClick);
        foreach (var button in _menuButtons)
        {
            button.UnregisterCallback<ClickEvent>(onAllButtonsClick);
        }
    }

    private void onPlayGameClick(ClickEvent evt)
    {
        Debug.Log("You pressed the Start Button");
        isVisible = !isVisible;
        ToggleVisibility(isVisible);

        // Only pause if the document is being shown
        if (isVisible)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }

    private void onAllButtonsClick(ClickEvent evt)
    {
        _audioSource.Play();
    }

    private void ToggleVisibility(bool show)
    {
        if (_document != null)
        {
            _document.rootVisualElement.style.display = show ? DisplayStyle.Flex : DisplayStyle.None;
        }
    }
}
