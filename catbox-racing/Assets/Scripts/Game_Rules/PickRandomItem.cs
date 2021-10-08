using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickRandomItem : MonoBehaviour
{
    [SerializeField] private GameObject[] itemsToPickFrom;

    void Update()
    {
        Pick();
    }

    private void Pick()
    {
        int randomIndex = Random.Range(0, itemsToPickFrom.Length);
        //Apartir daqui começa a lógica de escolher o item sorteado.
    }
}
