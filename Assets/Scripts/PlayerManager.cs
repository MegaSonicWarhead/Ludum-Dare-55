using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerManager : MonoBehaviour
{
    bool recievingQuest = false;
    bool makingDecision = false;

    int activeQuest;        //carry over
    string[] questName = new string[2] { "Prepare My Meal", "Get Me A Drink" };
    int[] questReward = new int[2] { 2, 1 };
    string[] questDescription = new string[2]
    { " a. Get empty Plate \n b. Prepare Meal \n c. Deliver Meal ",
      " a. Get empty glass \n b. Pour wine \n c. Deliver wine " };

    public UnityEngine.UI.Image questPanel;
    public TMP_Text questTxt;

    public int assasinationIndex;   //carry over
    string[] assasinationMethod = new string[2] { "Lethal Poison", "Basic Poison" };   //Method Name
    int[,] assasinationStats = new int[4, 2] {   { 8,    4 },        //Method cost in QP (0)
                                                 { 80,   40 },      //Methods success chance (1)
                                                 { 1,    1},        //Method Evidence Points Min (2)
                                                 { 3,    3}  };     //Method Evidence Points Max (3)
    private int dailyQuests;

    public string inventoryObject;  //needs to be carried over to next scene
    public int Days;    //needs to be carried over to next scene
    public int QuestPoints; //needs to be carried over to next scene
    public int EvidencePoints;  //needs to be carried over to next scene
    bool questCompleted = false; // Flag to track if the current quest is completed

    public TMP_Text DaysTxt;
    public TMP_Text QPTxt;
    public TMP_Text EPTxt;

    private bool isKingAlive = true;

    public UnityEngine.UI.Image EmptyPlateImg;
    public UnityEngine.UI.Image FullPlateImg;
    public UnityEngine.UI.Image PoisonedPlateImg;
    public UnityEngine.UI.Image EmptyGlassImg;
    public UnityEngine.UI.Image FullGlassImg;
    public UnityEngine.UI.Image PoisonedGlassImg;

    public TMP_Text actionLabel;

    public UnityEngine.UI.Image DialoguePanel;
    public TMP_Text InteractionNameTxt;
    public TMP_Text DialogueTxt;

    bool eBeingPressed = false;
    bool OneBeingPressed;
    bool TwoBeingPressed;
    bool colliding = false;
    private Collider2D collider;        //collider that player collides with

    System.Random random = new System.Random();

    void Start()
    {
    }

    void OnEnable()
    {
        if (Days == 0)
        {
            questPanel.enabled = true;
            questTxt.enabled = true;
            QuestAssignment();
        }

        activeQuest = random.Next(0, 2);

        inventoryObject = PlayerPrefs.GetString("InventoryObject");

        inventoryObject = null;

        actionLabel.enabled = false;

        DialoguePanel.enabled = false;
        InteractionNameTxt.enabled = false;
        DialogueTxt.enabled = false;

        questPanel.enabled = false;
        questTxt.enabled = false;

        EmptyPlateImg.enabled = false;
        FullPlateImg.enabled = false;
        PoisonedPlateImg.enabled = false;
        EmptyGlassImg.enabled = false;
        FullGlassImg.enabled = false;
        PoisonedGlassImg.enabled = false;
        FullPlateImg.enabled = false;

        if (inventoryObject != null)    //if player starts scene with somehting in inventory, that obj is visible in inventory
        {
            AddToInventory(inventoryObject);
        }
        Debug.Log("PlayerManager Script is active");

        activeQuest = random.Next(0, 2);

        inventoryObject = PlayerPrefs.GetString("InventoryObject");
    }

    void QuestAssignment()
    {
        if (recievingQuest)
        {
            activeQuest = Random.Range(0, questName.Length);
            questTxt.text = questName[activeQuest] + "\n" + questDescription[activeQuest];
            questPanel.enabled = true;
            questTxt.enabled = true;
        }
    }

    int[] GetRandomQuestIndexes()
    {
        List<int> indexes = new List<int> { 0, 1, 2, 3, 4 }; // Assuming you have 5 quests
        indexes.Shuffle(); // Shuffle the list to get random indexes
        return indexes.GetRange(0, 2).ToArray(); // Get the first 2 indexes after shuffling
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            eBeingPressed = true;

            if (colliding)
            {
                Interact();
            }
        }
        else { OneBeingPressed = false; }

        if (Input.GetKeyDown(KeyCode.Alpha1)) { OneBeingPressed = true; }
        else { OneBeingPressed = false; }

        if (Input.GetKeyDown(KeyCode.Alpha2)) { TwoBeingPressed = true; }
        else { TwoBeingPressed = false; }

        if (OneBeingPressed && recievingQuest)
        {
            HideDialoguePanel();

            dailyQuests++;

            questTxt.text = questName[activeQuest] + "\n" + questDescription[activeQuest];
            questPanel.enabled = true;
            questTxt.enabled = true;

            recievingQuest = false;
        }

        if (OneBeingPressed && makingDecision)
        {
            assasinationIndex = 0; //Lethal Poison
            HideDialoguePanel();
            makingDecision = false;
        }
        if (TwoBeingPressed && makingDecision)
        {
            assasinationIndex = 1; //Basic Poison
            HideDialoguePanel();
            makingDecision = false;
        }
        // Check if the player has completed the quest and is in the Throne Room
        if (questCompleted && colliding && collider.gameObject.tag == "#throne")
        {
            // Award quest points and reset the quest completion flag
            QuestPoints += questReward[activeQuest];
            questCompleted = false;

            // Hide the quest panel and text
            questPanel.enabled = false;
            questTxt.enabled = false;
        }

        // Check if player pressed E on the book in the throne room to start the next day
        if (Input.GetKeyDown(KeyCode.E) && colliding && collider.gameObject.tag == "#book")
        {
            // Start the next day
            StartNextDay();
        }

        UpdateUI();
    }
    void StartNextDay()
    {
        // Increase the day count
        Days++;

        // Reset daily quest count
        dailyQuests = 0;

        // Reset assassination selection
        assasinationIndex = -1;

        // Get two random quests for the day
        activeQuest = random.Next(0, 2);

        // Update UI
        UpdateUI();
    }

    IEnumerator DisableActionLabel(float delay)
    {
        yield return new WaitForSeconds(delay);
        actionLabel.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D collider2d)
    {
        Debug.Log("Player has collided");
       colliding = true;
        collider = collider2d;

        if (collider.gameObject.tag == "#poisonCrate")     //checks which object the player is colliding with
        {
            //A label with the interaction description must pop up
            actionLabel.text = "Press E to add poison";
            actionLabel.enabled = true;
        }
        else if(collider.gameObject.tag == "#throne")     //checks which object the player is colliding with
        {
            actionLabel.text = "Press E to talk to the King";
            actionLabel.enabled = true;
        }
        else if (collider.gameObject.tag == "#foodCrate")     //checks which object the player is colliding with
        {
            actionLabel.text = "Press E to prepare meal";
            actionLabel.enabled = true;
        }


        else if (collider.gameObject.tag == "#wine")     //checks which object the player is colliding with
        {
            actionLabel.text = "Press E to fill glass";
            actionLabel.enabled = true;
        }

        else if (collider.gameObject.tag == "#glass")     //checks which object the player is colliding with
        {
            actionLabel.text = "Press E to pick up Glass";
            actionLabel.enabled = true;
        }

        else if (collider.gameObject.tag == "#plate")     //checks which object the player is colliding with
        {
            actionLabel.text = "Press E to get plate";
            actionLabel.enabled = true;
        }

        else if (collider.gameObject.tag == "#storageDoor")     //checks which object the player is colliding with
        {
            actionLabel.text = "Press E to go to Storage";
            actionLabel.enabled = true;
        }

       else if (collider.gameObject.tag == "#kitchenDoor")     //checks which object the player is colliding with
        {
            actionLabel.text = "Press E to go to Kitchen";
            actionLabel.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider2d)
    {
        colliding = false;

        HideDialoguePanel();
        makingDecision = false;
        recievingQuest = false;

        //label with description must dissapear
       //actionLabel.enabled = false;
    }
    void Interact()
    {
        if (collider.gameObject.tag == "#foodCrate")     //checks which object the player is colliding with
        {
            if (inventoryObject == "EmptyPlate")    //checks if player has Empty plate
            {
                
                //replace Empty Plate with FullPlate
                RemoveFromInventory(inventoryObject);
                AddToInventory("FullPlate");
                PlayerPrefs.SetString("inventoryObject", "FullPlate");
            }
            else
            {
                Debug.Log("Ädd empty plate fisr");
            }
        }

        // Other interactions...

        if (collider.gameObject.tag == "#throneRoomDoor")     //checks which object the player is colliding with
        {
            // Save the current position
            Vector3 currentPosition = transform.position;

            // Change scene to throne room
            SceneManager.LoadScene("Throne Room", LoadSceneMode.Single);

            // Set the player's position in the new scene
            StartCoroutine(SetPlayerPositionAfterDelay(currentPosition));

            // Disable action label after 2 seconds
            StartCoroutine(DisableActionLabel(2f));
        }

        if (collider.gameObject.tag == "#poisonCrate")     //checks which object the player is colliding with
        {
            AssasinationSelection("Poison"); //choose between 2 poisons
            QuestPoints = QuestPoints - assasinationStats[assasinationIndex, 0];       //subtract QP
            if (inventoryObject == "FullPlate")    //checks if player has full plate
            {
                //replace full Plate with PoisonedFullPlate
                RemoveFromInventory(inventoryObject);
                AddToInventory("PoisonedFullPlate");
                PlayerPrefs.SetString("inventoryObject", "PoisonedFullPlate");
            }
            if (inventoryObject == "FullGlass")    //checks if player has full glass
            {
                //replace Empty Plate with PoisonedFullGlass
                RemoveFromInventory(inventoryObject);
                AddToInventory("PoisonedFullGlass");
                PlayerPrefs.SetString("inventoryObject", "PoisonedFullGlass");
            }
        }

        if (collider.gameObject.tag == "#throne")
        {
            //interact with king, by giving recieving quest or ending quest etc
            //subtract certain objects from inventory
            if (dailyQuests < 2)
            {
                RecieveQuest();
            }
            if (activeQuest == 1)
            {
                if (inventoryObject == "FullPlate")
                {
                    RemoveFromInventory("FullPlate");
                    QuestPoints = QuestPoints + questReward[activeQuest];
                    activeQuest = random.Next(0, 2);
                }
                if (inventoryObject == "PoisonedFullPlate")
                {
                    RemoveFromInventory("PoisonedFullPlate");
                    QuestPoints = QuestPoints + questReward[activeQuest];
                    CalculateAssassination();
                    activeQuest = random.Next(0, 2);
                }
            }
            if (activeQuest == 1)
            {
                if (inventoryObject == "FullGlass")
                {
                    RemoveFromInventory("FullGlass");
                    QuestPoints = QuestPoints + questReward[activeQuest];
                    activeQuest = random.Next(0, 2);
                }
                if (inventoryObject == "PoisonedFullGlass")
                {
                    RemoveFromInventory("PoisonedFullGlass");
                    QuestPoints = QuestPoints + questReward[activeQuest];
                    CalculateAssassination();
                    activeQuest = random.Next(0, 2);
                }
            }
            if (collider.gameObject.tag == "#throne")
            {
                if (activeQuest == 0 && inventoryObject == "FullPlate")
                {
                    RemoveFromInventory("FullPlate");
                    questCompleted = true; // Set quest as completed
                }
                else if (activeQuest == 1 && inventoryObject == "FullGlass")
                {
                    RemoveFromInventory("FullGlass");
                    questCompleted = true; // Set quest as completed
                }
            }
        }
        if (collider.gameObject.tag == "#wine")     //checks which object the player is colliding with
        {
            if (inventoryObject == "EmptyGlass")    //checks if player has EmptyGlass
            {
                //replace Empty Plate with FullGlass
                RemoveFromInventory(inventoryObject);
                AddToInventory("FullGlass");
                PlayerPrefs.SetString("inventoryObject", "FullGlass");
            }
        }
        if (collider.gameObject.tag == "#glass")     //checks which object the player is colliding with
        {
            RemoveFromInventory(inventoryObject);
            AddToInventory("EmptyGlass");
            //Add Empty glass to inventory
            PlayerPrefs.SetString("inventoryObject", "EmptyGlass");
        }
        if (collider.gameObject.tag == "#plate")     //checks which object the player is colliding with
        {
            if (inventoryObject != "EmptyPlate")
            {
                // The plate is not in the inventory, so add it
                AddToInventory("EmptyPlate");
                // Save the plate to PlayerPrefs
                PlayerPrefs.SetString("inventoryObject", "EmptyPlate");
                // Disable the plate object in the scene
                collider.gameObject.SetActive(false);
                Debug.Log("Plate picked up!"); ;
            }
        }

        if (collider.gameObject.tag == "#storageDoor")     //checks which object the player is colliding with
        {

                    
                // Save the current position
                 Vector3 currentPosition = transform.position;

                // Change scene to storage room
                SceneManager.LoadScene("Storage Room");

                // Set the player's position in the new scene
                StartCoroutine(SetPlayerPositionAfterDelay(currentPosition));

                // Disable action label after 2 seconds
                StartCoroutine(DisableActionLabel(2f));
            
           
        }

        if (collider.gameObject.tag == "#kitchenDoor")     //checks which object the player is colliding with
        {
            //change scene to kitchen
            SceneManager.LoadScene("Kitchen");
        }

        if (collider.gameObject.tag == "#throneRoomDoor")     //checks which object the player is colliding with
        {
            //change scene to throne room
            SceneManager.LoadScene("Throne Room");
        }
    }

    private IEnumerator SetPlayerPositionAfterDelay(Vector3 position)
    {
        // Wait for 0.1 seconds
        yield return new WaitForSeconds(0.1f);

        // Set the player's position
        transform.position = position;
    }

    void AddToInventory(string inventoryObj)
    {
        inventoryObject = inventoryObj;
        PlayerPrefs.SetString("InventoryObject", inventoryObject);

        if (inventoryObject == "EmptyGlass")
        {
            EmptyGlassImg.enabled = true;
        }
        if (inventoryObject == "FullGlass")
        {
            FullGlassImg.enabled = true;
        }
        if (inventoryObject == "PoisonedFullGlass")
        {
            PoisonedGlassImg.enabled = true;
        }
        if (inventoryObject == "EmptyPlate")
        {
            EmptyPlateImg.enabled = true;
        }
        if (inventoryObject == "FullPlate")
        {
            FullPlateImg.enabled = true;
        }
        if (inventoryObject == "PoisonedFullPlate")
        {
            PoisonedPlateImg.enabled = true;
        }
    }

    void RemoveFromInventory(string inventoryObj)
    {
        inventoryObject = inventoryObj;

        if (inventoryObject == "EmptyGlass")
        {
            EmptyGlassImg.enabled = false;
        }
        if (inventoryObject == "FullGlass")
        {
            FullGlassImg.enabled = false;
        }
        if (inventoryObject == "PoisonedFullGlass")
        {
            PoisonedGlassImg.enabled = false;
        }
        if (inventoryObject == "EmptyPlate")
        {
            EmptyPlateImg.enabled = false;
        }
        if (inventoryObject == "FullPlate")
        {
            FullPlateImg.enabled = false;
        }
        if (inventoryObject == "PoisonedFullPlate")
        {
            PoisonedPlateImg.enabled = false;
        }
    }

    void AssasinationSelection(string decisionType)
    {
        if (decisionType == "Poison")
        {
            makingDecision = true;
            int indexOne = 0;
            int indexTwo = 1;
            InteractionNameTxt.text = "Choose your Poison";
            DialogueTxt.text = "1. " + assasinationMethod[indexOne] + " \n    Cost: " + assasinationStats[0, indexOne] + "QP \n     Success Chance: " + assasinationStats[indexOne, 1] + "% \n \n" +
                                "2. " + assasinationMethod[indexTwo] + " \n    Cost: " + assasinationStats[0, indexTwo] + "QP \n     Success Chance: " + assasinationStats[indexTwo, 1] + "% ";
            DialoguePanel.enabled = true;
            InteractionNameTxt.enabled = true;
            DialogueTxt.enabled = true;
        }
    }

    void HideDialoguePanel()
    {
        if (DialoguePanel != null)
        {
            DialoguePanel.enabled = false;
        }

        if (InteractionNameTxt != null)
        {
            InteractionNameTxt.enabled = false;
        }

        if (DialogueTxt != null)
        {
            DialogueTxt.enabled = false;
        }
    }

    void CalculateAssassination()
    {
        System.Random random = new System.Random();

        //Calculate if successful
        int successNum = random.Next(0, 100);
        if (successNum < assasinationStats[1, assasinationIndex])
        {
            //Successful assasination
            isKingAlive = false;
        }

        //calculate EP
        int EpAdded = random.Next(assasinationStats[2, assasinationIndex], assasinationStats[3, assasinationIndex]);
        EvidencePoints = EvidencePoints + EpAdded;
    }

    void RecieveQuest()
    {
        recievingQuest = true;

        InteractionNameTxt.text = "The King: " + questName[activeQuest] + "      " + questReward[activeQuest] + "QP";
        DialogueTxt.text = questDescription[activeQuest] + "\n \n 1. Yes my Liege";
        DialoguePanel.enabled = true;
        InteractionNameTxt.enabled = true;
        DialogueTxt.enabled = true;
    }

    void UpdateUI()
    {
        DaysTxt.text = Days.ToString();
        QPTxt.text = QuestPoints.ToString();
        EPTxt.text = EvidencePoints.ToString();
    }

}

public static class ListExtensions
{
    public static void Shuffle<T>(this IList<T> list)
    {
        System.Random rng = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}