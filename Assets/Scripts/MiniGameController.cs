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
    MiniGameType selectedType;
    string lastLoadedScene = "";
    public bool IsMiniGameActive { get => isMiniGameActive; }

    bool isStackingComlete = false;
    bool isWiringComplete = false;
    bool isBoulderComplete = false;

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
        selectedType = type;
    }

    private void MiniGameUIController_OnMiniGameComplete(bool hasWon)
    {
        isMiniGameActive = false;
        SceneManager.UnloadSceneAsync(lastLoadedScene);
        if (hasWon)
        {
            switch (selectedType)
            {
                case MiniGameType.BoulderMaze:
                    isBoulderComplete = true;
                    break;
                case MiniGameType.PresentStacking:
                    isStackingComlete = true;
                    break;
                case MiniGameType.LightWiring:
                    isWiringComplete = true;
                    break;
            }
        }
        if(isBoulderComplete &&
           isStackingComlete &&
           isWiringComplete)
        {
            SceneManager.LoadScene("FinalScene");
        }
    }
}