using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestScript : MonoBehaviour
{
    public int questPoints = 1; // Points awarded for completing this quest

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Quest completion logic (e.g., showing UI, updating SceneUI)
            FindObjectOfType<SceneUI>().CompleteQuest(questPoints);
            Destroy(gameObject);
        }
    }
}
