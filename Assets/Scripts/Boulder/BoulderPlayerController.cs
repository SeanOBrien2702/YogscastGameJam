using UnityEngine;

public class BoulderPlayerController : MonoBehaviour
{
    Animator animator;
    SpriteRenderer spriteRenderer;
    Rigidbody2D body;

    Vector2 direction = Vector2.zero;
    Vector2 lastDirection = Vector2.zero;

    [SerializeField] LayerMask interactableMask;
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
        Movement();
    }

    private void Movement()
    {
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");

        if (direction.magnitude > 0)
        {
            footstepTimer += Time.deltaTime;
            if (footstepTimer > footstepCooldown)
            {
                footstepTimer = 0;
                //SFXController.Instance.PlayFootstepsSound();
            }
            animator.SetBool("IsMoving", true);
            animator.SetFloat("dirX", direction.x);
            animator.SetFloat("dirY", direction.y);
            lastDirection = direction;
        }
        else
        {
            animator.SetFloat("dirX", lastDirection.x);
            animator.SetFloat("dirY", lastDirection.y);
            animator.SetBool("IsMoving", false);
        }
    }

    void FixedUpdate()
    {
        body.linearVelocity = direction.normalized * runSpeed;
    }
}