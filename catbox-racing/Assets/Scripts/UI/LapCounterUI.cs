using UnityEngine;
using TMPro;

public class LapCounterUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI lapCounter;
    [SerializeField] private LapComplete lapComplete;

    // Start is called before the first frame update
    void Start()
    {
        lapComplete.OnLapDone += LapComplete_OnLapDone;
        lapCounter.text = lapComplete.GetLapsDone().ToString() + " / " + lapComplete.GetLapsMax().ToString();
    }

    private void LapComplete_OnLapDone(object sender, System.EventArgs e)
    {
        lapCounter.text = lapComplete.GetLapsDone().ToString() + " / " + lapComplete.GetLapsMax().ToString();
    }
}
