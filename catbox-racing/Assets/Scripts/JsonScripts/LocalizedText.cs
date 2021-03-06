using System.Collections;
using UnityEngine;
using System;
using TMPro;

public class LocalizedText : MonoBehaviour
{
    [SerializeField] private string _localizationKey;

    TextMeshProUGUI _textComponent;

    IEnumerator Start()
    {
        while (!LocalizationManager.Instance._isReady)
        {
            yield return null;
        }
        AttributionText();
    }

    public void AttributionText()
    {
        if(_textComponent == null)
        {
            _textComponent = gameObject.GetComponent<TextMeshProUGUI>();
        }
        try
        {
            _textComponent.text = LocalizationManager.Instance.GetTextForKey(_localizationKey);
        }
        catch(Exception e)
        {
            Debug.LogError(e);
        }
    }
}
