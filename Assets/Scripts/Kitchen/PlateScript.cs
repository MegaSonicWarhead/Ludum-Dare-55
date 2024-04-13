using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateScript : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Add the plate to the player's inventory
            GameManager.instance.AddToInventory("Plate");
            // Deactivate the plate object
            gameObject.SetActive(false);
        }
    }
}
