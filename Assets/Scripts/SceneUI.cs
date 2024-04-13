using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneUI : MonoBehaviour
{
    private int currentQuest;
    private int questAmount=2;
    void Start()
    {
        currentQuest = Random.Range(1, questAmount); // Change 2 to the total number of quests available
        SceneManager.LoadScene("ThroneRoom"); // Load the Throne Room scene when the game starts
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (SceneManager.GetActiveScene().name == "ThroneRoom")
            {
                if (currentQuest == 1)
                {
                    SceneManager.LoadScene("StorageRoom"); // Load the Storage Room scene for the "Get Meal" quest
                }
                // Add more conditions for other quests here
            }
            else if (SceneManager.GetActiveScene().name == "StorageRoom")
            {
                SceneManager.LoadScene("Kitchen"); // Load the Kitchen scene when the player interacts with the door in the Storage Room
            }
            else if (SceneManager.GetActiveScene().name == "Kitchen")
            {
                SceneManager.LoadScene("ThroneRoom"); // Load the Throne Room scene when the player interacts with the door in the Kitchen
            }
        }
    }
}
