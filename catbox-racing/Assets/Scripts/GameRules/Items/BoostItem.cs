using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BoostItem : ItemPowerUp
{
    public override void Activate(GameObject parent)
    {
        parent.GetComponent<KartController>().Boost();
    }
}
