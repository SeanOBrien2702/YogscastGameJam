using UnityEngine;

public class PlayerController : MonoBehaviour
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
        if (MiniGameController.Instance.IsMiniGameActive) return;
            //if (GameManager.Instance.IsGamePaused)
            //{
            //    direction = Vector2.zero;
            //    return;
            //}

        Interactions();
        Movement();
        
    }

    private void Interactions()
    {
        
        if (direction != Vector2.zero)
        {
            lastDirection = direction;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {

            float interactDistance = 2f;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, lastDirection, interactDistance, interactableMask);
            Debug.Log(lastDirection);
            Debug.DrawRay(transform.position, lastDirection * interactDistance, Color.green);
            if (hit.collider != null)
            {
                if (hit.transform.TryGetComponent(out IInteractable interactable))
                {
                    interactable.Interact();
                }
            }
        }
    }

    private void Movement()
    {
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