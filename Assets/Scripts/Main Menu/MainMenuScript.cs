using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public GameObject door; // The door object to transition to other scenes

    void Update()
    {
        // Check for player input to start the game
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartGame();
        }

        // Check for player input to interact with the door
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Transition to the selected scene when the player interacts with the door
            door.GetComponent<DoorScript>().ChangeScene("ThroneRoom");
        }
    }

    void StartGame()
    {
        // Transition to the Throne Room scene when the game starts
        SceneManager.LoadScene("ThroneRoom");
    }
}
