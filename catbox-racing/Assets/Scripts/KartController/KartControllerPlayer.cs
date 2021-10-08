using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KartControllerPlayer : MonoBehaviour
{
    private KartController kartController;

    void Awake()
    {
        kartController = GetComponent<KartController>();
    }

    // Update is called once per frame
    void Update()
    {
        float fowardAmount = Input.GetAxis("Vertical");
        float turnAmount = Input.GetAxis("Horizontal");
        bool isDrifiting = Input.GetKey(KeyCode.LeftShift);
        bool isUsingItem = Input.GetKeyDown(KeyCode.Space);

        kartController.SetInputs(fowardAmount, turnAmount, isDrifiting, isUsingItem);
    }
}
