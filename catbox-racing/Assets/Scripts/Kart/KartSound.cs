using UnityEngine;

public class KartSound : MonoBehaviour
{
    private AudioSource audioSource;
    private KartController kartController;
    [SerializeField] private float minPitch = 0.05f;
    private float pitchFromKart;

    // Start is called before the first frame update
    void Awake()
    {
        kartController = GetComponent<KartController>();
        audioSource = GetComponent<AudioSource>();
        audioSource.pitch = minPitch;
    }

    // Update is called once per frame
    void Update()
    {
        pitchFromKart = kartController.GetRealSpeed() / 30;
        if (pitchFromKart < minPitch)
            audioSource.pitch = minPitch;
        else
            audioSource.pitch = pitchFromKart;
    }
}
