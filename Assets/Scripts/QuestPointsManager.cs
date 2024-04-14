using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestPointsManager : MonoBehaviour
{
    public static QuestPointsManager Instance;

    private int questPoints = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public int GetQuestPoints()
    {
        return questPoints;
    }

    public void AddQuestPoints(int points)
    {
        questPoints += points;
    }

    public void ResetQuestPoints()
    {
        questPoints = 0;
    }
}
