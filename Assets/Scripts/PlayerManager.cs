using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerManager : MonoBehaviour
{
    public string inventoryObject;  //needs to be carried over to next scene

    bool recievingQuest = false;
    bool makingDecision = false;

    int activeQuest;
    string[] questName = new string[2] { "Prepare My Meal", "Get Me A Drink" };
    int[] questReward = new int[2] { 2, 1 };
    string[] questDescription = new string[2]
    { " a. Get empty Plate \n b. Prepare Meal \n c. Deliver Meal ",
      " a. Get empty glass \n b. Pour wine \n c. Deliver wine " };

    public UnityEngine.UI.Image questPanel;
    public TMP_Text questTxt;

    public int assasinationIndex;
    string[] assasinationMethod = new string[2] { "Lethal Poison", "Basic Poison" };   //Method Name
    int[,] assasinationStats = new int[4, 2] {   { 8,    4 },        //Method cost in QP (0)
                                                 { 80,   40 },      //Methods success chance (1)
                                                 { 1,    1},        //Method Evidence Points Min (2)
                                                 { 3,    3}  };     //Method Evidence Points Max (3)
    private int dailyQuests;
    public int Days;    //needs to be carried over to next scene
    public int QuestPoints; //needs to be carried over to next scene
    public int EvidencePoints;  //needs to be carried over to next scene

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
    private new Collider2D collider;        //collider that player collides with

    System.Random random = new System.Random();


    // Start is called before the first frame update
    void Start()
    {
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

        UpdateUI();
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
    }

    private void OnTriggerExit2D(Collider2D collider2d)
    {
        colliding = false;

        HideDialoguePanel();
        makingDecision = false;
        recievingQuest = false;

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
            if (dailyQuests < 2)
            {
                RecieveQuest();
            }
            if (activeQuest == 0)
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
            SceneManager.LoadScene("Storage Room");
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
        DialoguePanel.enabled = false;
        InteractionNameTxt.enabled = false;
        DialogueTxt.enabled = false;
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

