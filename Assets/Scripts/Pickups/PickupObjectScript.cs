using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObjectScript : MonoBehaviour
{
    public string objectType; // Type of the object (e.g., "Food", "Key", "Weapon")

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventoryScript playerInventory = other.GetComponent<PlayerInventoryScript>();
            if (playerInventory != null)
            {
                playerInventory.AddToInventory(gameObject, objectType);
                gameObject.SetActive(false); // Deactivate the object instead of destroying it
            }
        }
    }
}