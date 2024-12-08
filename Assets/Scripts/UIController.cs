using System;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject creditsPanel;

    void Start()
    {
        pausePanel.SetActive(false);
        creditsPanel.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            TogglePause();           
        }
    }

    public void TogglePause()
    {
        pausePanel.SetActive(!pausePanel.activeSelf);
        Time.timeScale = pausePanel.activeSelf ? 0 : 1;
    }

    public void ToggleCredits()
    {
        
        creditsPanel.SetActive(!creditsPanel.activeSelf);
        pausePanel.SetActive(!creditsPanel.activeSelf);
    }
}