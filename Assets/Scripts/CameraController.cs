using UnityEngine;

public class CameraController : MonoBehaviour
{
    Camera camera;
    void Start()
    {
        camera = GetComponent<Camera>();
        MiniGameSelection.OnSelectMiniGame += MiniGameSelection_OnSelectMiniGame;
        MiniGameUIController.OnMiniGameComplete += MiniGameUIController_OnMiniGameComplete;
    }

    private void OnDestroy()
    {
        MiniGameSelection.OnSelectMiniGame -= MiniGameSelection_OnSelectMiniGame;
        MiniGameUIController.OnMiniGameComplete += MiniGameUIController_OnMiniGameComplete;
    }

    private void MiniGameUIController_OnMiniGameComplete()
    {
        camera.enabled = true;
    }

    private void MiniGameSelection_OnSelectMiniGame(MiniGameType type)
    {
        camera.enabled = false;
    }
}