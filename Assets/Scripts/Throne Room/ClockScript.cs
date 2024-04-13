using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockScript : MonoBehaviour
{
    public GameObject king; // The king object in the throne room
    public GameObject door; // The door object to transition to other scenes

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Check if the king is dead
            if (!king.activeSelf)
            {
                // Transition to the main menu scene to start a new day
                door.GetComponent<DoorScript>().ChangeScene("MainMenu");
            }
            else
            {
                Debug.Log("The king is still alive. Complete the quests before starting a new day.");
            }
        }
    }
}
