using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KartController : MonoBehaviour
{
    #region "Variáveis"
    private Rigidbody rb;
    private float gravity = 35;

    //Valor dos Inputs
    private float fowardAmount;
    private float turnAmount;
    private bool isDrifiting;

    //Função Move();
    [Header("Função Move")]
    [SerializeField] private float reverseSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float boostSpeed;
    private float currentSpeed = 0;
    private float realSpeed;
    private float diferenceBetweenMaxSpeedAndBoostSpeed;

    //Funções Steer(), Drift() e GroundRotation();
    [SerializeField] private Transform kartModel;
    private float outwardDriftForce = 5000;
    private float steerDirection;
    private float driftTime;
    private bool driftLeft;
    private bool driftRight;
    private bool isGrounded;

    //Função Boost();
    private float boostTime = 0;

    #endregion

    #region "Funções do MonoBehaviour"
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        diferenceBetweenMaxSpeedAndBoostSpeed = boostSpeed - maxSpeed;
    }

    void FixedUpdate()
    {
        //gravity
        rb.AddForce(Vector3.down * gravity, ForceMode.Acceleration);

        Move(fowardAmount);
        Steer(turnAmount);
        Drift(isDrifiting, turnAmount);
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

        //Drift
        if (driftLeft && !driftRight)
        {
            steerDirection = turnAmount < 0 ? -1.5f : -.5f;

            if (isGrounded)
                rb.AddForce(transform.right * outwardDriftForce * Time.deltaTime, ForceMode.Acceleration);
        } 
        else if (driftRight && !driftLeft)
        {
            steerDirection = turnAmount > 0 ? 1.5f : .5f;

            if (isGrounded)
                rb.AddForce(transform.right * -outwardDriftForce * Time.deltaTime, ForceMode.Acceleration);
        }
        
        steerAmount = realSpeed > 30 ? realSpeed / steerMinStrength * steerDirection : realSpeed / steerMaxStrength * steerDirection;
        
        steerDirectionVector = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + steerAmount, transform.eulerAngles.z);
        transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, steerDirectionVector, steerSpeed * Time.deltaTime);
    }

    private void Drift(bool isDrifiting, float driftAmount)
    {
        if (isDrifiting && isGrounded)
        {
            if (steerDirection > 0)
            {
                driftRight = true;
                driftLeft = false;
            }
            else if (steerDirection < 0)
            {
                driftRight = false;
                driftLeft = true;
            }
        }

        if (isDrifiting && isGrounded && currentSpeed > 40 && driftAmount != 0)
        {
            driftTime += Time.deltaTime;
        }

        if (isDrifiting || realSpeed < 40)
        {
            driftLeft = false;
            driftRight = false;

            if (driftTime > 1.5f && driftTime < 4)
            {
                boostTime = .75f;
            }
            if (driftTime >= 4f && driftTime < 7)
            {
                boostTime = 1.5f;
            }
            if (driftTime > 1.5f && driftTime < 4)
            {
                boostTime = 2.5f;
            }

            driftTime = 0;
        }
    }

    private void GroundRotation()
    {
        RaycastHit hit;
        float raycastDistance = 1.5f;
        if (Physics.Raycast(transform.position, -transform.up, out hit, raycastDistance))
        {
            kartModel.up = Vector3.Lerp(kartModel.up, hit.normal, Time.deltaTime * 7.5f);
            kartModel.Rotate(0, transform.eulerAngles.y, 0);
            isGrounded = true;
        } 
        else
        {
            isGrounded = false;
        }
    }

    public void Boost()
    {
        boostTime -= Time.deltaTime;
        if(boostTime > 0)
        {
            maxSpeed = boostSpeed;
            currentSpeed = Mathf.Lerp(currentSpeed, maxSpeed, 1 * Time.deltaTime);
        }
        else
        {
            maxSpeed = boostSpeed - diferenceBetweenMaxSpeedAndBoostSpeed;
        }
    }

    #endregion

    #region "Setters"

    public void SetInputs(float fowardAmount, float turnAmount, bool isDrifiting)
    {
        this.fowardAmount = fowardAmount;
        this.turnAmount = turnAmount;
        this.isDrifiting = isDrifiting;
    }

    public void StopCompletely()
    {
        currentSpeed = 0;
    }
    #endregion
}
