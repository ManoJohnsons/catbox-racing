using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    #region "Variáveis"
    private Rigidbody rb;

    //Função Move();
    private float currentSpeed = 0;
    private float realSpeed;
    private float diferenceBetweenMaxSpeedAndBoostSpeed;
    [SerializeField] private float reverseSpeed; 
    [SerializeField] private float maxSpeed;
    [SerializeField] private float boostSpeed;

    //Funções Steer(), Drift(), e GroundRotation();
    private float steerDirection;
    private float driftTime;
    private bool driftLeft;
    private bool driftRight;
    private float outwardDriftForce = 5000;
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
        Move();
        Steer();
        GroundRotation();
        Drift();
        Boost();
    }
    #endregion

    #region "Movimentação do Player"
    private void Move()
    {
        realSpeed = transform.InverseTransformDirection(rb.velocity).z;

        if (Input.GetKey(KeyCode.W))
            currentSpeed = Mathf.Lerp(currentSpeed, maxSpeed, Time.deltaTime * 0.5f);
        else if (Input.GetKey(KeyCode.S))
            currentSpeed = Mathf.Lerp(currentSpeed, -maxSpeed / reverseSpeed, 1f * Time.deltaTime);
        else
            currentSpeed = Mathf.Lerp(currentSpeed, 0, Time.deltaTime * 1.5f);

        Vector3 velocity = transform.forward * currentSpeed;
        velocity.y = rb.velocity.y;
        rb.velocity = velocity;
    }

    private void Steer()
    {
        steerDirection = Input.GetAxisRaw("Horizontal");
        float steerSpeed = 3f;
        float steerMinStrength = 4f;
        float steerMaxStrength = 1.5f;
        Vector3 steerDirectionVector;
        float steerAmount;

        //Drift
        if (driftLeft && !driftRight)
        {
            steerDirection = Input.GetAxis("Horizontal") < 0 ? -1.5f : -.5f;

            if (isGrounded)
                rb.AddForce(transform.right * outwardDriftForce * Time.deltaTime, ForceMode.Acceleration);
        } 
        else if (driftRight && !driftLeft)
        {
            steerDirection = Input.GetAxis("Horizontal") > 0 ? 1.5f : .5f;

            if (isGrounded)
                rb.AddForce(transform.right * -outwardDriftForce * Time.deltaTime, ForceMode.Acceleration);
        }
        
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
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.FromToRotation(transform.up * 2, hit.normal) * transform.rotation, 7.5f * Time.deltaTime);
            isGrounded = true;
        } 
        else
        {
            isGrounded = false;
        }
    }

    private void Drift()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift) && isGrounded)
        {
            if(steerDirection > 0)
            {
                driftRight = true;
                driftLeft = false;
            }
            else if(steerDirection < 0)
            {
                driftRight = false;
                driftLeft = true;
            }
        }

        if(Input.GetKey(KeyCode.LeftShift) && isGrounded && currentSpeed > 40 && Input.GetAxis("Horizontal") != 0)
        {
            driftTime += Time.deltaTime;
        }

        if(Input.GetKey(KeyCode.LeftShift) || realSpeed < 40)
        {
            driftLeft = false;
            driftRight = false;

            if(driftTime > 1.5f && driftTime < 4)
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

    private void Boost()
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
}
