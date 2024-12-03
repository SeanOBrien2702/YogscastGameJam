using System;
using UnityEngine;

public class MiniGameSelection : MonoBehaviour, IInteractable
{
    public static event Action<MiniGameType> OnSelectMiniGame = delegate{ };
    [SerializeField] MiniGameType gameType;

    public void Interact()
    {
        OnSelectMiniGame?.Invoke(gameType);
    }
}