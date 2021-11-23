using System;
using UnityEngine;
using TMPro;

public class PositionManager : MonoBehaviour
{
    public int currentPoint;
    [SerializeField] private PlayerStats player;
    [SerializeField] private AIStats[] AI;
    [SerializeField] private TextMeshProUGUI positionText;
    private float[] kartPositions = new float[4];
    private float playerPosition;
    private int currentPosition;

    void Update()
    {
        PositionCalculation();
        positionText.text = currentPosition.ToString() + " / " + kartPositions.Length;
    }

    public void PositionCalculation()
    {
        kartPositions[0] = player.playerDistance;
        kartPositions[1] = AI[0].AIDistance;
        kartPositions[2] = AI[1].AIDistance;
        kartPositions[3] = AI[2].AIDistance;

        playerPosition = player.playerDistance;

        Array.Sort(kartPositions);

        int kartIndex = Array.IndexOf(kartPositions, playerPosition);

        switch (kartIndex) 
        {
            case 0:
                currentPosition = 1;
                break;
            case 1:
                currentPosition = 2;
                break;
            case 2:
                currentPosition = 3;
                break;
            case 3:
                currentPosition = 4;
                break;
        }
    }

    public void FocusPassed()
    {
        currentPoint = (currentPoint + 1) % player.GetTotalPoints();
    }
}
