using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void GiveObjectToKing()
    {
        if (PlayerManager.inventoryObject != null)
        {
            if (PlayerManager.inventoryObject == "Lethal Poison" || PlayerManager.inventoryObject == "Basic Poison")
            {
                // Find the GameObject with the KingScript component
                GameObject kingObject = GameObject.Find("YourKingGameObjectName"); // Replace "YourKingGameObjectName" with the actual name of your king GameObject

                // Access the KingScript component and call CompleteQuest
                if (kingObject != null)
                {
                    KingScript kingScript = kingObject.GetComponent<KingScript>();
                    if (kingScript != null)
                    {
                        kingScript.CompleteQuest(PlayerManager.inventoryObject);
                        PlayerManager.inventoryObject = null; // Reset the inventoryObject after completing the quest
                    }
                }
            }
        }
    }
}
