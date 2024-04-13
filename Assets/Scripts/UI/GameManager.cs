using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // Singleton instance of the GameManager

    private Dictionary<string, int> inventory; // Player's inventory
    private int questPoints; // Player's quest points
    private int totalEvidencePoints; // Total evidence points earned

    void Awake()
    {
        // Ensure there is only one instance of the GameManager
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Initialize the inventory, quest points, and total evidence points
        inventory = new Dictionary<string, int>();
        questPoints = 0;
        totalEvidencePoints = 0;
    }

    public void AddToInventory(string item)
    {
        // Add the item to the inventory or increment its count if it already exists
        if (inventory.ContainsKey(item))
        {
            inventory[item]++;
        }
        else
        {
            inventory.Add(item, 1);
        }
    }

    public void RemoveFromInventory(string item)
    {
        // Remove the item from the inventory or decrement its count if it exists
        if (inventory.ContainsKey(item))
        {
            if (inventory[item] > 1)
            {
                inventory[item]--;
            }
            else
            {
                inventory.Remove(item);
            }
        }
    }

    public bool CheckInventory(string item)
    {
        // Check if the item exists in the inventory
        return inventory.ContainsKey(item);
    }

    public int GetQuestPoints()
    {
        // Get the player's quest points
        return questPoints;
    }

    public void UseQuestPoints(int points)
    {
        // Use quest points
        questPoints -= points;
    }

    public void AddQuestPoints(int points)
    {
        // Add quest points
        questPoints += points;
    }

    public void AddEvidencePoints(int points)
    {
        // Add evidence points
        totalEvidencePoints += points;
    }

    public int GetTotalEvidencePoints()
    {
        // Get the total evidence points earned
        return totalEvidencePoints;
    }
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
