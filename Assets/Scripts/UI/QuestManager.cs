using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance; // Singleton instance of the QuestManager

    public List<string> quests; // List of available quests
    public int questsPerDay = 2; // Number of quests the king gives per day

    private List<string> currentQuests; // List of current quests for the day

    void Awake()
    {
        // Ensure there is only one instance of the QuestManager
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Initialize the list of current quests
        currentQuests = new List<string>();
        GenerateQuests();
    }

    public void GenerateQuests()
    {
        // Generate new random quests for the day
        currentQuests.Clear();
        for (int i = 0; i < questsPerDay; i++)
        {
            string quest = quests[Random.Range(0, quests.Count)];
            currentQuests.Add(quest);
        }
        Debug.Log("New quests generated for the day: " + string.Join(", ", currentQuests.ToArray()));
    }

    public void CompleteQuest(string quest)
    {
        // Remove the completed quest from the list of current quests
        currentQuests.Remove(quest);
        Debug.Log("Quest completed: " + quest);
    }

    public List<string> GetCurrentQuests()
    {
        // Get the list of current quests
        return currentQuests;
    }
}
