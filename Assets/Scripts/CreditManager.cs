using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CreditManager : MonoBehaviour
{
    public TMP_Text DaysTxt;
    public TMP_Text MessgaeOneTxt;
    public TMP_Text MessageTwoTxt;
    public TMP_Text EPTxt;

    // Start is called before the first frame update
    void Start()
    {
        // Find the GameObject with the tag "Player" (assuming "Player" is the tag of the GameObject with the PlayerManager script)
        GameObject playerObject = GameObject.FindGameObjectWithTag("#player");


        if (playerObject != null)
        {
            // Get the PlayerManager component attached to the "Player" GameObject
            PlayerManager playerManager = playerObject.GetComponent<PlayerManager>();

            if (playerManager != null)
            {
                // Access variables from the PlayerManager script
                int days = playerManager.Days;
                int daysHalved = days / 2;
                int evidencePoints = playerManager.EvidencePoints;

                DaysTxt.text = "Days Taken to kill the King: " + daysHalved.ToString();
                EPTxt.text = "Total Evidence Points: " + evidencePoints.ToString();

                if (evidencePoints > 10)
                {
                    MessgaeOneTxt.text = "YOU WERE EXECUTED";
                    MessageTwoTxt.text = "You left too much evidence behind.";
                }
                else
                {
                    MessgaeOneTxt.text = "YOU WERE SUCCESSFULL";
                    MessageTwoTxt.text = "You were careful not to leave too much evidence.";
                }

                
            }
            else
            {
                Debug.LogWarning("PlayerManager component not found on the 'Player' GameObject.");
            }
        }
        else
        {
            Debug.LogWarning("GameObject with tag 'Player' not found in the scene.");
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
