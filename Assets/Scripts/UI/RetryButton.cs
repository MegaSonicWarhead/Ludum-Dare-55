using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryButton : MonoBehaviour
{
    public void OnRetryButtonClicked()
    {
        // Reload the current scene to retry the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
