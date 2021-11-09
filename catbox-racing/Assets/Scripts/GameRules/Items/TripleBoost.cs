using UnityEngine;

[CreateAssetMenu]
public class TripleBoost : ItemPowerUp
{
    public override void Activate(GameObject parent)
    {
        parent.GetComponent<KartController>().Boost();
    }
}
