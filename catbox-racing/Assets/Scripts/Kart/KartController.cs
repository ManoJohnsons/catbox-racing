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
    [SerializeField] private float reverseSpeed;
    [SerializeField] private float maxSpeed;
    private float currentSpeed = 0;
    private float realSpeed;

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

        Move(fowardAmount);
        Steer(turnAmount);
        Drift(isDrifting, turnAmount);
        GroundRotation();
        Boost();
    }
    #endregion

    #region "Ações do Kart"
    private void Move(float fowardAmount)
    {
        realSpeed = transform.InverseTransformDirection(rb.velocity).z;

        if (fowardAmount > 0)
            currentSpeed = Mathf.Lerp(currentSpeed, maxSpeed, Time.deltaTime * 0.5f);
        else if (fowardAmount < 0)
            currentSpeed = Mathf.Lerp(currentSpeed, -maxSpeed / reverseSpeed, 1f * Time.deltaTime);
        else
            currentSpeed = Mathf.Lerp(currentSpeed, 0, Time.deltaTime * 1.5f);

        Vector3 velocity = transform.forward * currentSpeed;
        velocity.y = rb.velocity.y;
        rb.velocity = velocity;
    }

    private void Steer(float turnAmount)
    {
        steerDirection = turnAmount;
        float steerSpeed = 3f;
        float steerMinStrength = 4f;
        float steerMaxStrength = 1.5f;
        Vector3 steerDirectionVector;
        float steerAmount;

        steerAmount = realSpeed > 30 ? realSpeed / steerMinStrength * steerDirection : realSpeed / steerMaxStrength * steerDirection;

        steerDirectionVector = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + steerAmount, transform.eulerAngles.z);
        transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, steerDirectionVector, steerSpeed * Time.deltaTime);
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

    //Controle da IA
    public void StopCompletely()
    {
        currentSpeed = 0;
    }
}
