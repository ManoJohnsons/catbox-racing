using UnityEngine;

public class ChangeFocus : MonoBehaviour
{
    [SerializeField] private PositionManager positionManager;
    private bool isUsed;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("KartAgent") && !isUsed || other.CompareTag("KartPlayer") && !isUsed)
        {
            isUsed = true;
            positionManager.currentPoint++;
        }
    }
}
