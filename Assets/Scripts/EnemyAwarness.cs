using UnityEngine;

public class EnemyAwarness : MonoBehaviour
{
    EnemyController enemy;

    private void Start()
    {
        enemy = GetComponentInParent<EnemyController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Player"))
        {
            return;
        }
        enemy.IsMoving = true;
    }
}