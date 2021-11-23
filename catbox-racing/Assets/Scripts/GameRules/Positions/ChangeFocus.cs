using UnityEngine;

public class ChangeFocus : MonoBehaviour
{
    [SerializeField] private PositionManager positionManager;
    private bool isUsed;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out KartController _) && !isUsed)
        {
            isUsed = true;
            positionManager.FocusPassed();
        }
    }
}
