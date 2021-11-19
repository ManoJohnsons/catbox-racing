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
        transform.position = spawnPosition.position;
        transform.forward = spawnPosition.forward;
        trackCheckpoints.ResetCheckpoint(transform);
        kartController.StopCompletely();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        Vector3 checkpointForward = trackCheckpoints.GetNextCheckpoint(transform).forward;
        float directionDot = Vector3.Dot(transform.forward, checkpointForward);
        sensor.AddObservation(directionDot);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float forwardAmount = 0f;
        float turnAmount = 0f;
        bool isDrifting = false;

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

        kartController.SetFowardAmount(forwardAmount);
        kartController.SetTurnAmount(turnAmount);
        kartController.SetDrifting(isDrifting);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            AddReward(-0.5f);
            //EndEpisode();
        }
    }
}
