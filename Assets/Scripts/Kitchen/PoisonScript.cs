using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonScript : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Add the poison to the player's inventory
            GameManager.instance.AddToInventory("Poison");
            // Deactivate the poison object
            gameObject.SetActive(false);
        }
    }
}
