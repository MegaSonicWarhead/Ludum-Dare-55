using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvidencePointsManager : MonoBehaviour
{
    public static EvidencePointsManager Instance;

    private int evidencePoints = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public int GetEvidencePoints()
    {
        return evidencePoints;
    }

    public void AddEvidencePoints(int points)
    {
        evidencePoints += points;
    }

    public void ResetEvidencePoints()
    {
        evidencePoints = 0;
    }
}
