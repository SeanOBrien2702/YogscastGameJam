using UnityEngine;

public class BoulderController : MonoBehaviour
{
    [SerializeField] GameObject victoryScreen;

    void Start()
    {
        victoryScreen.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        victoryScreen.SetActive(true);
    }
}