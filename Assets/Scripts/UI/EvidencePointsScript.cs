using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvidencePointsScript : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Get the number of evidence points from the GameManager
            int points = Random.Range(1, 4); // Randomly generate 1-3 evidence points
            // Add the evidence points to the GameManager
            GameManager.instance.AddEvidencePoints(points);
            // Deactivate the evidence points object
            gameObject.SetActive(false);
        }
    }
}
