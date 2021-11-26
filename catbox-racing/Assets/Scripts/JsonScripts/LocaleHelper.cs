using UnityEngine;

public static class LocaleHelper
{
    public static string GetSupportedLanguageCode()
    {
        SystemLanguage lang = Application.systemLanguage;

        switch (lang) 
        {
            case SystemLanguage.Portuguese:
                return LocaleApplication.PT;
            case SystemLanguage.English:
                return LocaleApplication.EN;
            default:
                return GetDefaultSupportedLanguageCode();
        }
    }

    static string GetDefaultSupportedLanguageCode()
    {
        return LocaleApplication.EN;
    }
}
