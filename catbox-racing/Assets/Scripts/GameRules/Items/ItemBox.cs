using UnityEngine;

public class ItemBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("KartPlayer"))
        {
            if(other.GetComponent<KartItem>().heldItem == -1 && other.GetComponent<KartItem>().canPickup)
            {
                other.GetComponent<KartItem>().StartPickup();
                gameObject.SetActive(false);
            }

            gameObject.SetActive(false);
        }

        if (other.gameObject.CompareTag("KartAgent"))
        {
            gameObject.SetActive(false);
        }
    }
}
