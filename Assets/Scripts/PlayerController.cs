using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator animator;
    [SerializeField] Animator attackAnimator;
    SpriteRenderer spriteRenderer;
    Rigidbody2D body;

    Vector2 direction = Vector2.zero;
    Vector2 lastDirection = Vector2.zero;

    [SerializeField] LayerMask interactableMask;
    [SerializeField] LayerMask enemyMask;
    [SerializeField] float runSpeed = 0.5f;
    [SerializeField] Transform carryPosition;

    float footstepTimer;
    float footstepCooldown = 0.3f;

    float attackTimer;
    float attackCooldown = 0.3f;

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
        Attack();
        
    }

    private void Attack()
    {
        attackTimer += Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && attackTimer > attackCooldown)
        {
            attackAnimator.SetTrigger("Attack");
            attackTimer = 0;
            //RaycastHit2D[] hit = Physics2D.CircleCastAll(transform.position, 75, lastDirection, 4, interactableMask);

            //Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, meleeRange, attackMask);
            //if (colliders.Length > 0)
            //{
            //    AudioController.Instance.PlayOneShot(meleeAttackHitSound, transform.position);
            //    //IDamageable damageable = colliders[UnityEngine.Random.Range(0, colliders.Length - 1)].GetComponent<IDamageable>();
            //    //if (damageable != null)
            //    //{
            //    //    damageable.CalculateDamage(meleeDamage);
            //    //}
            //    foreach (Collider2D collider in colliders)
            //    {
            //        collider.GetComponent<IDamageable>().CalculateDamage(meleeDamage);
            //    }
            //}
            //else
            //{
            //    AudioController.Instance.PlayOneShot(meleeAttackMissSound, transform.position);
            //}
        }
    }


    void OnAttack()
    {
        
    }
    private void Interactions()
    {           
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