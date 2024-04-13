using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingScript : MonoBehaviour
{
    public GameObject questCompletedText; // Quest completed text prefab
    private bool questCompleted;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && questCompleted)
        {
            // Give plate of food to the king
            Instantiate(questCompletedText, transform.position, Quaternion.identity);
            // Add quest points to the player's total
            FindObjectOfType<QuestManagerScript>().CompleteQuest(2); // 2 points for completing the "Get Meal" quest
            questCompleted = false; // Reset quest completed state
        }
    }

    public void QuestCompleted()
    {
        questCompleted = true;
    }
}
