using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float playerDistance;
    [SerializeField] private List<GameObject> playerPoints;
    [SerializeField] private PositionManager positionManager;

    void Update()
    {
        FindPosition();
    }

    public void FindPosition()
    {
        playerDistance = Vector3.Distance(playerPoints[positionManager.currentPoint].transform.position, transform.position);
    }

    public int GetTotalPoints()
    {
        return playerPoints.Count;
    }
}
