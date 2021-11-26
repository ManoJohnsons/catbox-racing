using UnityEngine;
using UnityEngine.InputSystem;

public class KartControllerPlayer : MonoBehaviour
{
    [SerializeField] private GameCondition gameCondition;
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
        playerInputActions.KartMove.Drift.canceled += Drift_canceled;

        //Pause
        playerInputActions.KartMove.Pause.performed += _ => PauseCondition();
    }

    private void PauseCondition()
    {
        if (gameCondition.GetIsPaused())
            gameCondition.GameResume();
        else
            gameCondition.GamePaused();
    }

    private void Drift_canceled(InputAction.CallbackContext context)
    {
        bool isDrifting;

        if (context.canceled)
        {
            isDrifting = false;
            kartController.SetDrifting(isDrifting);
        }
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

        //Pausando


        //Setters
        kartItem.SetInput(isUsingItem);
    }
}
