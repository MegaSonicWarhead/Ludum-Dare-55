using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneUI : MonoBehaviour
{
    private int currentQuestPoints;
    private int totalQuestPoints;
    private int daysPassed;
    private int currentQuestAmount = 2; // Number of quests given each day
    private int questPointsNeeded = 10; // Number of quest points needed to kill the king
    private bool isKingAlive = true;

    void Start()
    {
        currentQuestPoints = 0;
        totalQuestPoints = 0;
        daysPassed = 0;
        LoadNextDay();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (SceneManager.GetActiveScene().name == "ThroneRoom" && totalQuestPoints < questPointsNeeded)
            {
                LoadNextDay();
            }
        }
    }

    public void LoadNextDay()
    {
        currentQuestPoints = 0;
        currentQuestAmount = 2;
        daysPassed++;

        if (!isKingAlive)
        {
            SceneManager.LoadScene("CreditScene");
        }
        else
        {
            // Generate and display new quests for the day
            // You can implement this part based on your quest generation system
        }
    }

    public void CompleteQuest(int questPoints)
    {
        currentQuestPoints += questPoints;
        totalQuestPoints += questPoints;

        if (currentQuestPoints >= questPointsNeeded)
        {
            isKingAlive = false;
        }

        currentQuestAmount--;

        if (currentQuestAmount <= 0)
        {
            // Display message to interact with the clock to begin the next day
        }
    }
}
