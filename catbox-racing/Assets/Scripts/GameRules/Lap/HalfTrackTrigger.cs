using UnityEngine;

public class HalfTrackTrigger : MonoBehaviour
{
    [SerializeField] private GameObject startTrigger;
    [SerializeField] private GameObject halfTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out KartController _))
        {
            startTrigger.SetActive(true);
            halfTrigger.SetActive(false);
        }
    }
}
