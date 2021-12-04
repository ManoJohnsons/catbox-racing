using UnityEngine;

public class MenuStart : MonoBehaviour
{
    private void Awake()
    {
        Time.timeScale = 1;
    }
    public void CloseGame()
    {
        Application.Quit();
    }
}
