using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCondition : MonoBehaviour
{
    public event EventHandler OnGameContinue;
    public event EventHandler OnGameTryAgain;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameResume;

    [SerializeField] private LapComplete lapComplete;
    private PositionManager positionManager;
    private GameTime gameTime;

    private void Awake()
    {
        positionManager = GetComponent<PositionManager>();
        gameTime = GetComponent<GameTime>();
    }

    public void GameEnd()
    {
        if (lapComplete.GetLapsDone() == lapComplete.GetLapsMax())
        {
            //GameContinue
            if(positionManager.GetCurrentPosition() == 1)
            {
                gameTime.GameStop();
                OnGameContinue?.Invoke(this, EventArgs.Empty);
            }

            //GameOver
            if (positionManager.GetCurrentPosition() == 2 || 
                positionManager.GetCurrentPosition() == 3 || 
                positionManager.GetCurrentPosition() == 4)
            {
                gameTime.GameStop();
                OnGameTryAgain?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public void GamePaused()
    {
        gameTime.GameStop();
        OnGamePaused?.Invoke(this, EventArgs.Empty);
    }

    public void GameResume()
    {
        gameTime.GameResume();
        OnGameResume?.Invoke(this, EventArgs.Empty);
    }

    public void NextScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        gameTime.GameResume();
    }

    public void GameRestart(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        gameTime.GameResume();
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }

    public void CloseGame()
    {
        Application.Quit();
    }
    public bool GetIsPaused()
    {
        return gameTime.GetIsPaused();
    }
}
