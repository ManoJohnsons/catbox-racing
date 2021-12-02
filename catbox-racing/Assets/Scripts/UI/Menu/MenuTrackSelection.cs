using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuTrackSelection : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI minuteDisplay;
    [SerializeField] private TextMeshProUGUI secondDisplay;
    [SerializeField] private TextMeshProUGUI millisecondDisplay;
    private int minuteCount;
    private int secondCount;
    private float milliCount;

    void Start()
    {
        minuteCount = PlayerPrefs.GetInt("MinSave");
        secondCount = PlayerPrefs.GetInt("SecSave");
        milliCount = PlayerPrefs.GetInt("MilliSave");

        minuteDisplay.text = "" + minuteCount + ":";
        secondDisplay.text = "" + secondCount + ".";
        millisecondDisplay.text = "" + milliCount;
    }
}
