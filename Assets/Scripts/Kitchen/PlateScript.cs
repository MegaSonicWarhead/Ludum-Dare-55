using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateScript : MonoBehaviour
{
    public GameObject foodOnPlate; // Food on plate prefab
    private bool hasFood;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && !hasFood)
        {
            Instantiate(foodOnPlate, transform.position, Quaternion.identity, transform);
            hasFood = true;
        }
    }

    public void TakePlate()
    {
        if (hasFood)
        {
            // Player picks up the plate with food
            // You can implement this part based on your player's inventory system
            // For example, you can add the plate with food to the player's inventory
            // and set hasFood to false to indicate that the plate is no longer on the table
        }
    }
}
