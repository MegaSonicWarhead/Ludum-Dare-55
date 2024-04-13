using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingScript : MonoBehaviour
{
    private bool isAlive = true; // Flag to track if the king is alive

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Check if the player has delivered the meal
            if (GameManager.instance.CheckInventory("Plate"))
            {
                // Determine if the king dies based on the assassination method used
                if (GameManager.instance.CheckInventory("Poison"))
                {
                    // Use the poison and check the chance of killing the king
                    if (Random.Range(0, 100) < 80) // 80% chance of killing the king
                    {
                        KillKing();
                    }
                }
                else
                {
                    // If no assassination method used, the king cannot be killed
                    Debug.Log("The king is not affected by the meal.");
                }
            }
            else
            {
                Debug.Log("The player needs to deliver the meal to the king.");
            }
        }
    }

    void KillKing()
    {
        // Set the king as dead
        isAlive = false;
        // Deactivate the king object
        gameObject.SetActive(false);
        // Transition to the credit scene
        GameManager.instance.ChangeScene("CreditScene");
    }
}
