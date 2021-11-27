using UnityEngine;
using UnityEngine.UI;

public class ItemHandlerUI : MonoBehaviour
{
    public Sprite emptyItem;
    public Image imageChange;
    public KartItem kart;

    // Start is called before the first frame update
    void Start()
    {
        if (kart == null)
            return;
    }

    // Update is called once per frame
    void Update()
    {
        if (kart.heldItem == -1)
        {
            if (kart.canPickup)            
                imageChange.sprite = emptyItem;            
        }
        else       
            imageChange.sprite = kart.itemUse.image;
    }
}
