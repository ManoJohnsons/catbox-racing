using System;
using UnityEngine;

public class LapComplete : MonoBehaviour
{
    public event EventHandler OnKartLapDone;
    public event EventHandler OnPlayerLapDone;

    [SerializeField] private GameCondition gameCondition;
    [SerializeField] private int lapsDone;
    [SerializeField] private int lapsMax;

    private void Update()
    {
        gameCondition.GameEnd();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out KartController _))
        {
            OnKartLapDone?.Invoke(this, EventArgs.Empty);
        }

        if (other.CompareTag("KartPlayer"))
        {
            lapsDone += 1;
            OnPlayerLapDone?.Invoke(this, EventArgs.Empty);
        }
    }

    public int GetLapsDone()
    {
        return lapsDone;
    }

    public int GetLapsMax()
    {
        return lapsMax;
    }
}
