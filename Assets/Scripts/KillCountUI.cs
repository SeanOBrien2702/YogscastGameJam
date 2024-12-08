using TMPro;
using UnityEngine;

public class KillCountUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI countText;
    void Start()
    {
        PlayerController.OnChangeKillCount += PlayerController_OnChangeKillCount;
        countText.text = "0";
    }

    private void OnDestroy()
    {
        PlayerController.OnChangeKillCount -= PlayerController_OnChangeKillCount;
    }

    private void PlayerController_OnChangeKillCount(int killCount)
    {
        countText.text = killCount.ToString();
    }
}