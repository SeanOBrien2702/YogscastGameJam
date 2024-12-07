using UnityEngine;

public class EnemyController : MonoBehaviour, IDamageable
{
    Animator animator;
    float movementSpeed = 2;
    Transform player;
    bool isMoving = false;
    Vector3 direction;
    public bool IsMoving { get => isMoving; set => isMoving = value; }

    void Start()
    {
        animator = GetComponent<Animator>();
        player = PlayerController.Instance.transform;
    }

    void FixedUpdate()
    {
        if (isMoving)
        { 
            transform.position = Vector3.MoveTowards(transform.position, player.position, movementSpeed * Time.fixedDeltaTime);
            var heading = transform.position - player.transform.position;
            if (Mathf.Abs(heading.x) > Mathf.Abs(heading.y))
            {
                if (heading.x > 0)
                {
                    heading = Vector3.right;
                }
                else
                {
                    heading = Vector3.left;
                }
            }
            else
            {
                if (heading.y > 0)
                {
                    heading = Vector3.up;
                }
                else
                {
                    heading = Vector3.down;
                }
            }
            animator.SetFloat("dirX", heading.x);
            animator.SetFloat("dirY", heading.y);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Player"))
        {
            return;
        }
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage();
        }
        Destroy(gameObject);
    }

    public void TakeDamage()
    {
        Destroy(gameObject);
    }
}