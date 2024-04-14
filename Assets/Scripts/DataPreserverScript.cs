using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPreserverScript : MonoBehaviour
{
    public PlayerManager Player;

    public string inventoryObjectData;
    public int DaysData; 
    public int QuestPointsData; 
    public int EvidencePointsData;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);      //this game object will be preserved accross loads
    }

    public void GetData()
    {
        inventoryObjectData = PlayerManager.inventoryObject;
        DaysData = Player.Days;
        QuestPointsData = Player.QuestPoints;
        EvidencePointsData = Player.EvidencePoints;
    }
}
