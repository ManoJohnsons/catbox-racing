using System;
using UnityEngine;

public class KartController : MonoBehaviour
{
    #region "Eventos"
    public event EventHandler OnWheelRotate;
    #endregion

    #region "Variáveis"
    private Rigidbody rb;
    private float gravity = 75;

    //Valor dos Inputs
    private float fowardAmount;
    private float turnAmount;

    //Função Move();
    [Header("Função Move")]
    [SerializeField] private float reverseSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float boostSpeed;
    private float currentSpeed = 0;
    private float realSpeed;

    //Funções Steer(), Drift() e GroundRotation();
    [Header("Função Ground Rotation")]
    [SerializeField] private Transform kartModel;
    private float steerDirection;
    #endregion

    #region "Funções do MonoBehaviour"
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        //gravity
        rb.AddForce(Vector3.down * gravity, ForceMode.Acceleration);

        Move(fowardAmount);
        Steer(turnAmount);
        OnWheelRotate?.Invoke(this, EventArgs.Empty);
        GroundRotation();
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
        Vector3 steerDirectionVector;
        float steerSpeed = 3f;
        float steerMinStrength = 4f;
        float steerMaxStrength = 1.5f;
        float steerAmount;

        steerAmount = realSpeed > 30 ? realSpeed / steerMinStrength * steerDirection : realSpeed / steerMaxStrength * steerDirection;

        steerDirectionVector = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + steerAmount, transform.eulerAngles.z);
        transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, steerDirectionVector, steerSpeed * Time.deltaTime);
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
        currentSpeed = currentSpeed * 2;
    }
    #endregion

    #region "Setters e Getters"
    public void SetFowardAmount(float fowardAmount)
    {
        this.fowardAmount = fowardAmount;
    }

    public void SetTurnAmount(float turnAmount)
    {
        this.turnAmount = turnAmount;
    }

    public float GetTurnAmount()
    {
        return turnAmount;
    }

    public float GetCurrentSpeed()
    {
        return currentSpeed;
    }

    public float GetRealSpeed()
    {
        return realSpeed;
    }
    #endregion

    #region "Controle da IA"
    public void StopCompletely()
    {
        currentSpeed = 0;
    }
    #endregion
}
