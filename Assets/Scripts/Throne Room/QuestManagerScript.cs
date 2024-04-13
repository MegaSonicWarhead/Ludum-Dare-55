using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManagerScript : MonoBehaviour
{
    private int currentQuestPoints;
    private int totalQuestPoints;
    public int questPointsNeeded = 10; // Number of quest points needed to kill the king

    void Start()
    {
        currentQuestPoints = 0;
        totalQuestPoints = 0;
    }

    public void CompleteQuest(int questPoints)
    {
        currentQuestPoints += questPoints;
        totalQuestPoints += questPoints;

        // Check if king should be killed
        if (totalQuestPoints >= questPointsNeeded)
        {
            KillKing();
        }
    }

    private void KillKing()
    {
        // Handle king's death (e.g., show death animation, end game)
        Debug.Log("King is dead!");
    }
}
