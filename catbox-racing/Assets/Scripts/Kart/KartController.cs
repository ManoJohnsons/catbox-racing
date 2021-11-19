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
    [SerializeField] private bool drifting;
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

    void Update()
    {
        MoveForward(fowardAmount);
        Steering(turnAmount);

        currentSpeed = Mathf.SmoothStep(currentSpeed, speed, Time.deltaTime * 12f); speed = 0f;
        currentRotate = Mathf.Lerp(currentRotate, rotate, Time.deltaTime * 4f); rotate = 0f;
    }

    void FixedUpdate()
    {
        //Gravidade
        rb.AddForce(Vector3.down * gravity, ForceMode.Acceleration);

        //Acelera o kart
        if (!drifting)
            rb.AddForce(-kartModel.transform.right * currentSpeed, ForceMode.Acceleration);
        else
            rb.AddForce(transform.forward * currentSpeed, ForceMode.Acceleration);

        //Vira o kart
        transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(0, transform.eulerAngles.y + currentRotate, 0), Time.deltaTime * 5f);

        GroundRotation();
    }
    #endregion

    #region "Ações do Kart"
    private void MoveForward(float fowardAmount)
    {
        if (fowardAmount > 0)
        {
            speed = acceleration;
        }
        else if(fowardAmount < 0)
        {
            speed = -acceleration;
        }
<<<<<<< HEAD
        steerAmount = realSpeed > 30 ? realSpeed / steerMinStrength * steerDirection : realSpeed / steerMaxStrength * steerDirection;

        steerDirectionVector = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + steerAmount, transform.eulerAngles.z);
        transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, steerDirectionVector, steerSpeed * Time.deltaTime);
=======
>>>>>>> parent of 9f3269d (Kart Controller)
    }

    private void Drift(bool isDrifting, float driftTurnAmount)
    {
        //continua
    }

<<<<<<< HEAD
        if (isDrifting && isGrounded && currentSpeed > 40 && driftTurnAmount != 0)       
            driftTime += Time.deltaTime;
=======
    private void Steering(float turnAmount)
    {
        if(turnAmount != 0)
        {
            int direction = turnAmount > 0 ? 1 : -1;
            float amount = Mathf.Abs(turnAmount);
            Steer(direction, amount);
        }
    }
>>>>>>> parent of 9f3269d (Kart Controller)

    private void Steer(int direction, float amount)
    {

        rotate = (steering * direction) * amount;
    }



    private void GroundRotation()
    {
        RaycastHit hit;
        float raycastDistance = 1.5f;
        if (Physics.Raycast(transform.position, -transform.up, out hit, raycastDistance))
        {
            kartModel.up = Vector3.Lerp(kartModel.up, hit.normal, Time.deltaTime * 7.5f);
            kartModel.Rotate(0, transform.eulerAngles.y, 0);
<<<<<<< HEAD
            isGrounded = true;
        }
        else      
            isGrounded = false;
=======
        }
>>>>>>> parent of 9f3269d (Kart Controller)
    }

    public void Boost()
    {
<<<<<<< HEAD
        boostTime -= Time.deltaTime;
        if (boostTime > 0)
        {
            maxSpeed = boostSpeed;
            currentSpeed = Mathf.Lerp(currentSpeed, maxSpeed, 1 * Time.deltaTime);
        }
        else       
            maxSpeed = boostSpeed - diferenceBetweenMaxSpeedAndBoostSpeed;        
=======
        
>>>>>>> parent of 9f3269d (Kart Controller)
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
