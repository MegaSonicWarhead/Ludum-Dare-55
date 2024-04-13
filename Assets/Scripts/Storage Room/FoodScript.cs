using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodScript : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Add the food to the player's inventory
            GameManager.instance.AddToInventory("Food");
            // Deactivate the food object
            gameObject.SetActive(false);
        }
    }
}
