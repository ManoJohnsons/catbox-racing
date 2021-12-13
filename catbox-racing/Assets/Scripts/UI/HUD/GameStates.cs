using UnityEngine;

public class GameStates : MonoBehaviour
{
    [SerializeField] private GameCondition gameCondition;
    [SerializeField] private GameObject continueScreen;
    [SerializeField] private GameObject tryAgainScreen;
    [SerializeField] private GameObject pausedScreen;

    private void Awake()
    {
        if (pausedScreen == null)
            return;

        gameCondition.OnGameContinue += GameCondition_OnGameContinue;
        gameCondition.OnGameTryAgain += GameCondition_OnGameTryAgain;
        gameCondition.OnGamePaused += GameCondition_OnGamePaused;
        gameCondition.OnGameResume += GameCondition_OnGameResume;
    }

    private void GameCondition_OnGameResume(object sender, System.EventArgs e)
    {
        pausedScreen.SetActive(false);
        AudioListener.pause = false;
    }

    private void GameCondition_OnGamePaused(object sender, System.EventArgs e)
    {
        pausedScreen.SetActive(true);
        AudioListener.pause = true;
    }

    private void GameCondition_OnGameTryAgain(object sender, System.EventArgs e)
    {
        tryAgainScreen.SetActive(true);
    }

    private void GameCondition_OnGameContinue(object sender, System.EventArgs e)
    {
        continueScreen.SetActive(true);
    }
}
