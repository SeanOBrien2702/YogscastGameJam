using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGameController : MonoBehaviour
{
    public static MiniGameController Instance;
    [SerializeField] string[] miniGameScenes;
    bool isMiniGameActive = false;
    Dictionary<MiniGameType, string> sceneDict = new Dictionary<MiniGameType, string>();
    string lastLoadedScene = "";
    public bool IsMiniGameActive { get => isMiniGameActive; }

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

        for (int i = 0; i < miniGameScenes.Length; i++)
        {
            sceneDict.Add((MiniGameType)i, miniGameScenes[i]);
        }
    }

    void Start()
    {
        MiniGameSelection.OnSelectMiniGame += MiniGameSelection_OnSelectMiniGame;
        MiniGameUIController.OnMiniGameComplete += MiniGameUIController_OnMiniGameComplete;
    }

    private void OnDestroy()
    {
        MiniGameSelection.OnSelectMiniGame -= MiniGameSelection_OnSelectMiniGame;
        MiniGameUIController.OnMiniGameComplete += MiniGameUIController_OnMiniGameComplete;
    }

    private void MiniGameSelection_OnSelectMiniGame(MiniGameType type)
    {
        isMiniGameActive = true;
        SceneManager.LoadScene(sceneDict[type], LoadSceneMode.Additive);
        lastLoadedScene = sceneDict[type];
    }

    private void MiniGameUIController_OnMiniGameComplete()
    {
        isMiniGameActive = false;
        SceneManager.UnloadSceneAsync(lastLoadedScene);
    }
}