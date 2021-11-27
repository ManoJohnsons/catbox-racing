using UnityEngine;
using UnityEngine.InputSystem;

public class KartControllerPlayer : MonoBehaviour
{
    [SerializeField] private GameCondition gameCondition;
    [SerializeField] private Transform spawnPosition;
    private KartController kartController;
    private KartItem kartItem;
    private PlayerInputActions playerInputActions;

    void Awake()
    {
        kartController = GetComponent<KartController>();
        kartItem = GetComponent<KartItem>();

        playerInputActions = new PlayerInputActions();
        playerInputActions.KartMove.Enable();

        //Pause
        playerInputActions.KartMove.Pause.performed += _ => PauseCondition();
        playerInputActions.PlayerUI.Resume.performed += _ => PauseCondition();

        gameObject.transform.position = spawnPosition.position;
    }

    private void PauseCondition()
    {
        if (gameCondition.GetIsPaused())
        {
            gameCondition.GameResume();
            playerInputActions.KartMove.Enable();
            playerInputActions.PlayerUI.Disable();
        }
        else
        {
            gameCondition.GamePaused();
            playerInputActions.PlayerUI.Enable();
            playerInputActions.KartMove.Disable();
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

        //Setters
        kartItem.SetInput(isUsingItem);
    }
}
