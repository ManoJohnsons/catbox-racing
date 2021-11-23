using UnityEngine;

public class KartAnimations : MonoBehaviour
{
    private KartController kartController;
    [SerializeField] private Transform backWheelRight;
    [SerializeField] private Transform backWheelLeft;
    [SerializeField] private Transform frontWheelRight;
    [SerializeField] private Transform frontWheelLeft;

    private void Awake()
    {
        kartController = GetComponent<KartController>();
    }

    void Start()
    {
        kartController.OnWheelRotate += KartController_OnWheelRotate;
    }

    private void KartController_OnWheelRotate(object sender, System.EventArgs e)
    {
        float kartTurnAmount = kartController.GetTurnAmount();
        float kartCurrentSpeed = kartController.GetCurrentSpeed();
        float kartRealSpeed = kartController.GetRealSpeed();

        if(kartTurnAmount > 0)
        {
            frontWheelLeft.localEulerAngles = Vector3.Lerp(frontWheelLeft.localEulerAngles, new Vector3(0, 205, 0), 5 * Time.deltaTime);
            frontWheelRight.localEulerAngles = Vector3.Lerp(frontWheelRight.localEulerAngles, new Vector3(0, 205, 0), 5 * Time.deltaTime);
        } 
        else if(kartTurnAmount < 0)
        {
            frontWheelLeft.localEulerAngles = Vector3.Lerp(frontWheelLeft.localEulerAngles, new Vector3(0, 155, 0), 5 * Time.deltaTime);
            frontWheelRight.localEulerAngles = Vector3.Lerp(frontWheelRight.localEulerAngles, new Vector3(0, 155, 0), 5 * Time.deltaTime);
        }
        else
        {
            frontWheelLeft.localEulerAngles = Vector3.Lerp(frontWheelLeft.localEulerAngles, new Vector3(0, 180, 0), 5 * Time.deltaTime);
            frontWheelRight.localEulerAngles = Vector3.Lerp(frontWheelRight.localEulerAngles, new Vector3(0, 180, 0), 5 * Time.deltaTime);
        }

        if(kartCurrentSpeed > 30)
        {
            frontWheelLeft.GetChild(0).Rotate(-90 * Time.deltaTime * kartCurrentSpeed * .5f, 0, 0);
            frontWheelRight.GetChild(0).Rotate(-90 * Time.deltaTime * kartCurrentSpeed * .5f, 0, 0);
            backWheelLeft.Rotate(90 * Time.deltaTime * kartCurrentSpeed * .5f, 0, 0);
            backWheelRight.Rotate(90 * Time.deltaTime * kartCurrentSpeed * .5f, 0, 0);
        }
        else
        {
            frontWheelLeft.GetChild(0).Rotate(-90 * Time.deltaTime * kartRealSpeed * .5f, 0, 0);
            frontWheelRight.GetChild(0).Rotate(-90 * Time.deltaTime * kartRealSpeed * .5f, 0, 0);
            backWheelLeft.Rotate(90 * Time.deltaTime * kartRealSpeed * .5f, 0, 0);
            backWheelRight.Rotate(90 * Time.deltaTime * kartRealSpeed * .5f, 0, 0);
        }
    }
}
