using System;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static event Action OnTimerFinished = delegate { };
    [SerializeField] float timeLength = 10;
    [SerializeField] GameObject victoryScreen;
    [SerializeField] GameObject gameoverScreen;
    bool timerIsRunning = false;
    public TextMeshProUGUI timeText;
    float timeRemaining = 10;

    private void Start()
    {
        timeRemaining = timeLength;
        timerIsRunning = true;
        gameoverScreen.SetActive(false);
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
                OnTimerFinished?.Invoke();
                if(!victoryScreen.activeSelf)
                    gameoverScreen.SetActive(true);
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}