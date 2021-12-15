using UnityEngine;

public class GameTime : MonoBehaviour
{
    private bool isPaused = false;

    public void GameStop()
    {
        Time.timeScale = 0;
        isPaused = true;
    }

    public void GameResume()
    {
        Time.timeScale = 1;
        isPaused = false;
    }

    public bool GetIsPaused()
    {
        return isPaused;
    }
}
