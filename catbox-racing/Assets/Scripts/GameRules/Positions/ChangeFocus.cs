using UnityEngine;

public class ChangeFocus : MonoBehaviour
{
    [SerializeField] private LapComplete lapComplete;
    [SerializeField] private PositionManager positionManager;
    private bool isUsed;

    void Awake()
    {
        lapComplete.OnKartLapDone += LapComplete_OnKartLapDone;
    }

    private void LapComplete_OnKartLapDone(object sender, System.EventArgs e)
    {
        isUsed = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out KartController _) && !isUsed)
        {
            isUsed = true;
            positionManager.FocusPassed();
        }
    }

}
