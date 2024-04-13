using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageRoomScript : MonoBehaviour
{
    public GameObject foodItem; // Food item prefab
    public Transform playerInventory; // Player's inventory transform

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Instantiate(foodItem, playerInventory.position, Quaternion.identity, playerInventory);
            Destroy(gameObject);
        }
    }
}
