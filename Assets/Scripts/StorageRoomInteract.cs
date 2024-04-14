using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageRoomInteract : MonoBehaviour
{
    private PlayerManager playerManager;

    private void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("FoodCrate"))
        {
            playerManager.InteractWithStorageRoom();
        }
    }
}
