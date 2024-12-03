using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator animator;
    SpriteRenderer spriteRenderer;
    Rigidbody2D body;

    Vector2 direction = Vector2.zero;

    [SerializeField] float runSpeed = 0.5f;
    [SerializeField] Transform carryPosition;

    float footstepTimer;
    float footstepCooldown = 0.3f;

    void Start()
    {
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //if (GameManager.Instance.IsGamePaused)
        //{
        //    direction = Vector2.zero;
        //    return;
        //}

        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");
        animator.SetFloat("dirX", direction.x);
        animator.SetFloat("dirY", direction.y);

        if (direction.magnitude > 0)
        {
            footstepTimer += Time.deltaTime;
            if (footstepTimer > footstepCooldown)
            {
                footstepTimer = 0;
                //SFXController.Instance.PlayFootstepsSound();
            }
        }
    }

    void FixedUpdate()
    {
        body.linearVelocity = direction.normalized * runSpeed;
    }
}