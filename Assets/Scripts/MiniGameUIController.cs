using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGameUIController : MonoBehaviour
{
    public static event Action<bool> OnMiniGameComplete = delegate { };

    public void CloseAdditiveLostScene()
    {
        OnMiniGameComplete?.Invoke(false);
    }

    public void CloseAdditiveScene()
    {
        OnMiniGameComplete?.Invoke(true);
    }
}
