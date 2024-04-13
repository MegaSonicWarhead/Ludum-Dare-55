using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditSceneScript : MonoBehaviour
{
    public TextMeshProUGUI deathInfoText; // Text to display how the king died and how many days it took
    public TextMeshProUGUI evidencePointsText; // Text to display the evidence points earned
    public GameObject retryButton; // Button to retry the game
    public GameObject mainMenuButton; // Button to go to the main menu
    public GameObject exitButton; // Button to exit the game

    void Start()
    {
        // Display the death information
        deathInfoText.text = "The king died by [Death Method].\nIt took [Number of Days] days.";
        // Display the evidence points earned
        evidencePointsText.text = "Evidence Points: [Number of Points]";
    }

    public void RetryGame()
    {
        // Reload the current scene to retry the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu()
    {
        // Load the main menu scene
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitGame()
    {
        // Exit the game
        Application.Quit();
    }
}
