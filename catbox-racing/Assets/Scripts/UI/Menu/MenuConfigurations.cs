using UnityEngine;

public class MenuConfigurations : MonoBehaviour
{
    [SerializeField] private GameObject[] screens;

    public void GeneralConfigClicked()
    {
        screens[0].SetActive(true);
        screens[1].SetActive(false);
        screens[2].SetActive(false);
    }

    public void GraphicsConfigClicked()
    {
        screens[0].SetActive(false);
        screens[1].SetActive(true);
        screens[2].SetActive(false);
    }

    public void AudioConfigClicked()
    {
        screens[0].SetActive(false);
        screens[1].SetActive(false);
        screens[2].SetActive(true);
    }
}
