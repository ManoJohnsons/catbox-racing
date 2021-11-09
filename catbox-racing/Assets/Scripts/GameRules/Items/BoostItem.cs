using UnityEngine;

[CreateAssetMenu]
public class BoostItem : ItemPowerUp
{
    public override void Activate(GameObject parent)
    {
        parent.GetComponent<KartController>().Boost();
    }
}
