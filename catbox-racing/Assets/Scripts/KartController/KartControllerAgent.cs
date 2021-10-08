﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class KartControllerAgent : Agent
{
    [SerializeField] private TrackCheckpoints trackCheckpoints;
    [SerializeField] private Transform spawnPosition;

    private KartController kartController;

    private void Awake()
    {
        kartController = GetComponent<KartController>();
    }

    private void Start()
    {
        trackCheckpoints.OnKartCorrectCheckpoint += TrackCheckpoints_OnKartCorrectCheckpoint;
        trackCheckpoints.OnKartWrongCheckpoint += TrackCheckpoints_OnKartWrongCheckpoint;
    }

    private void TrackCheckpoints_OnKartWrongCheckpoint(object sender, TrackCheckpoints.KartCheckpointEventArgs e)
    {
        if(e.kartTransform == transform)
        {
            AddReward(-1f);
        }
    }

    private void TrackCheckpoints_OnKartCorrectCheckpoint(object sender, TrackCheckpoints.KartCheckpointEventArgs e)
    {
        if(e.kartTransform == transform)
        {
            AddReward(1f);
        }
    }

    public override void OnEpisodeBegin()
    {
        transform.position = spawnPosition.position + new Vector3(Random.Range(-4f, +4f), 0, Random.Range(-6f, 0f));
        transform.forward = spawnPosition.forward;
        trackCheckpoints.ResetCheckpoint(transform);
        kartController.StopCompletely();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        Vector3 checkpointForward = trackCheckpoints.GetNextCheckpoint(transform).transform.forward;
        float directionDot = Vector3.Dot(transform.forward, checkpointForward);
        sensor.AddObservation(directionDot);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float forwardAmount = 0f;
        float turnAmount = 0f;
        bool isDrifting = false;
        bool isUsingItem = false;

        switch (actions.DiscreteActions[0]) 
        {
            case 0: forwardAmount = 0f; break;
            case 1: forwardAmount = +1f; break;
            case 2: forwardAmount = -1f; break;
        }
        switch (actions.DiscreteActions[1]) 
        {
            case 0: turnAmount = 0f; break;
            case 1: turnAmount = +1f; break;
            case 2: turnAmount = -1f; break;
        }
        switch (actions.DiscreteActions[2]) 
        { 
            case 0: isDrifting = false; break;
            case 1: isDrifting = true; break; 
        }
        switch (actions.DiscreteActions[3])
        {
            case 0: isUsingItem = false; break;
            case 1: isUsingItem = true; break;
        }


        kartController.SetInputs(forwardAmount, turnAmount, isDrifting, isUsingItem);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        int forwardAction = 0;
        if (Input.GetKey(KeyCode.W)) forwardAction = 1;
        if (Input.GetKey(KeyCode.S)) forwardAction = 2;

        int turnAction = 0;
        if (Input.GetKey(KeyCode.W)) turnAction = 1;
        if (Input.GetKey(KeyCode.S)) turnAction = 2;

        int driftAction = 0;
        if (Input.GetKey(KeyCode.LeftShift)) driftAction = 1;

        int usingItemAction = 0;
        if (Input.GetKeyDown(KeyCode.Space)) usingItemAction = 1;

        ActionSegment<int> discreteActions = actionsOut.DiscreteActions;
        discreteActions[0] = forwardAction;
        discreteActions[1] = turnAction;
        discreteActions[2] = driftAction;
        discreteActions[3] = usingItemAction;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            AddReward(-0.5f);
            EndEpisode();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            AddReward(-0.1f);
        }
    }
}
