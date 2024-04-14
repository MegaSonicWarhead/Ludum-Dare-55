using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenInteract : MonoBehaviour
{
    private PlayerManager playerManager;

    private void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            playerManager.InteractWithKitchen();
            //
        }
    }
}
