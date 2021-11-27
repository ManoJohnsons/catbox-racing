using UnityEngine;

public class ChangeLanguageEvent : MonoBehaviour
{
    public void OnChangeLanguageRuntime(string lang)
    {
        LocalizationManager.Instance.ChangeLanguage(lang);
    }
}
