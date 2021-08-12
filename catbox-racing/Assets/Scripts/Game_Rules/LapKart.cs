using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LapKart : MonoBehaviour
{
    public int lapNumber;
    public int checkpointIndex;

    private void Start()
    {
        lapNumber = 1;
        checkpointIndex = 0;
    }
}
