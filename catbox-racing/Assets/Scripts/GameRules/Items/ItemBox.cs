using UnityEngine;
using System.Collections;

public class ItemBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("KartPlayer"))
        {
            if(other.GetComponent<KartItem>().heldItem == -1 && other.GetComponent<KartItem>().canPickup)
            {
                other.GetComponent<KartItem>().StartPickup();
                gameObject.GetComponent<SphereCollider>().enabled = false;
                gameObject.GetComponent<MeshRenderer>().enabled = false;
                StartCoroutine(RespawningItem());
            }

            gameObject.GetComponent<SphereCollider>().enabled = false;
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            StartCoroutine(RespawningItem());
        }

        if (other.gameObject.CompareTag("KartAgent"))
        {
            gameObject.GetComponent<SphereCollider>().enabled = false;
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            StartCoroutine(RespawningItem());
        }
    }

    IEnumerator RespawningItem()
    {
        yield return new WaitForSeconds(2.5f);
        gameObject.GetComponent<SphereCollider>().enabled = true;
        gameObject.GetComponent<MeshRenderer>().enabled = true;
    }
}
