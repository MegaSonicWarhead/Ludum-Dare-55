using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestObject : MonoBehaviour
{
    public string questName; // Name of the quest associated with this object

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Check if the quest is in the player's current quests
            List<string> currentQuests = QuestManager.instance.GetCurrentQuests();
            if (currentQuests.Contains(questName))
            {
                // Complete the quest
                QuestManager.instance.CompleteQuest(questName);
                Debug.Log("Quest completed: " + questName);
                // Deactivate the quest object
                gameObject.SetActive(false);
            }
            else
            {
                Debug.Log("Quest not available: " + questName);
            }
        }
    }
}
