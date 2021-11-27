using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    public void OnSwitchScene(string nameScene)
    {
        SceneManager.LoadScene(nameScene);
    }
}
