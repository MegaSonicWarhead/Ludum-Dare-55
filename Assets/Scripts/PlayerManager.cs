using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public TMP_Text actionLabel;
    public string inventoryObject;  //needs to be carried over to next scene

    public int Days;    //needs to be carried over to next scene
    public int QuestPoints; //needs to be carried over to next scene
    public int EvidencePoints;  //needs to be carried over to next scene

    public UnityEngine.UI.Image EmptyPlateImg;
    public UnityEngine.UI.Image FullPlateImg;
    public UnityEngine.UI.Image PoisonedPlateImg;
    public UnityEngine.UI.Image EmptyGlassImg;
    public UnityEngine.UI.Image FullGlassImg;
    public UnityEngine.UI.Image PoisonedGlassImg;

    
    bool eBeingPressed = false;
    bool colliding = false;
    private Collider2D collider;




    // Start is called before the first frame update
    void Start()
    {
        actionLabel.enabled = false;

        EmptyPlateImg.enabled = false;
        FullPlateImg.enabled = false;
        PoisonedPlateImg.enabled = false;
        EmptyGlassImg.enabled = false;
        FullGlassImg.enabled = false;
        PoisonedGlassImg.enabled = false;
        FullPlateImg.enabled = false;
        Debug.Log("Interaction Script is active");
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
        else
        {
            eBeingPressed = false;
        }
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

    private void OnCollisionExit2D(Collision2D collision)
    {
        colliding = false;

        //label with description must dissapear
        actionLabel.enabled = false;
    }

    void Interact()
    {
        if (collider.gameObject.tag == "#foodCrate")     //checks which object the player is colliding with
        {
                if (inventoryObject == "EmptyPlate")
            {
                RemoveFromInventory(inventoryObject);
                AddToInventory("FullPlate");
            }
                //replace Empty Plate with FullPlate
                Debug.Log("Preparing meal");
                AddToInventory("FullPlate"); //testing purposes
        }

        if (collider.gameObject.tag == "#poisonCrate")     //checks which object the player is colliding with
        {
                //choose between 2 poisons
                //subtract QP
                //check if inventory contains FullPlate or FullGlass and replace it with PoisonedFullPlate or PoisonedFullGlass
        }

        if (collider.gameObject.tag == "#throne")     //checks which object the player is colliding with
        {
                //interact with king, by giving recieving quest or ending quest etc
                //subtract certain objects from inventory
        }

        if (collider.gameObject.tag == "#wine")     //checks which object the player is colliding with
        {
                //check if EmptyGlass is in inventory
                //change EmptyGlass to FullGlass in inventory
        }

        if (collider.gameObject.tag == "#glass")     //checks which object the player is colliding with
        {
            AddToInventory("EmptyGlass");
            //Add Empty glass to inventory
        }

        if (collider.gameObject.tag == "#plate")     //checks which object the player is colliding with
        {
                //add emptyPlate to invetory
        }

        if (collider.gameObject.tag == "#storageDoor")     //checks which object the player is colliding with
        {
                //change scene to storage room
        }

        if (collider.gameObject.tag == "#kitchenDoor")     //checks which object the player is colliding with
        {
                //change scene to kitchen
        }

        if (collider.gameObject.tag == "#throneRoomDoor")     //checks which object the player is colliding with
        {
                //change scene to throne room
        }
    }

    void AddToInventory(string inventoryObj)
    {
        inventoryObject = inventoryObj;

        if (inventoryObject == "EmptyGlass")
        {
            EmptyGlassImg.enabled = true;
        }
        if(inventoryObject == "FullGlass")
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
}
