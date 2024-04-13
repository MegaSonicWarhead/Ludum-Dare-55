using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionPickupsScript : MonoBehaviour
{
    public PlayerInventoryScript playerInventory;
    public GameObject questObject; // Reference to the specific object needed for the quest

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            InteractWithQuestObject();
        }
    }

    private void InteractWithQuestObject()
    {
        if (questObject != null)
        {
            playerInventory.AddToInventory(questObject, "QuestItem"); // Assuming the objectType for quest items is "QuestItem"
            questObject.SetActive(false); // Deactivate the quest object instead of destroying it
        }
    }
}
