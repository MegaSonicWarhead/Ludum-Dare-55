using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public Quest[] quests;
    private int currentQuestIndex = 0;

    void Start()
    {
        // Initialize your quests
        quests = new Quest[2]; // Assuming you have 2 quests to start with
        SetQuest(0, "Prepare My Meal", "a. Get empty Plate \n b. Prepare Meal \n c. Deliver Meal", 2, 1);
        SetQuest(1, "Get Me A Drink", "a. Get empty glass \n b. Pour wine \n c. Deliver wine", 1, 1);
    }

    public void SetCurrentQuestIndex(int index)
    {
        currentQuestIndex = index;
    }

    public Quest GetCurrentQuest()
    {
        if (currentQuestIndex < 0 || currentQuestIndex >= quests.Length)
        {
            Debug.LogError("Invalid current quest index: " + currentQuestIndex);
            return null;
        }

        return quests[currentQuestIndex];
    }

    public void CompleteCurrentQuest()
    {
        if (currentQuestIndex < 0 || currentQuestIndex >= quests.Length)
        {
            Debug.LogError("Invalid current quest index: " + currentQuestIndex);
            return;
        }

        quests[currentQuestIndex].CompleteQuest();
    }

    public bool AreAllQuestsComplete()
    {
        foreach (Quest quest in quests)
        {
            if (!quest.IsComplete())
            {
                return false;
            }
        }
        return true;
    }

    public void ResetQuests()
    {
        foreach (Quest quest in quests)
        {
            quest.ResetQuest();
        }
    }

    // Add a method to set the properties of a quest
    public void SetQuest(int index, string name, string description, int reward, int successChance)
    {
        if (index < 0 || index >= quests.Length)
        {
            Debug.LogError("Invalid quest index: " + index);
            return;
        }

        if (quests[index] == null)
        {
            quests[index] = new Quest();
        }

        quests[index].SetQuest(name, description, reward, successChance);
    }
}
