using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{

    void OnEnable()
    {
        SceneManager.LoadScene(0);
    }
}
