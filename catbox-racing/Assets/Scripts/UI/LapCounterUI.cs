using UnityEngine;
using TMPro;

public class LapCounterUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI lapCounter;
    [SerializeField] private LapComplete lapComplete;

    void Start()
    {
        lapComplete.OnPlayerLapDone += LapComplete_OnPlayerLapDone;
        lapCounter.text = lapComplete.GetLapsDone().ToString() + " / " + lapComplete.GetLapsMax().ToString();
    }

    private void LapComplete_OnPlayerLapDone(object sender, System.EventArgs e)
    {
        lapCounter.text = lapComplete.GetLapsDone().ToString() + " / " + lapComplete.GetLapsMax().ToString();
    }
}
