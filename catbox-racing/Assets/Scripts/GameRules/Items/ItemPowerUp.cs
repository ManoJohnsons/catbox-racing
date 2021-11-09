using UnityEngine;

public class ItemPowerUp : ScriptableObject
{
    public new string name;
    public string description;
    public int itemUses;
    public Sprite image;

    public virtual void Activate(GameObject parent){ }
}
