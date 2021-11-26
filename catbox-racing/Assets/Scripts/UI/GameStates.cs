using UnityEngine;

public class GameStates : MonoBehaviour
{
    [SerializeField] private GameCondition gameCondition;

    [SerializeField] private GameObject continueScreen;
    [SerializeField] private GameObject tryAgainScreen;
    [SerializeField] private GameObject pausedScreen;

    private void Awake()
    {
        gameCondition.OnGameContinue += GameCondition_OnGameContinue;
        gameCondition.OnGameTryAgain += GameCondition_OnGameTryAgain;
        gameCondition.OnGamePaused += GameCondition_OnGamePaused;
    }

    private void GameCondition_OnGamePaused(object sender, System.EventArgs e)
    {
        pausedScreen.SetActive(true);
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
