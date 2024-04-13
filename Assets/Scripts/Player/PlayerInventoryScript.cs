using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryScript : MonoBehaviour
{
    private Dictionary<string, List<GameObject>> inventory = new Dictionary<string, List<GameObject>>();

    public void AddToInventory(GameObject item, string objectType = "")
    {
        if (!inventory.ContainsKey(objectType))
        {
            inventory[objectType] = new List<GameObject>();
        }

        inventory[objectType].Add(item);
        item.SetActive(false); // Deactivate the object instead of destroying it
    }

    public void CombineItems(string objectType1, string objectType2, string combinedObjectType, GameObject combinedItemPrefab)
    {
        if (inventory.ContainsKey(objectType1) && inventory.ContainsKey(objectType2))
        {
            // Remove items from inventory
            inventory[objectType1].RemoveAt(0);
            inventory[objectType2].RemoveAt(0);

            // Add combined item to inventory
            if (!inventory.ContainsKey(combinedObjectType))
            {
                inventory[combinedObjectType] = new List<GameObject>();
            }

            GameObject combinedItem = Instantiate(combinedItemPrefab, transform.position, Quaternion.identity);
            inventory[combinedObjectType].Add(combinedItem);
        }
    }

    public void GiveItemToKing(string objectType, KingScript king)
    {
        if (inventory.ContainsKey(objectType))
        {
            GameObject item = inventory[objectType][0];
            inventory[objectType].RemoveAt(0);
            king.ReceiveItem(item, objectType);
        }
    }

    public List<GameObject> GetInventory(string objectType)
    {
        if (inventory.ContainsKey(objectType))
        {
            return inventory[objectType];
        }

        return new List<GameObject>();
    }
}
