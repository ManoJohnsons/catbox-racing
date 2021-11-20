using UnityEngine;

public class AIStats : MonoBehaviour
{
    public float AIDistance;
    [SerializeField] private GameObject[] points;
    [SerializeField] private PositionManager positionManager;

    void Update()
    {
        FindPosition();
    }

    public void FindPosition()
    {
        AIDistance = Vector3.Distance(points[positionManager.currentPoint].transform.position, transform.position);
    }
}
