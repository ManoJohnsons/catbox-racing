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
    private string trackNameSong;
    private PositionManager positionManager;
    private GameTime gameTime;

    private void Awake()
    {
        positionManager = GetComponent<PositionManager>();
        gameTime = GetComponent<GameTime>();
        trackNameSong = GetComponent<Countdown>().nameTrackMusic;
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
        FindObjectOfType<AudioManager>().Stop(trackNameSong);
        AudioListener.pause = false;
        SceneManager.LoadScene(sceneName);
        //gameTime.GameResume();
    }

    public void GameRestart(string sceneName)
    {
        FindObjectOfType<AudioManager>().Stop(trackNameSong);
        AudioListener.pause = false;
        SceneManager.LoadScene(sceneName);
        gameTime.GameResume();
    }

    public void LoadMenu()
    {
        FindObjectOfType<AudioManager>().Stop(trackNameSong);
        AudioListener.pause = false;
        FindObjectOfType<AudioManager>().Play("MenuMusic");
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
