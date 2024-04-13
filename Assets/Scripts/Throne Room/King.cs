using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : MonoBehaviour
{
    public List<string> availableQuests; // List of available quests that the king can give
    public int questsPerDay = 2; // Number of quests the king gives per day

    private QuestManager questManager; // Reference to the QuestManager

    void Start()
    {
        questManager = QuestManager.instance;
        GiveQuests();
    }

    void GiveQuests()
    {
        // Generate new quests for the day
        questManager.GenerateQuests();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Check if the player has completed the quests
            List<string> currentQuests = questManager.GetCurrentQuests();
            if (currentQuests.Count == 0)
            {
                // Give new quests to the player
                GiveQuests();
            }
            else
            {
                Debug.Log("Complete the quests first!");
            }
        }
    }
}
