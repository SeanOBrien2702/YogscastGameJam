using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] Sprite fullHealth;
    [SerializeField] Sprite halfHealth;
    [SerializeField] Sprite emptyHealth;

    [SerializeField] List<Image> images; 
    void Start()
    {
        PlayerController.OnChangeHealth += PlayerController_OnChangeHealth;
    }

    private void OnDestroy()
    {
        PlayerController.OnChangeHealth -= PlayerController_OnChangeHealth;
    }

    private void PlayerController_OnChangeHealth(int health)
    {
        //TODO: clean up this garbage code
        switch (health)
        {
            case 0:
                foreach (var image in images)
                {
                    image.sprite = emptyHealth;
                }
                break;
            case 1:
                images[0].sprite = halfHealth;
                images[1].sprite = emptyHealth;
                images[2].sprite = emptyHealth;
                break;
            case 2:
                images[0].sprite = fullHealth;
                images[1].sprite = emptyHealth;
                images[2].sprite = emptyHealth;
                break;
            case 3:
                images[0].sprite = fullHealth;
                images[1].sprite = halfHealth;
                images[2].sprite = emptyHealth;
                break;
            case 4:
                images[0].sprite = fullHealth;
                images[1].sprite = fullHealth;
                images[2].sprite = emptyHealth;
                break;
            case 5:
                images[0].sprite = fullHealth;
                images[1].sprite = fullHealth;
                images[2].sprite = halfHealth;
                break;
            case 6:
                foreach (var image in images)
                {
                    image.sprite = fullHealth;
                }
                break;
            default:
                break;
        }
    }
}