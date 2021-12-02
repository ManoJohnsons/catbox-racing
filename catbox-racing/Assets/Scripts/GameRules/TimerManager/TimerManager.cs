using UnityEngine;
using TMPro;

public class TimerManager : MonoBehaviour
{
    [SerializeField] private LapComplete lapComplete;
    [SerializeField] private GameObject minuteBox;
    [SerializeField] private GameObject secondBox;
    [SerializeField] private GameObject millisecondBox;

    private int minuteCount;
    private int secondCount;
    private float millisecondCount;
    private string millisecondDisplay;
    private float rawTime;
    private float rawTimePlayer;

    private void Awake()
    {
        lapComplete.OnPlayerLapDone += LapComplete_OnPlayerLapDone;
    }

    private void LapComplete_OnPlayerLapDone(object sender, System.EventArgs e)
    {
        rawTimePlayer = PlayerPrefs.GetFloat("RawTime");
        if (rawTime <= rawTimePlayer)
        {
            if (secondCount <= 9)
                secondBox.GetComponent<TextMeshProUGUI>().text = "0" + secondCount + ".";
            else
                secondBox.GetComponent<TextMeshProUGUI>().text = "" + secondCount + ".";

            if (minuteCount <= 9)
                minuteBox.GetComponent<TextMeshProUGUI>().text = "0" + minuteCount + ".";
            else
                minuteBox.GetComponent<TextMeshProUGUI>().text = "" + minuteCount + ".";

            millisecondBox.GetComponent<TextMeshProUGUI>().text = "" + millisecondCount;
        }

        PlayerPrefs.SetInt("MinSave", minuteCount);
        PlayerPrefs.SetInt("SecSave", secondCount);
        PlayerPrefs.SetFloat("MilliSave", millisecondCount);
        PlayerPrefs.SetFloat("RawTime", rawTime);

        millisecondCount = 0;
        secondCount = 0;
        millisecondCount = 0;
        rawTime = 0;
    }

    private void Update()
    {
        millisecondCount += Time.deltaTime * 10;
        rawTime += Time.deltaTime;
        millisecondDisplay = millisecondCount.ToString("F0");
        millisecondBox.GetComponent<TextMeshProUGUI>().text = "" + millisecondDisplay;

        if (millisecondCount >= 10)
        {
            millisecondCount = 0;
            secondCount += 1;
        }

        if (secondCount <= 9)
            secondBox.GetComponent<TextMeshProUGUI>().text = "0" + secondCount + ".";
        else
            secondBox.GetComponent<TextMeshProUGUI>().text = "" + secondCount + ".";

        if (secondCount >= 60)
        {
            secondCount = 0;
            minuteCount += 1;
        }

        if (minuteCount <= 9)
            minuteBox.GetComponent<TextMeshProUGUI>().text = "0" + minuteCount + ":";
        else
            minuteBox.GetComponent<TextMeshProUGUI>().text = "" + minuteCount + ":";
    }
}
