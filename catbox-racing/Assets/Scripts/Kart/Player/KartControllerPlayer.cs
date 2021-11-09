using UnityEngine;
using UnityEngine.InputSystem;

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
        //Drift
        playerInputActions.KartMove.Drift.performed += Drift_performed;
    }

    void Update()
    {
        //Movimento
        Vector2 inputVector = playerInputActions.KartMove.Movement.ReadValue<Vector2>();
        float fowardAmount = inputVector.y;
        float turnAmount = inputVector.x;
        kartController.SetFowardAmount(fowardAmount);
        kartController.SetTurnAmount(turnAmount);

        //Usando Item
        bool isUsingItem = playerInputActions.KartMove.UseItem.triggered;

        //Setters
        kartItem.SetInput(isUsingItem);
    }

    private void Drift_performed(InputAction.CallbackContext context)
    {
        bool isDrifting;

        if (context.performed)
        {
            isDrifting = true;
            kartController.SetDrifting(isDrifting);
        }
    }
}
