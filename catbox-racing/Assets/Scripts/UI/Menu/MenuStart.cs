using UnityEngine;

public class MenuStart : MonoBehaviour
{
    private void Awake()
    {
        Time.timeScale = 1;
    }

    private void Start()
    {
        FindObjectOfType<AudioManager>().Play("MenuMusic");
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
