using UnityEngine;
using TMPro;

public class LapCounterUI : MonoBehaviour
{
    [SerializeField] private LapController lapController;
    [SerializeField] private TextMeshProUGUI lapCountText;
    private int currentLap;
    private int maxLap;

    private void Start()
    {
        currentLap = lapController.GetCurrentLap();
        //maxLap = lapController.GetCurrentMaxLap();
        lapController.OnLapIncrement += LapController_OnLapIncrement;
        ChangeText();
    }

    private void LapController_OnLapIncrement(object sender, System.EventArgs e)
    {
        ChangeText();
    }

    private void ChangeText()
    {
        lapCountText.text = currentLap++.ToString();
    }
}
