using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerQuestScript : MonoBehaviour
{
    private QuestManagerScript questManager;

    void Start()
    {
        questManager = FindObjectOfType<QuestManagerScript>();
    }

    void Update()
    {
        // Check for interactions with objects (e.g., picking up items)
        if (Input.GetKeyDown(KeyCode.E))
        {
            InteractWithKing();
        }
    }

    private void InteractWithKing()
    {
        // Handle interaction with the king (e.g., completing quests)
        questManager.CompleteQuest(2); // Example: Completing "Get Meal" quest gives 2 points
    }
}
