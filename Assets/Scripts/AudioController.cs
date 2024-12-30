using FMOD;
using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.EventSystems.EventTrigger;

public class AudioController : MonoBehaviour
{
    [Header("Music")]
    [SerializeField] EventReference overworld;
    [SerializeField] EventReference overworld2;
    [SerializeField] EventReference minigame;
    [SerializeField] EventReference minigame2;
    [SerializeField] EventReference menu;
    [SerializeField] EventReference credit;
    public static AudioController Instance { get; private set; }
    List<EventInstance> backgroundMusic = new List<EventInstance>();
    EventInstance soundFX;
    int currentMusic = 0;
    float musicVolume = 0.5f;
    float soundVolume = 0.5f;
    private void Awake()
    {
       
    }

    void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        //if (FMODUnity.RuntimeManager.HasBankLoaded("Master"))
        //{
        //    Debug.Log("Master Bank Loaded");
        //    eventInstance = FMODUnity.RuntimeManager.CreateInstance(eventPath);
        //}

        StartCoroutine(LoadMusic());

    

        MiniGameSelection.OnSelectMiniGame += MiniGameSelection_OnSelectMiniGame;
        MiniGameUIController.OnMiniGameComplete += MiniGameUIController_OnMiniGameComplete;
        //SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        GameSettings.OnMusicVolumeChange += GameSettings_OnMusicVolumeChange;
        GameSettings.OnSoundFXVolumeChange += GameSettings_OnSoundFXVolumeChange;
    }

    IEnumerator LoadMusic()
    {
        yield return new WaitForSeconds(1);
        backgroundMusic.Add(RuntimeManager.CreateInstance(menu));
        backgroundMusic.Add(RuntimeManager.CreateInstance(overworld));
        backgroundMusic.Add(RuntimeManager.CreateInstance(overworld2));
        backgroundMusic.Add(RuntimeManager.CreateInstance(minigame));
        backgroundMusic.Add(RuntimeManager.CreateInstance(minigame2));
        backgroundMusic.Add(RuntimeManager.CreateInstance(credit));

        //if (backgroundMusic[0].)

        backgroundMusic[currentMusic].start();
    }

    //private void SceneManager_sceneLoaded(Scene scene, LoadSceneMode arg1)
    //{
    //    if(scene.name == "GameScene")
    //    {
    //        PlayOverworldMusic();
    //    }
    //}

    private void OnDestroy()
    {
        MiniGameSelection.OnSelectMiniGame -= MiniGameSelection_OnSelectMiniGame;
        MiniGameUIController.OnMiniGameComplete -= MiniGameUIController_OnMiniGameComplete;
        GameSettings.OnMusicVolumeChange -= GameSettings_OnMusicVolumeChange;
        GameSettings.OnSoundFXVolumeChange -= GameSettings_OnSoundFXVolumeChange;
    }

    void PlayOverworldMusic()
    {
        backgroundMusic[currentMusic].stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        currentMusic = Random.Range(0, 2) == 0 ? 1 : 2;
        backgroundMusic[currentMusic].start();
        backgroundMusic[currentMusic].setVolume(musicVolume);
    }

    public void PlaySoundEffect(EventReference soundEffect)
    {
        soundFX = RuntimeManager.CreateInstance(soundEffect);
        soundFX.setVolume(soundVolume);
        soundFX.start();
    }

    private void MiniGameUIController_OnMiniGameComplete(bool hasWon)
    {
        PlayOverworldMusic();
    }

    private void MiniGameSelection_OnSelectMiniGame(MiniGameType obj)
    {
        backgroundMusic[currentMusic].stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        currentMusic = Random.Range(0, 2) == 0 ? 4 : 3;
        backgroundMusic[currentMusic].start();
    }

    private void GameSettings_OnSoundFXVolumeChange(float newSoundVolume)
    {
        soundVolume = newSoundVolume; 
    }

    private void GameSettings_OnMusicVolumeChange(float newMusicVolume)
    {
        musicVolume = newMusicVolume;
        backgroundMusic[currentMusic].setVolume(musicVolume);
    }

    internal void StartGame()
    {
        PlayOverworldMusic();
    }
}