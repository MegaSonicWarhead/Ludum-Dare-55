using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EvidencePointsDisplay : MonoBehaviour
{
    public TextMeshProUGUI evidencePointsText; // Text to display the total number of evidence points

    void Start()
    {
        // Display the total number of evidence points earned by the player
        evidencePointsText.text = "Evidence Points: " + GameManager.instance.GetTotalEvidencePoints().ToString();
    }
}
