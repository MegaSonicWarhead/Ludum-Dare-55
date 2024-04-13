using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockScript : MonoBehaviour
{
    public void StartNextDay()
    {
        FindObjectOfType<SceneUI>().LoadNextDay();
    }
}
