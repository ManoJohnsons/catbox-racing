using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackCheckpoints : MonoBehaviour
{
    public event EventHandler OnPlayerCorrectCheckpoint;
    public event EventHandler OnPlayerWrongCheckpoint;

    [SerializeField] private List<Transform> kartTransformList;

    private List<Checkpoint> checkpointList;
    private List<int> nextCheckpointIndexList;

    private int lastLapCheckpoint;

    [SerializeField] private LapController lapController;
    void Awake()
    {
        Transform checkpointsTransform = transform.Find("Checkpoints");
        checkpointList = new List<Checkpoint>();
        foreach (Transform checkpointSingleTransform in checkpointsTransform)
        {
            Checkpoint checkpoint = checkpointSingleTransform.GetComponent<Checkpoint>();

            checkpoint.SetTrackCheckpoints(this);

            checkpointList.Add(checkpoint);
            lastLapCheckpoint++;
        }

        nextCheckpointIndexList = new List<int>();
        foreach (Transform kartTransform in kartTransformList)
        {
            nextCheckpointIndexList.Add(0);
        }
    }

    public void KartTroughCheckpoint(Checkpoint checkpoint, Transform kartTransform)
    {
        int nextCheckpointIndex = nextCheckpointIndexList[kartTransformList.IndexOf(kartTransform)];
        if (checkpointList.IndexOf(checkpoint) == nextCheckpointIndex)
        {
            //Correct checkpoint
            Debug.Log("Correct");
            Debug.Log(nextCheckpointIndex);
            nextCheckpointIndexList[kartTransformList.IndexOf(kartTransform)] = (nextCheckpointIndex + 1) % checkpointList.Count;
            OnPlayerCorrectCheckpoint?.Invoke(this, EventArgs.Empty);

            if(nextCheckpointIndex == lastLapCheckpoint - 1)
            {
                Debug.Log("Lap Complete");
                lapController.IncrementLap();
            }
        }
        else
        {
            //Wrong checkpoint
            Debug.Log("Wrong");
            OnPlayerWrongCheckpoint?.Invoke(this, EventArgs.Empty);
        }
    }
}
