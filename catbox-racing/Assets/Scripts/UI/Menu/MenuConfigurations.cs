using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;

public class MenuConfigurations : MonoBehaviour
{
    [SerializeField] private GameObject[] screens;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private TMP_Dropdown resolutionDropdown;

    Resolution[] resolutions;

    void Awake()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for(int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width && 
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void GeneralConfigClicked()
    {
        screens[0].SetActive(true);
        screens[1].SetActive(false);
        screens[2].SetActive(false);
    }

    public void GraphicsConfigClicked()
    {
        screens[0].SetActive(false);
        screens[1].SetActive(true);
        screens[2].SetActive(false);
    }

    public void AudioConfigClicked()
    {
        screens[0].SetActive(false);
        screens[1].SetActive(false);
        screens[2].SetActive(true);
    }

    public void SetBGMVolume(float volume)
    {
        audioMixer.SetFloat("VolumeBGM", volume);
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("VolumeSFX", volume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
