using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGameUIController : MonoBehaviour
{
    public static event Action OnMiniGameComplete = delegate { };
    public void CloseAdditiveScene()
    {
        //int lastSceneIndex = SceneManager.sceneCount - 1;
        //SceneManager.UnloadSceneAsync(lastSceneIndex);
        OnMiniGameComplete?.Invoke();
    }
}
