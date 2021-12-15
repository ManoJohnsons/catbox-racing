using System.Collections;
using UnityEngine;
using TMPro;

public class Countdown : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdown;
    [SerializeField] private GameObject kartControl;
    [SerializeField] private GameObject[] UIElements;
    public string nameTrackMusic;
    private TimerManager timerManager;


    private void Awake()
    {
        timerManager = GetComponent<TimerManager>();
    }

    void Start()
    {
        StartCoroutine(CountdownStart());
    }

    IEnumerator CountdownStart()
    {
        yield return new WaitForSeconds(0.5f);
        countdown.text = "3";
        countdown.gameObject.SetActive(true);
        FindObjectOfType<AudioManager>().Play("Countdown");
        yield return new WaitForSeconds(1);
        countdown.gameObject.SetActive(false);
        countdown.text = "2";
        countdown.gameObject.SetActive(true);
        FindObjectOfType<AudioManager>().Play("Countdown");
        yield return new WaitForSeconds(1);
        countdown.gameObject.SetActive(false);
        countdown.text = "1";
        countdown.gameObject.SetActive(true);
        FindObjectOfType<AudioManager>().Play("Countdown");
        yield return new WaitForSeconds(1);
        countdown.gameObject.SetActive(false);
        FindObjectOfType<AudioManager>().Play("GO");
        ActiveUI();
        kartControl.SetActive(true);
        timerManager.enabled = true;
        FindObjectOfType<AudioManager>().Play(nameTrackMusic);
    }

    private void ActiveUI()
    {
        for (int i = 0; i < UIElements.Length; i++)
        {
            UIElements[i].SetActive(true);
        }
    }
}
