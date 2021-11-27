using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LocalizationManager : MonoBehaviour
{
    [Header("Important string")]
    private const string FILENAME_PREFIX = "text_";
    private const string FILE_EXTENSION = ".json";
    private string FULL_NAME_TEXT_FILE;
    private string FULL_PATH_TEXT_FILE;
    private string LANGUAGE_CHOOSE = "EN";
    private string LOADED_JSON_TEXT = "";

    [Header("Important bool")]
    [HideInInspector] public bool _isReady = false;
    private bool _isFileFound = false;
    private bool _isTryChangeLangRunTime = false;

    [Header("Json variable")]
    private Dictionary<string, string> _localizedDictionary;
    private LocalizationData _loadedData;

    #region "Instance Function"
    private static LocalizationManager localizationManagerInstance;

    public static LocalizationManager Instance
    {
        get
        {
            if (localizationManagerInstance == null)
                localizationManagerInstance = FindObjectOfType(typeof(LocalizationManager)) as LocalizationManager;

            return localizationManagerInstance;
        }
    }
    #endregion Instance Function

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    IEnumerator Start()
    {
        LANGUAGE_CHOOSE = LocaleHelper.GetSupportedLanguageCode();
        FULL_NAME_TEXT_FILE = FILENAME_PREFIX + LANGUAGE_CHOOSE.ToLower() + FILE_EXTENSION;

        FULL_PATH_TEXT_FILE = Path.Combine(Application.streamingAssetsPath, FULL_NAME_TEXT_FILE);

        yield return StartCoroutine(LoadJsonLanguageData());
        _isReady = true;
    }

    IEnumerator LoadJsonLanguageData()
    {
        CheckFileExist();

        yield return new WaitUntil(() => _isFileFound);
        _loadedData = JsonUtility.FromJson<LocalizationData>(LOADED_JSON_TEXT);
        _localizedDictionary = new Dictionary<string, string>(_loadedData.items.Count);
        _loadedData.items.ForEach(item =>
        {
            try
            {
                _localizedDictionary.Add(item.key, item.value);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }); 
    }

    private void CheckFileExist()
    {
        if (!File.Exists(FULL_PATH_TEXT_FILE))
        {
            CopyFileFromResources();
        }
        else
        {
            LoadFileContents();
        }
    }

    private void LoadFileContents()
    {
        LOADED_JSON_TEXT = File.ReadAllText(FULL_PATH_TEXT_FILE);
        _isFileFound = true;
    }

    private void CopyFileFromResources()
    {
        TextAsset myFile = Resources.Load(FILENAME_PREFIX + LANGUAGE_CHOOSE) as TextAsset;
        if(myFile == null)
        {
            Debug.LogError("Make sure the file " + FILENAME_PREFIX + LANGUAGE_CHOOSE + " is in resource folder");
            return;
        }

        LOADED_JSON_TEXT = myFile.ToString();
        File.WriteAllText(FULL_PATH_TEXT_FILE, LOADED_JSON_TEXT);
        StartCoroutine(WaitCreationFile());
    }

    IEnumerator WaitCreationFile()
    {
        FileInfo myFile = new FileInfo(FULL_NAME_TEXT_FILE);
        float timeOut = 0.0f;

        while(timeOut < 5.0f && !IsFileFinishCreate(myFile))
        {
            timeOut += Time.deltaTime;
            yield return null;
        }
    }

    private bool IsFileFinishCreate(FileInfo file)
    {
        FileStream stream = null;
        try
        {
            stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
        }
        catch (IOException)
        {
            _isFileFound = true;
            return true;
        }
        finally
        {
            if(stream != null)
            {
                stream.Close();
            }
        }
        _isFileFound = false;
        return false;
    }

    public string GetTextForKey(string localizationKey)
    {
        if (_localizedDictionary.ContainsKey(localizationKey))
        {
            return _localizedDictionary[localizationKey];
        }
        else
        {
            return "Error: No key matching with " + localizationKey;
        }
    }

    IEnumerator SwitchLanguageRuntime(string langChoose)
    {
        if (!_isTryChangeLangRunTime)
        {
            _isTryChangeLangRunTime = true;
            _isFileFound = false;
            _isReady = false;
            LANGUAGE_CHOOSE = langChoose;

            FULL_NAME_TEXT_FILE = FILENAME_PREFIX + LANGUAGE_CHOOSE.ToLower() + FILE_EXTENSION;

            FULL_PATH_TEXT_FILE = Path.Combine(Application.streamingAssetsPath, FULL_NAME_TEXT_FILE);

            yield return StartCoroutine(LoadJsonLanguageData());
            _isReady = true;

            LocalizedText[] arrayText = Resources.FindObjectsOfTypeAll<LocalizedText>();

            for(int i = 0; i < arrayText.Length; i++)
            {
                arrayText[i].AttributionText();
            }
            _isTryChangeLangRunTime = false;
        }
    }

    public void ChangeLanguage(string lang)
    {
        StartCoroutine(SwitchLanguageRuntime(lang));
    }
}
