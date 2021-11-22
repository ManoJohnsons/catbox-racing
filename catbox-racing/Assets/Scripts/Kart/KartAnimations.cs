using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KartAnimations : MonoBehaviour
{
    private KartController kartController;
    [SerializeField] private GameObject WheelsModels;
    [SerializeField] private GameObject FrontWheeModels;

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
        float kartFowardAmount = kartController.GetFowardAmount();
        float kartTurnAmount = kartController.GetTurnAmount();

        if(kartTurnAmount > 0)
        {
            //FrontWheeModels.transform.eulerAngles = Vector3.Lerp()
        }
    }
}
