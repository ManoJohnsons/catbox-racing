using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KartItem : MonoBehaviour
{
    private ItemsHandler handler;

    private float delayBeforeItemPickup = 1;

    public int heldItem;

    public bool canPickup;
    private bool useItem;

    public ItemPowerUp itemUse;
    private int remainingItemUses;

    void Start()
    {
        handler = GameObject.FindGameObjectWithTag("GameController").GetComponent<ItemsHandler>();

        ResetItem();
    }

    void Update()
    {
        useItem = Input.GetKeyDown(KeyCode.Space);
        if(useItem && heldItem != -1)
        {
            ActivateItem();
        }
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
        {
            ResetItem();
        }
    }

    public void StartPickup()
    {
        StartCoroutine(Pickup());
    }
}
