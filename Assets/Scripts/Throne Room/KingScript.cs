using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingScript : MonoBehaviour
{
    public GameObject questCompletedText; // Quest completed text prefab
    private bool questCompleted;

    public void QuestCompleted()
    {
        questCompleted = true;
    }

    public void ReceiveItem(GameObject item, string objectType)
    {
        // Implement logic for receiving item from the player
        // For example, you can check the type of item and react accordingly
        Debug.Log("Received item: " + item.name + " of type: " + objectType);
        // Depending on your game, you might do something like this:
        // if (objectType == "Food") { ... }
        // else if (objectType == "Weapon") { ... }
        // etc.
    }

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
}
