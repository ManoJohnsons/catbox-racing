using UnityEngine;

public class GameTime : MonoBehaviour
{
    public void GameStop()
    {
        Time.timeScale = 0;
    }

    public void GameResume()
    {
        Time.timeScale = 1;
    }
}
