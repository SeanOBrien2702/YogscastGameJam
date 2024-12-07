using FMOD;
using FMOD.Studio;
using FMODUnity;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
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
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        backgroundMusic.Add(RuntimeManager.CreateInstance(overworld));
        backgroundMusic.Add(RuntimeManager.CreateInstance(overworld2));
        backgroundMusic.Add(RuntimeManager.CreateInstance(minigame));
        backgroundMusic.Add(RuntimeManager.CreateInstance(minigame2));
        backgroundMusic.Add(RuntimeManager.CreateInstance(menu));
        backgroundMusic.Add(RuntimeManager.CreateInstance(credit));
        backgroundMusic[currentMusic].start();

        MiniGameSelection.OnSelectMiniGame += MiniGameSelection_OnSelectMiniGame;
        MiniGameUIController.OnMiniGameComplete += MiniGameUIController_OnMiniGameComplete;
        //GameSettings.OnMusicVolumeChange += GameSettings_OnMusicVolumeChange;
        //GameSettings.OnSoundFXVolumeChange += GameSettings_OnSoundFXVolumeChange;
    }

    private void OnDestroy()
    {
        MiniGameSelection.OnSelectMiniGame -= MiniGameSelection_OnSelectMiniGame;
        MiniGameUIController.OnMiniGameComplete -= MiniGameUIController_OnMiniGameComplete;
    }

    public void PlaySoundEffect(EventReference soundEffect)
    {
        soundFX = RuntimeManager.CreateInstance(soundEffect);
        soundFX.setVolume(soundVolume);
        soundFX.start();
    }

    private void MiniGameUIController_OnMiniGameComplete()
    {
        backgroundMusic[currentMusic].stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        currentMusic = Random.Range(0, 2) == 0 ? 0 : 2;
        backgroundMusic[currentMusic].start();
    }

    private void MiniGameSelection_OnSelectMiniGame(MiniGameType obj)
    {
        backgroundMusic[currentMusic].stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        currentMusic = Random.Range(0, 2) == 0 ? 1 : 3;
        backgroundMusic[currentMusic].start();
    }
}