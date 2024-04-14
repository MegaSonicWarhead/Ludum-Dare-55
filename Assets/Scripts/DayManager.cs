using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DayManager : MonoBehaviour
{
    public int Days;
    public TMP_Text DaysTxt;
    public GameObject book;

    private void Start()
    {
        Days = 0;
        UpdateUI();
    }

    void UpdateUI()
    {
        DaysTxt.text = Days.ToString();
    }

    public void StartNextDay()
    {
        Days++;
        UpdateUI();
        book.SetActive(true); // Activate the book for the player to interact with
    }
}
