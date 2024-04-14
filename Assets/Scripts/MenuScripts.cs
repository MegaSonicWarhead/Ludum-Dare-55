using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuScripts : MonoBehaviour
{
    public TMP_Text TitleText;
    public TMP_Text InstructionsTxt;
    public UnityEngine.UI.Image HowToPanel;
    public Button returnButton;


    // Start is called before the first frame update
    void Start()
    {
        TitleText.enabled = false;
        InstructionsTxt.enabled = false;
        HowToPanel.enabled = false;
        returnButton.gameObject.SetActive(false);
        returnButton.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HowToPlayButton()
    {
        TitleText.enabled = true;
        InstructionsTxt.enabled = true;
        HowToPanel.enabled = true;
        returnButton.gameObject.SetActive(true);
        returnButton.enabled = true;
    }

    public void ExitButton()
    {
        Debug.Log("Quitting the application");
        // Check if the application is running in the Unity Editor (for testing purposes)
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit(); // Quit the application in a standalone build
        #endif
    }

    public void BackButton()
    {
        TitleText.enabled = false;
        InstructionsTxt.enabled = false;
        HowToPanel.enabled = false;
        returnButton.gameObject.SetActive(false);
        returnButton.enabled = false;

    }
}
