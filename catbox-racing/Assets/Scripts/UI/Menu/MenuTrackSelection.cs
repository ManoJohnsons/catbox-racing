using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuTrackSelection : MonoBehaviour
{
    public GameObject[] tracks;
    public Image[] selectedTrackImages;
    public TextMeshProUGUI trackName;
    public int selectedTrack = 0;

    private void Awake()
    {
        trackName.text = tracks[selectedTrack].GetComponent<TextMeshPro>().text;
        selectedTrackImages[selectedTrack].gameObject.SetActive(true);
    }
    public void NextTrack()
    {
        DeactiveUI();
        selectedTrack = (selectedTrack + 1) % tracks.Length;
        trackName.text = tracks[selectedTrack].GetComponent<TextMeshPro>().text;
        ActiveUI();
    }

    public void PreviousTrack()
    {
        DeactiveUI();
        selectedTrack--;
        if (selectedTrack < 0)      
            selectedTrack += tracks.Length;   
        trackName.text = tracks[selectedTrack].GetComponent<TextMeshPro>().text;
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
                //SceneManager.LoadScene(); Track Two
                break;
            case 2:
                //SceneManager.LoadScene(); Track One - Reversed
                break;
            case 3:
                //SceneManager.LoadScene(); Track Two - Reversed
                break;
        }
    }
    private void ActiveUI()
    {
        tracks[selectedTrack].SetActive(true);
        selectedTrackImages[selectedTrack].gameObject.SetActive(true);
        trackName.gameObject.SetActive(true);
    }

    private void DeactiveUI()
    {
        tracks[selectedTrack].SetActive(false);
        selectedTrackImages[selectedTrack].gameObject.SetActive(false);
        trackName.gameObject.SetActive(false);
    }
}
