using System.Collections;
using UnityEngine;

public class KartController : MonoBehaviour
{
    #region "Variáveis"
    private Rigidbody rb;
    private float gravity = 35;

    //Valor dos Inputs
    private float fowardAmount;
    private float turnAmount;
    private bool isDrifting;

    //Função Move();
    [Header("Função Move")]
    [SerializeField] private float acceleration = 30f;
    [SerializeField] private float steering = 80f;
    private float speed, currentSpeed = 0;
    private float rotate, currentRotate;

    //Função GroundRotation();
    [SerializeField] private Transform kartModel;

    //Função Steer();
    private float steerDirection;

    //Função Drift();

    //Função Boost();


    #endregion

    #region "Funções do MonoBehaviour"
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        //Gravidade
        rb.AddForce(Vector3.down * gravity, ForceMode.Acceleration);

        //Accelerate the kart
        rb.AddForce(transform.forward * currentSpeed, ForceMode.Acceleration);

        //Steering the kart
        transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(0, transform.eulerAngles.y + currentRotate, 0), Time.deltaTime * 5f);

        Move(fowardAmount);
        Steer(turnAmount);
        Drift(isDrifting, turnAmount);
        GroundRotation();
        Boost();
    }
    #endregion

    #region "Ações do Kart"
    private void MoveForward(float fowardAmount)
    {
        if (fowardAmount != 0)
        {
            int direction = fowardAmount > 0 ? 1 : -1;
            //continua aqiu meu parsero
        }
    }

    private void Steering(float turnAmount)
    {
        if(turnAmount != 0)
        {
            int direction = turnAmount > 0 ? 1 : -1;
            float amount = Mathf.Abs(turnAmount);
            Steer(direction, amount);
        }
    }

    private void Steer(int direction, float amount)
    {

        rotate = (steering * direction) * amount;
    }

    private void Drift(bool isDrifting, float driftTurnAmount)
    {
       
    }

    private void GroundRotation()
    {
        RaycastHit hit;
        float raycastDistance = 1.5f;
        if (Physics.Raycast(transform.position, -transform.up, out hit, raycastDistance))
        {
            kartModel.up = Vector3.Lerp(kartModel.up, hit.normal, Time.deltaTime * 7.5f);
            kartModel.Rotate(0, transform.eulerAngles.y, 0);
        }
    }

    public void Boost()
    {
        
    }

    #endregion

    #region "Setters"

    public void SetFowardAmount(float fowardAmount)
    {
        this.fowardAmount = fowardAmount;
    }

    public void SetTurnAmount(float turnAmount)
    {
        this.turnAmount = turnAmount;
    }

    public void SetDrifting(bool isDrifting)
    {
        this.isDrifting = isDrifting;
    }

    #endregion

    #region "Controle da IA"
    public void StopCompletely()
    {
        currentSpeed = 0;
    }
    #endregion
}
