using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenScript : MonoBehaviour
{
    public GameObject plate; // The plate object in the kitchen
    public GameObject foodPrefab; // The food prefab to instantiate on the plate
    public GameObject poison; // The poison object to add to the meal
    public GameObject door; // The door object to transition to other scenes

    private bool hasPlate = false; // Flag to check if the player has placed the food on the plate
    private bool hasPoison = false; // Flag to check if the player has added poison to the meal

    void Start()
    {
        // Plate is not placed initially
        plate.SetActive(false);
        hasPlate = false;
        hasPoison = false;
    }

    void Update()
    {
        // Check for player input to place the food on the plate
        if (Input.GetKeyDown(KeyCode.F) && GameManager.instance.CheckInventory("Food") && !hasPlate)
        {
            PlaceFoodOnPlate();
        }

        // Check for player input to add poison to the meal
        if (Input.GetKeyDown(KeyCode.P) && GameManager.instance.GetQuestPoints() >= 4 && !hasPoison)
        {
            AddPoisonToMeal();
        }

        // Check for player input to interact with the door
        if (Input.GetKeyDown(KeyCode.E) && hasPlate)
        {
            // Transition to the Throne Room scene when the player interacts with the door
            door.GetComponent<DoorScript>().ChangeScene("ThroneRoom");
        }
    }

    void PlaceFoodOnPlate()
    {
        // Instantiate the food on the plate
        Instantiate(foodPrefab, plate.transform.position, Quaternion.identity);
        // Set the plate active in the scene
        plate.SetActive(true);
        // Remove the food from the player's inventory
        GameManager.instance.RemoveFromInventory("Food");
        // Set the flag to true to indicate that the player has placed the food on the plate
        hasPlate = true;
    }

    void AddPoisonToMeal()
    {
        // Set the poison active on the plate
        poison.SetActive(true);
        // Remove quest points for using the poison
        GameManager.instance.UseQuestPoints(4);
        // Set the flag to true to indicate that the player has added poison to the meal
        hasPoison = true;
    }
}
