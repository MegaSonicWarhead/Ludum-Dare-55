using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayManager : MonoBehaviour
{
    public TMP_Text dayText;
    public TMP_Text questPointsText;
    public TMP_Text evidencePointsText;

    private int currentDay = 0;

    public DayManager dayManager;
    public QuestPointsManager questPointsManager;
    public EvidencePointsManager evidencePointsManager;

    private void Update()
    {
        UpdateDayText();
        UpdateQuestPointsText();
        UpdateEvidencePointsText();
    }

    public int GetCurrentDay()
    {
        return currentDay;
    }

    public void StartNextDay()
    {
        currentDay++;
        // Reset quests for the new day
        dayManager.StartNextDay();
    }

    private void UpdateDayText()
    {
        dayText.text = "Day: " + GetCurrentDay().ToString();
    }

    private void UpdateQuestPointsText()
    {
        questPointsText.text = "Quest Points: " + questPointsManager.GetQuestPoints().ToString();
    }

    private void UpdateEvidencePointsText()
    {
        evidencePointsText.text = "Evidence Points: " + evidencePointsManager.GetEvidencePoints().ToString();
    }
}
