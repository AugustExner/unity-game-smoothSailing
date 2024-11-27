using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSailing : MonoBehaviour
{
    // Define the Z position threshold
    public float targetXPosition = 42f;

    // Update is called once per frame
    void Update()
    {
        // Check if the object's Z position has crossed the target Z position
        if (transform.position.x >= targetXPosition)
        {
            // Trigger the scene change
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
