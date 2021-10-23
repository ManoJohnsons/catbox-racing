using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackCheckpoints : MonoBehaviour
{
    #region "Events"
    public event EventHandler<KartCheckpointEventArgs> OnKartCorrectCheckpoint;
    public event EventHandler<KartCheckpointEventArgs> OnKartWrongCheckpoint;

    public class KartCheckpointEventArgs : EventArgs
    {
        public Transform kartTransform;
    }
    #endregion

    [SerializeField] private List<Transform> kartTransformList;

    private List<Checkpoint> checkpointList;
    private List<int> nextCheckpointIndexList;

    void Awake()
    {
        Transform checkpointsTransform = transform.Find("Checkpoints");
        checkpointList = new List<Checkpoint>();
        foreach (Transform checkpointSingleTransform in checkpointsTransform)
        {
            Checkpoint checkpoint = checkpointSingleTransform.GetComponent<Checkpoint>();

            checkpoint.SetTrackCheckpoints(this);

            checkpointList.Add(checkpoint);

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
            OnKartCorrectCheckpoint?.Invoke(this, new KartCheckpointEventArgs {kartTransform = kartTransform});

        }
        else
        {
            //Wrong checkpoint
            Debug.Log("Wrong");
            OnKartWrongCheckpoint?.Invoke(this, new KartCheckpointEventArgs { kartTransform = kartTransform });
        }
    }

    public void ResetCheckpoint(Transform kartTransform)
    {
        int nextCheckpointIndex = nextCheckpointIndexList[kartTransformList.IndexOf(kartTransform)];
        nextCheckpointIndexList[kartTransformList.IndexOf(kartTransform)] = 0;
    }

    public Transform GetNextCheckpoint(Transform kartTransform)
    {
        int nextCheckpointIndex = nextCheckpointIndexList[kartTransformList.IndexOf(kartTransform)];

        return checkpointList[nextCheckpointIndex].transform;
    }
}


