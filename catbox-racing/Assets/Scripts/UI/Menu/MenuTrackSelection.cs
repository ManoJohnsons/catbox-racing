using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuTrackSelection : MonoBehaviour
{
    public GameObject[] tracks;
    public Image[] selectedTrackImages;
    public Image[] trackImages;
    public TextMeshProUGUI[] trackNames;
    public int selectedTrack = 0;

    private void Awake()
    {
        selectedTrackImages[selectedTrack].gameObject.SetActive(true);
        trackImages[selectedTrack].gameObject.SetActive(true);
        trackNames[selectedTrack].gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        FindObjectOfType<AudioManager>().Play("SelectTrackMusic");
        FindObjectOfType<AudioManager>().Stop("MenuMusic");
    }

    private void OnDisable()
    {
        FindObjectOfType<AudioManager>().Play("MenuMusic");
        FindObjectOfType<AudioManager>().Stop("SelectTrackMusic");
    }

    public void NextTrack()
    {
        DeactiveUI();
        selectedTrack = (selectedTrack + 1) % tracks.Length;
        ActiveUI();
    }

    public void PreviousTrack()
    {
        DeactiveUI();
        selectedTrack--;
        if (selectedTrack < 0)      
            selectedTrack += tracks.Length;   
        ActiveUI();
    }

    public void PlayTrack()
    {
        FindObjectOfType<AudioManager>().Stop("SelectTrackMusic");
        switch (selectedTrack)
        {
            case 0:
                SceneManager.LoadScene("TrackOne");
                break;
            case 1:
                SceneManager.LoadScene("TrackTwo");
                break;
            case 2:
                SceneManager.LoadScene("TrackOne_Reversed");
                break;
            case 3:
                SceneManager.LoadScene("TrackTwo_Reversed");
                break;
        }
    }
    private void ActiveUI()
    {
        tracks[selectedTrack].SetActive(true);
        selectedTrackImages[selectedTrack].gameObject.SetActive(true);
        trackImages[selectedTrack].gameObject.SetActive(true);
        trackNames[selectedTrack].gameObject.SetActive(true);
    }

    private void DeactiveUI()
    {
        tracks[selectedTrack].SetActive(false);
        selectedTrackImages[selectedTrack].gameObject.SetActive(false);
        trackImages[selectedTrack].gameObject.SetActive(false);
        trackNames[selectedTrack].gameObject.SetActive(false);
    }
}
