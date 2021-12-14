using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuTrackSelection : MonoBehaviour
{
    public GameObject[] tracks;
    public Image[] selectedTrackImages;
    public TextMeshProUGUI[] trackNames;
    public int selectedTrack = 0;

    private void Awake()
    {
        selectedTrackImages[selectedTrack].gameObject.SetActive(true);
        trackNames[selectedTrack].gameObject.SetActive(true);
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
                //SceneManager.LoadScene("TrackOne");
                break;
        }
    }
    private void ActiveUI()
    {
        tracks[selectedTrack].SetActive(true);
        selectedTrackImages[selectedTrack].gameObject.SetActive(true);
        trackNames[selectedTrack].gameObject.SetActive(true);
    }

    private void DeactiveUI()
    {
        tracks[selectedTrack].SetActive(false);
        selectedTrackImages[selectedTrack].gameObject.SetActive(false);
        trackNames[selectedTrack].gameObject.SetActive(false);
    }
}
