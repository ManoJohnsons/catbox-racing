using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LapController : MonoBehaviour
{
    public event EventHandler OnLapIncrement;

    [SerializeField] private int maxLap;
    private int currentLap = 1;

    private void Update()
    {
        RaceOver();
    }

    public void IncrementLap()
    {
        if(currentLap <= maxLap)
        {
            currentLap++;
            OnLapIncrement?.Invoke(this, EventArgs.Empty);
        }
    }

    public void RaceOver()
    {
        if(currentLap > maxLap)
        {
            //Acaba a corrida
            Debug.Log("Acabou a corrida");
        }
    }

    public int GetCurrentLap()
    {
        return currentLap;
    }
}
