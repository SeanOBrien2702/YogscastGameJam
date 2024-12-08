using System;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    float timeElapsed = 0;
    private float direction = 1f;
    private float moveSpeed = .5f;
    [SerializeField] SpriteRenderer spritePrefab;

    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }

    private void Start()
    {
        MiniGameUIController.OnMiniGameComplete += MiniGameUIController_OnMiniGameComplete;
    }

    private void OnDestroy()
    {
        MiniGameUIController.OnMiniGameComplete -= MiniGameUIController_OnMiniGameComplete;
    }

    void Update()
    {
        timeElapsed += Time.deltaTime;

        if (timeElapsed > moveSpeed)
        {
            timeElapsed = 0;
            transform.position = new Vector3(transform.position.x + direction , transform.position.y, transform.position.z);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Wall")
        {
            direction *= -1;
        }
    }

    internal void SetSize(float size)
    {
        for (int i = 0; i < size; i++)
        {
            SpriteRenderer sprite = Instantiate(spritePrefab, transform);
            sprite.transform.localPosition = new Vector3(i, 0, 0);
        }
        BoxCollider2D collider2D = GetComponent<BoxCollider2D>();
        collider2D.size = new Vector2(size, 1);
        collider2D.offset = new Vector2(0.5f * (size - 1), 0);
    }

    private void MiniGameUIController_OnMiniGameComplete(bool obj)
    {
        Destroy(gameObject);
    }
}