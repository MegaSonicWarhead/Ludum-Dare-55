using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayManager : MonoBehaviour
{
    private int currentDay = 0;
    public QuestManager questManager;
    public GameObject someObject; // Example GameObject that needs initialization


    public int GetCurrentDay()
    {
        return currentDay;
    }

    void Start()
    {
        // Check if the object is not null before using it
        if (someObject != null)
        {
            // Example: Accessing the current quest's name
            string currentQuestName = questManager.GetCurrentQuest().questName;
            Debug.Log("Current Quest Name: " + currentQuestName);
        }
        else
        {
            Debug.LogError("someObject is null!");
        }
        
    }

    public void StartNextDay()
    {
        currentDay++;
        // Reset quests for the new day
        questManager.ResetQuests();
    }
}
