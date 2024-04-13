using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public List<string> scenes; // List of scene names to choose from

    private int currentIndex = 0; // Index of the currently selected scene

    void Start()
    {
        // Initialize the index to 0
        currentIndex = 0;
    }

    void Update()
    {
        // Check for player input to cycle through the available scenes
        if (Input.GetKeyDown(KeyCode.E))
        {
            CycleScene();
        }

        // Check for player input to change to the selected scene
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ChangeScene();
        }
    }

    void CycleScene()
    {
        // Increment the index
        currentIndex = (currentIndex + 1) % scenes.Count;
    }

    public void ChangeScene()
    {
        // Change to the selected scene
        UnityEngine.SceneManagement.SceneManager.LoadScene(scenes[currentIndex]);
    }

    public void ChangeScene(string sceneName)
    {
        // Change to the specified scene
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
