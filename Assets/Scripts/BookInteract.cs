using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookInteract : MonoBehaviour
{
    public DayManager dayManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            dayManager.StartNextDay();
            gameObject.SetActive(false); // Deactivate the book until the next day
        }
    }
}
