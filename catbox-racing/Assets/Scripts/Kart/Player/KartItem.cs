using System.Collections;
using UnityEngine;

public class KartItem : MonoBehaviour
{
    public ItemPowerUp itemUse;
    public int heldItem;
    public bool canPickup;

    private ItemsHandler handler;
    private int remainingItemUses;
    private float delayBeforeItemPickup = 1;
    private bool useItem;

    private void Awake()
    {
        handler = GameObject.FindGameObjectWithTag("GameController").GetComponent<ItemsHandler>();
    }

    void Start()
    {
        ResetItem();
    }

    void Update()
    {
        if(useItem && heldItem != -1)
            ActivateItem();
    }

    void ResetItem()
    {
        itemUse = null;
        heldItem = -1;
        canPickup = true;
    }

    private IEnumerator Pickup()
    {
        if(heldItem == -1 && canPickup)
        {
            canPickup = false;

            yield return new WaitForSeconds(delayBeforeItemPickup);
            //mostra o item na tela e deixa o item para ser usado

            int itemRandom = Random.Range(0, handler.AllItems.Length);

            itemUse = handler.AllItems[itemRandom];

            heldItem = itemRandom;
            remainingItemUses = itemUse.itemUses;
        }
    }

    void ActivateItem()
    {
        remainingItemUses -= 1;
        itemUse.Activate(gameObject);

        if(remainingItemUses <= 0)
            ResetItem();      
    }

    public void StartPickup()
    {
        StartCoroutine(Pickup());
    }

    public void SetInput(bool isUsingItem)
    {
        this.useItem = isUsingItem;
    }
}
