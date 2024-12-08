using System;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageable
{
    public static PlayerController Instance { get; private set; }
    public static event Action<int> OnChangeHealth = delegate { };
    public static event Action<int> OnChangeKillCount = delegate { };
    Animator animator;
    SpriteRenderer spriteRenderer;
    Rigidbody2D body;

    Vector2 direction = Vector2.zero;
    Vector2 lastDirection = Vector2.zero;

    [SerializeField] LayerMask interactableMask;
    [SerializeField] float runSpeed = 0.5f;
    [SerializeField] int health;
    int currentHealth;
    float footstepTimer;
    float footstepCooldown = 0.3f;
    bool isTalking = false;
    

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        OnChangeHealth?.Invoke(health);
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        NPCController.OnNPCSelected += NPCController_OnNPCSelected;
        DialogueController.OnDialogueComplete += DialogueController_OnDialogueComplete;
    }

    private void OnDestroy()
    {
        NPCController.OnNPCSelected -= NPCController_OnNPCSelected;
        DialogueController.OnDialogueComplete -= DialogueController_OnDialogueComplete;
    }

    void Update()
    {
        if (MiniGameController.Instance.IsMiniGameActive) return;
        if (isTalking) return;
        Interactions();
        Movement();        
    }

    private void Interactions()
    {      
        if (Input.GetKeyDown(KeyCode.E))
        {
            float interactDistance = 2f;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, lastDirection, interactDistance, interactableMask);
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

    private void DialogueController_OnDialogueComplete()
    {
        isTalking = false;
    }

    private void NPCController_OnNPCSelected(string[] obj)
    {
        isTalking = true;
    }

    public void TakeDamage()
    {
        health--;
        OnChangeHealth.Invoke(health);
    }
}