using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestUIManager : MonoBehaviour
{
    public TextMeshProUGUI questText; // Text element to display the current quests

    void Start()
    {
        UpdateQuestUI();
    }

    public void UpdateQuestUI()
    {
        // Get the list of current quests from the QuestManager
        List<string> currentQuests = QuestManager.instance.GetCurrentQuests();

        // Update the quest text to display the current quests
        if (currentQuests.Count > 0)
        {
            questText.text = "Current Quests:\n";
            foreach (string quest in currentQuests)
            {
                questText.text += "- " + quest + "\n";
            }
        }
        else
        {
            questText.text = "No quests available.";
        }
    }
}
