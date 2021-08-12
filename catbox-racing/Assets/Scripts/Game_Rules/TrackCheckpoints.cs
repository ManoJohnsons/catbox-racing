using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackCheckpoints : MonoBehaviour
{
    [SerializeField] private List<Transform> kartTransformList;

    private List<Checkpoint> checkpointList;
    private List<int> nextCheckpointIndexList;

    void Awake()
    {
        Transform checkpointsTransform = transform.Find("Checkpoint");

        checkpointList = new List<Checkpoint>();
        foreach (Transform checkpointTransform in checkpointsTransform)
        {
            Checkpoint checkpoint = checkpointTransform.GetComponent<Checkpoint>();

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
            nextCheckpointIndexList[kartTransformList.IndexOf(kartTransform)] = (nextCheckpointIndex + 1) % checkpointList.Count;
            Debug.Log(nextCheckpointIndex);
        }
    }
}
