using System;
using UnityEngine;

public class LapComplete : MonoBehaviour
{
    public event EventHandler OnLapDone;

    [SerializeField] private GameObject startTrigger;
    [SerializeField] private GameObject halfTrigger;
    [SerializeField] private int lapsDone;
    [SerializeField] private int lapsMax;

    private void Update()
    {
        if(lapsDone == lapsMax)
        {
            Debug.Log("Race Finished");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out KartController _))
        {
            startTrigger.SetActive(false);
            halfTrigger.SetActive(true);
        }

        if (other.CompareTag("KartPlayer"))
        {
            lapsDone += 1;
            OnLapDone?.Invoke(this, EventArgs.Empty);
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
