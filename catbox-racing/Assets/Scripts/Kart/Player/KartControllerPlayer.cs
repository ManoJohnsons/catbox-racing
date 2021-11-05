using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KartControllerPlayer : MonoBehaviour
{
    private KartController kartController;
    private KartItem kartItem;
    private PlayerInputActions playerInputActions;

    void Awake()
    {
        kartController = GetComponent<KartController>();
        kartItem = GetComponent<KartItem>();

        playerInputActions = new PlayerInputActions();
        playerInputActions.KartMove.Enable();
    }

    void Update()
    {
        //Movimento
        Vector2 inputVector = playerInputActions.KartMove.Movement.ReadValue<Vector2>();
        float fowardAmount = inputVector.y;
        float turnAmount = inputVector.x;

        //Drift
        bool isDrifiting = playerInputActions.KartMove.Drift.triggered;

        //Usando Item
        bool isUsingItem = playerInputActions.KartMove.UseItem.triggered;

        //Setters
        kartController.SetInputs(fowardAmount, turnAmount, isDrifiting);
        kartItem.SetInput(isUsingItem);
    }
}
