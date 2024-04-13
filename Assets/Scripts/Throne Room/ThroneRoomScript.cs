using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThroneRoomScript : MonoBehaviour
{
    public GameObject king; // The king object in the throne room
    public GameObject meal; // The meal object to deliver to the king
    public GameObject creditScene; // The credit scene object to transition to
    public GameObject door; // The door object to transition to other scenes

    private bool hasDeliveredMeal = false; // Flag to check if the player has delivered the meal

    void Start()
    {
        // King is alive initially
        king.SetActive(true);
        hasDeliveredMeal = false;
    }

    void Update()
    {
        // Check for player input to deliver the meal to the king
        if (Input.GetKeyDown(KeyCode.F) && GameManager.instance.CheckInventory("Plate") && !hasDeliveredMeal)
        {
            DeliverMealToKing();
        }

        // Check if the king is dead and transition to the credit scene
        if (!king.activeSelf)
        {
            TransitionToCreditScene();
        }

        // Check for player input to interact with the door
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Transition to the selected scene when the player interacts with the door
            door.GetComponent<DoorScript>().ChangeScene();
        }
    }

    void DeliverMealToKing()
    {
        // Set the meal inactive in the scene
        meal.SetActive(false);
        // Remove the plate from the player's inventory
        GameManager.instance.RemoveFromInventory("Plate");
        // Set the king inactive in the scene
        king.SetActive(false);
        // Set the flag to true to indicate that the player has delivered the meal
        hasDeliveredMeal = true;
    }

    void TransitionToCreditScene()
    {
        // Activate the credit scene object to transition to
        creditScene.SetActive(true);
    }
}
