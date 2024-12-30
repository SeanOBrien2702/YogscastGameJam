using FMOD;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUIController : MonoBehaviour
{
//    [SerializeField] EventReference menu;

//    EventInstance menuMusic;
//    private void Start()
//    {
//        menuMusic = RuntimeManager.CreateInstance(menu);
//        menuMusic.setVolume(0.5f);
//        menuMusic.start();
//    }

    public void StartGame()
    {
        
        //menuMusic.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        SceneManager.LoadScene("GameScene");
        AudioController.Instance.StartGame();
    }

    public void Quit()
    {
        Application.Quit();
    }
}