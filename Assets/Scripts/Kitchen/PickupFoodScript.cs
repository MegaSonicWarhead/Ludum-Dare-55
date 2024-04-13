using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupFoodScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventoryScript playerInventory = other.GetComponent<PlayerInventoryScript>();
            if (playerInventory != null)
            {
                playerInventory.AddToInventory(gameObject, "Food"); // Assuming the objectType for food is "Food"
                gameObject.SetActive(false); // Deactivate the food item instead of destroying it
            }
        }
    }
}
