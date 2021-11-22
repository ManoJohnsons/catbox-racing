using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float playerDistance;
    [SerializeField] private GameObject[] points;
    [SerializeField] private PositionManager positionManager;

    void Update()
    {
        FindPosition();
    }

    public void FindPosition()
    {
        playerDistance = Vector3.Distance(points[positionManager.currentPoint].transform.position, transform.position);
    }
}
