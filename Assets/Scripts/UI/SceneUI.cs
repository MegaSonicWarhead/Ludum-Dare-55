using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneUI : MonoBehaviour
{
    private int currentQuestPoints; // Current quest points earned for the day
    private int totalQuestPoints; // Total quest points accumulated
    private int daysPassed; // Number of days passed in the game
    private int currentQuestAmount = 2; // Number of quests given each day
    private int questPointsNeeded = 10; // Number of quest points needed to kill the king
    private bool isKingAlive = true; // Flag to track if the king is alive

    void Start()
    {
        currentQuestPoints = 0;
        totalQuestPoints = 0;
        daysPassed = 0;
        LoadNextDay(); // Start the game by loading the first day
    }

    void Update()
    {
        // Check for interaction to load the next day
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (SceneManager.GetActiveScene().name == "ThroneRoom" && totalQuestPoints < questPointsNeeded)
            {
                LoadNextDay();
            }
        }
    }

    // Method to load the next day
    public void LoadNextDay()
    {
        currentQuestPoints = 0;
        currentQuestAmount = 2; // Reset the number of quests for the day
        daysPassed++;

        if (!isKingAlive || totalQuestPoints >= questPointsNeeded)
        {
            SceneManager.LoadScene("CreditScene"); // Load the credit scene if the king is dead or quest points are enough to kill the king
        }
        else
        {
            // Generate and display new quests for the day
            // You can implement this part based on your quest generation system
        }
    }

    // Method to complete a quest and earn quest points
    public void CompleteQuest(int questPoints)
    {
        currentQuestPoints += questPoints;
        totalQuestPoints += questPoints;

        // Check if the king should be killed
        if (totalQuestPoints >= questPointsNeeded)
        {
            isKingAlive = false;
        }

        currentQuestAmount--;

        if (currentQuestAmount <= 0)
        {
            // Display a message to interact with the clock to begin the next day
            // You can implement this based on your UI system
        }
    }
}
