using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageRoom : MonoBehaviour
{
    public GameObject food; // The food object in the storage room
    public GameObject door; // The door object to transition to other scenes

    private bool hasFood = false; // Flag to check if the player has picked up the food

    void Start()
    {
        // Food is not picked up initially
        food.SetActive(true);
        hasFood = false;
    }

    void Update()
    {
        // Check for player input to pick up the food
        if (Input.GetKeyDown(KeyCode.E) && !hasFood)
        {
            PickUpFood();
        }

        // Check for player input to interact with the door
        if (Input.GetKeyDown(KeyCode.E) && hasFood)
        {
            // Transition to the Throne Room scene when the player interacts with the door
            door.GetComponent<DoorScript>().ChangeScene("ThroneRoom");
        }
    }

    void PickUpFood()
    {
        // Set food inactive in the scene
        food.SetActive(false);
        // Set the flag to true to indicate that the player has picked up the food
        hasFood = true;
        // Add the food to the player's inventory
        GameManager.instance.AddToInventory("Food");
    }
}
