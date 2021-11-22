using System;
using UnityEngine;

public class LapComplete : MonoBehaviour
{
    public EventHandler OnLapDone;
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
        if (other.CompareTag("KartPlayer"))
        {
            lapsDone += 1;
            /* Terminar isso aqui
             * OnLapDone? EventHandler temp = MyEvent;
            if (temp != null)
            {
                temp();
            }*/
            startTrigger.SetActive(false);
            halfTrigger.SetActive(true);
        }
    }
}
