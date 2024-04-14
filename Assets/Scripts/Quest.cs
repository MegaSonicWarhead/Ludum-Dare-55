using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public string questName;
    public string questDescription;
    public int questPointsReward;
    public int minEvidencePointsReward;
    public int maxEvidencePointsReward;
    public int assassinationMethod;

    private bool completed = false;

    public void SetQuest(string name, string description, int questPointsReward, int assassinationMethod)
    {
        this.questName = name;
        this.questDescription = description;
        this.questPointsReward = questPointsReward;
        this.assassinationMethod = assassinationMethod;
    }

    public void ResetQuest()
    {
        completed = false;
    }

    public void CompleteQuest()
    {
        completed = true;
    }

    public bool IsComplete()
    {
        return completed;
    }
}
