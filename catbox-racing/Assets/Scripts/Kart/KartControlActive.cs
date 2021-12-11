using UnityEngine;

public class KartControlActive : MonoBehaviour
{
    [SerializeField] private KartController[] kartsControllers;

    void Start()
    {
        foreach (var kart in kartsControllers)
        {
            kart.enabled = true;
            kart.GetComponent<AudioSource>().Play();
        }
    }
}
