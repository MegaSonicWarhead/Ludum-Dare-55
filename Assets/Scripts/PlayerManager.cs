using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerManager : MonoBehaviour
{
    public string inventoryObject;      //needs to be carried over to next scene
    public int dailyQuests;             //needs to be carried over
    public int Days;                    //needs to be carried over to next scene
    public int QuestPoints;             //needs to be carried over to next scene
    public int EvidencePoints;          //needs to be carried over to next scene
    public int numberOfRuns = 0;        //carry over
    public QuestManager questManager; // Reference to your QuestManager component
    bool makingDecision = false;    //carried over
    bool openDialogue = false;      //carried over
    bool recievingQuest = false;

    int activeQuest = 3;    //needs to be carried over
    const int numberOfQuests = 2;
    string[] questName = new string[numberOfQuests] { "Prepare My Meal", "Get Me A Drink" };
    int[] questReward = new int[numberOfQuests] { 2, 1 };
    string[] questDescription = new string[numberOfQuests]
    { " a. Get empty Plate \n b. Prepare Meal \n c. Deliver Meal ",
      " a. Get empty glass \n b. Pour wine \n c. Deliver wine " };

    

    public int assasinationIndex;   //carried over
    string[] assasinationMethod = new string[2] { "Lethal Poison", "Basic Poison" };   //Method Name
    int[,] assasinationStats = new int[4, 2] {   { 8,    4 },        //Method cost in QP (0)
                                                 { 80,   40 },      //Methods success chance (1)
                                                 { 1,    1},        //Method Evidence Points Min (2)
                                                 { 3,    3}  };     //Method Evidence Points Max (3)



    public UnityEngine.UI.Image questPanel;
    public TMP_Text questTxt;

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
    public UnityEngine.UI.Image dialoguePanel;

    public TMP_Text actionLabel;

    public UnityEngine.UI.Image DialoguePanel;
    public TMP_Text InteractionNameTxt;
    public TMP_Text DialogueTxt;

    bool eBeingPressed = false;
    bool OneBeingPressed;
    bool TwoBeingPressed;
    bool colliding = false;
    private new Collider2D collider;        //collider that player collides with

    System.Random random = new System.Random();


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        if (EmptyPlateImg != null)
        {
            EmptyPlateImg.enabled = false;
        }
        if (inventoryObject?.Equals("EmptyPlate") ?? false)
        {
            Debug.Log("inventoryObject is: " + inventoryObject);
        }
        // Instantiate quests and set their properties
        Quest quest1 = gameObject.AddComponent<Quest>();
        quest1.SetQuest("Prepare My Meal", "a. Get empty Plate \n b. Prepare Meal \n c. Deliver Meal", 2, 1);

        Quest quest2 = gameObject.AddComponent<Quest>();
        quest2.SetQuest("Get Me A Drink", "a. Get empty glass \n b. Pour wine \n c. Deliver wine", 1, 1);

        // Add quests to the quest manager
        //questManager.quests = new Quest[] { quest1, quest2 };

        // Load saved data from PlayerPrefs on scene start
        LoadPlayerData();

        numberOfRuns = numberOfRuns + 1;
        Debug.Log("Number of runs: " + numberOfRuns);

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
        if (activeQuest < numberOfQuests)
        {
            ShowQuestPanel();
        }
        Debug.Log("PlayerManager Script is active");
    }

    // Update is called once per frame
    void Update()
    {
        if (colliding)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Interact();
            }
        }
        
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

        if (OneBeingPressed && openDialogue)
        {
            HideDialoguePanel();
        }
        if (TwoBeingPressed && openDialogue)
        {  
            HideDialoguePanel();
        }

        if (OneBeingPressed && makingDecision)
        {
            assasinationIndex = 0; //Lethal Poison
            makingDecision = false;

            if (recievingQuest)
            {
                ShowQuestPanel();
                recievingQuest = false;
            }
        }
        if (TwoBeingPressed && makingDecision)
        {
            assasinationIndex = 1; //Basic Poison
            makingDecision = false;
        }

        if (activeQuest != 3 || activeQuest !=  (numberOfQuests + 1))
        {
            ShowQuestPanel();
        }

        UpdateUI();
    }

    void OnDestroy()
    {
        // Save player data when the GameObject is destroyed (e.g., when changing scenes)
        SavePlayerData();
    }

    void OnTriggerEnter2D(Collider2D collider2d)
    {
        Debug.Log("Player has collided");
        colliding = true;
        collider = collider2d;

        if (collider.gameObject.tag == "#foodCrate")     //checks which object the player is colliding with
        {
            actionLabel.text = "Press E to prepare meal";
            actionLabel.enabled = true;
        }

        if (collider.gameObject.tag == "#poisonCrate")     //checks which object the player is colliding with
        {
            //A label with the interaction description must pop up
            actionLabel.text = "Press E to add poison";
            actionLabel.enabled = true;
        }

        if (collider.gameObject.tag == "#throne")     //checks which object the player is colliding with
        {
            actionLabel.text = "Press E to talk to the King";
            actionLabel.enabled = true;
        }

        if (collider.gameObject.tag == "#wine")     //checks which object the player is colliding with
        {
            actionLabel.text = "Press E to fill glass";
            actionLabel.enabled = true;
        }

        if (collider.gameObject.tag == "#glass")     //checks which object the player is colliding with
        {
            actionLabel.text = "Press E to pick up Glass";
            actionLabel.enabled = true;
        }

        if (collider.gameObject.tag == "#plate")     //checks which object the player is colliding with
        {
            actionLabel.text = "Press E to get plate";
            actionLabel.enabled = true;
        }

        if (collider.gameObject.tag == "#storageDoor")     //checks which object the player is colliding with
        {
            actionLabel.text = "Press E to go to Storage";
            actionLabel.enabled = true;
        }

        if (collider.gameObject.tag == "#kitchenDoor")     //checks which object the player is colliding with
        {
            actionLabel.text = "Press E to go to Kitchen";
            actionLabel.enabled = true;
        }

        if (collider.gameObject.tag == "#throneRoomDoor")     //checks which object the player is colliding with
        {
            actionLabel.text = "Press E to go to Throne Room";
            actionLabel.enabled = true;
        }

        if (collider.gameObject.tag == "#book")
        {
            actionLabel.text = "Press E to progress to next day";
            actionLabel.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider2d)
    {
        colliding = false;

        HideDialoguePanel();

        //label with description must dissapear
        actionLabel.enabled = false;
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
            }
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
            }
            if (inventoryObject == "FullGlass")    //checks if player has full glass
            {
                //replace Empty Plate with PoisonedFullGlass
                RemoveFromInventory(inventoryObject);
                AddToInventory("PoisonedFullGlass");
            }
        }

        if (collider.gameObject.tag == "#throne")     //checks which object the player is colliding with
        {
            //interact with king, by giving recieving quest or ending quest etc
            //subtract certain objects from inventory
            if (activeQuest == (numberOfQuests + 1) && dailyQuests < 2)  //if player currently doesnt have a quest
            {
                RecieveQuest();
            }
            else if (activeQuest == 0)
            {
                if (inventoryObject == "FullPlate")
                {
                    CompleteQuest("FullPlate");
                }
                else if (inventoryObject == "PoisonedFullPlate")
                {
                    CompleteQuest("PoisonedFullPlate");
                    CalculateAssassination();
                }
            }
            else if (activeQuest == 1)
            {
                if (inventoryObject == "FullGlass")
                {
                    CompleteQuest("FullGlass");
                }
                else if (inventoryObject == "PoisonedFullGlass")
                {
                    CompleteQuest("PoisonedFullGlass");
                    CalculateAssassination();
                }
            }
            else if (dailyQuests > 2)
            {
                openDialogue = true;

                InteractionNameTxt.text = "The King:";
                DialogueTxt.text = " I have no more tasks for you today. \n \n 1. Yes my Liege";
                DialoguePanel.enabled = true;
                InteractionNameTxt.enabled = true;
                DialogueTxt.enabled = true;
            }
        }

        if (collider.gameObject.tag == "#wine")     //checks which object the player is colliding with
        {
            if (inventoryObject == "EmptyGlass")    //checks if player has EmptyGlass
            {
                //replace Empty Plate with FullGlass
                RemoveFromInventory(inventoryObject);
                AddToInventory("FullGlass");
            }
        }

        if (collider.gameObject.tag == "#glass")     //checks which object the player is colliding with
        {
            RemoveFromInventory(inventoryObject);
            AddToInventory("EmptyGlass");
            //Add Empty glass to inventory
        }

        if (collider.gameObject.tag == "#plate")     //checks which object the player is colliding with
        {
            RemoveFromInventory(inventoryObject);
            AddToInventory("EmptyPlate");
            //add emptyPlate to invetory
        }

        if (collider.gameObject.tag == "#storageDoor")     //checks which object the player is colliding with
        {
            //change scene to storage room
            ChangeScene("Storage Room");
        }

        if (collider.gameObject.tag == "#kitchenDoor")     //checks which object the player is colliding with
        {
            //change scene to kitchen
            ChangeScene("Kitchen");
        }

        if (collider.gameObject.tag == "#throneRoomDoor")     //checks which object the player is colliding with
        {
            //change scene to throne room
            ChangeScene("Throne Room");
        }

        if (collider.gameObject.tag == "#book")
        {
            //sets daily Quests to 0
            dailyQuests = 0;
            Days = Days + 1;
            activeQuest = numberOfQuests + 1;   //removes current quest
            Debug.Log("Active quest is now " + (numberOfQuests + 1));

            questPanel.enabled = false;
            questTxt.enabled = false;
        }
    }

    void AddToInventory(string inventoryObj)
    {
        inventoryObject = inventoryObj;

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
        else
        {
            Debug.Log("Nothing in inventory");
        }
    }

    void RemoveFromInventory(string inventoryObj)
    {
        if (inventoryObj == "EmptyGlass")
        {
            EmptyGlassImg.enabled = false;
        }
        if (inventoryObj == "FullGlass")
        {
            FullGlassImg.enabled = false;
        }
        if (inventoryObj == "PoisonedFullGlass")
        {
            PoisonedGlassImg.enabled = false;
        }
        if (inventoryObj == "EmptyPlate")
        {
            EmptyPlateImg.enabled = false;
        }
        if (inventoryObj == "FullPlate")
        {
            FullPlateImg.enabled = false;
        }
        if (inventoryObj == "PoisonedFullPlate")
        {
            PoisonedPlateImg.enabled = false;
        }

        inventoryObject = "";
    }

    void AssasinationSelection(string decisionType)
    {
        if (decisionType == "Poison")
        {
            openDialogue = true;

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

    public void HideDialoguePanel()
    {
        if (dialoguePanel != null)
        {
            dialoguePanel.enabled = false;
        }
        else
        {
            Debug.LogWarning("Dialogue panel is null.");
        }
    }

    void ShowQuestPanel()
    {
        questTxt.text = questName[activeQuest] + "\n" + questDescription[activeQuest];

        questPanel.enabled = true;
        questTxt.enabled = true;
    }

    void CalculateAssassination()
    {
        System.Random random = new System.Random();

        //Calculate if successful
        int successNum = random.Next(0, 100);
        if (successNum < assasinationStats[1, assasinationIndex])
        {
            InteractionNameTxt.text = "The King: ";
            DialogueTxt.text = "GAH! \n \n 1. Oh no! My Liege!";
            DialoguePanel.enabled = true;
            InteractionNameTxt.enabled = true;
            DialogueTxt.enabled = true;
            
            //Successful assasination
            isKingAlive = false;
        }

        //calculate EP
        int EpAdded = random.Next(assasinationStats[2, assasinationIndex], assasinationStats[3, assasinationIndex]);
        EvidencePoints = EvidencePoints + EpAdded;
    }

    void RecieveQuest()
    {
        openDialogue = true;
        recievingQuest = true;

            activeQuest = random.Next(0, 2);
            InteractionNameTxt.text = "The King: " + questName[activeQuest] + "      " + questReward[activeQuest] + "QP";
            DialogueTxt.text = questDescription[activeQuest] + "\n \n 1. Yes my Liege";

        dailyQuests = dailyQuests + 1;

        DialoguePanel.enabled = true;
        InteractionNameTxt.enabled = true;
        DialogueTxt.enabled = true;
    }

    void CompleteQuest(string item)
    {
        openDialogue = true;

        RemoveFromInventory(item);

        InteractionNameTxt.text = "The King: ";
        DialogueTxt.text = "Well done. \n \n 1. It is my pleasure My Liege. (+ " + questReward[activeQuest] + "QP!)";
        DialoguePanel.enabled = true;
        InteractionNameTxt.enabled = true;
        DialogueTxt.enabled = true;

        questPanel.enabled = false;
        questTxt.enabled = false;

        activeQuest = numberOfQuests + 1;
        QuestPoints = QuestPoints + questReward[activeQuest];

        if (dailyQuests < 2)
        {
            activeQuest = random.Next(0, 2);
        }
            
    }

    void UpdateUI()
    {
        DaysTxt.text = Days.ToString();
        QPTxt.text = QuestPoints.ToString();
        //EPTxt.text = EvidencePoints.ToString();
        EPTxt.text = activeQuest.ToString();
        QPTxt.text = dailyQuests.ToString();
    }

    void LoadPlayerData()
    {
        // Load inventoryObject
        if (PlayerPrefs.HasKey("InventoryObject"))
        {
            inventoryObject = PlayerPrefs.GetString("InventoryObject");
            // You might want to update the UI here
        }

        // Load other variables
        Days = PlayerPrefs.GetInt("Days", Days);
        activeQuest = PlayerPrefs.GetInt("ActiveQuest", activeQuest);
        QuestPoints = PlayerPrefs.GetInt("QuestPoints", QuestPoints);
        EvidencePoints = PlayerPrefs.GetInt("EvidencePoints", EvidencePoints);
    }

    void SavePlayerData()
    {
        // Save inventoryObject
        PlayerPrefs.SetString("InventoryObject", inventoryObject);

        // Save other variables
        PlayerPrefs.SetInt("DailyQuest", dailyQuests);
        PlayerPrefs.SetInt("Days", Days);
        PlayerPrefs.SetInt("ActiveQuest", activeQuest);
        PlayerPrefs.SetInt("QuestPoints", QuestPoints);
        PlayerPrefs.SetInt("EvidencePoints", EvidencePoints);
        PlayerPrefs.SetInt("assasinationIndex", assasinationIndex);
        PlayerPrefs.SetInt("NumberOfRuns", numberOfRuns);

        // Convert boolean value to integer (0 or 1)
        int makingDecisionInt = makingDecision ? 1 : 0;
        int openDialogueInt = openDialogue ? 1 : 0;
        // Save the integer value to PlayerPrefs
        PlayerPrefs.SetInt("makingDecision", makingDecisionInt);
        PlayerPrefs.SetInt("openDialogue", openDialogueInt);


        // PlayerPrefs save to disk (not needed for immediate retrieval but for persistence)
        PlayerPrefs.Save();
    }

    public void ChangeScene(string sceneName)
    {
        // Save player data before changing scene
        SavePlayerData();

        // Change scene
        SceneManager.LoadScene(sceneName);
    }
}

