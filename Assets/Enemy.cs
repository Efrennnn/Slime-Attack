using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    public float detectionRange = 5f;
    public float health = 1f;
    public float separationDistance = 1f; // Minimum distance to maintain between slimes
    public LayerMask slimeLayer; // Layer for slimes
    public LayerMask environmentLayer; // Layer for environment obstacles

    private GameObject player;
    private Animator animator;

    public float Health
    {
        set
        {
            health = value;

            if (health <= 0)
            {
                Defeated();
            }
        }
        get
        {
            return health;
        }
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (health > 0) // Only move if the slime is alive
        {
            if (player != null)
            {
                float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

                if (distanceToPlayer < detectionRange)
                {
                    Vector2 direction = (player.transform.position - transform.position).normalized;
                    Vector2 newPosition = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

                    // Check for nearby slimes to avoid overlap
                    Collider2D[] slimeColliders = Physics2D.OverlapCircleAll(transform.position, separationDistance, slimeLayer);
                    foreach (Collider2D collider in slimeColliders)
                    {
                        if (collider.gameObject != gameObject)
                        {
                            Vector2 separationDirection = (transform.position - collider.transform.position).normalized;
                            newPosition += separationDirection * Time.deltaTime;
                        }
                    }

                    // Check for environmental obstacles
                    RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, speed * Time.deltaTime + 0.1f, environmentLayer);
                    if (hit.collider == null) // No obstacles detected
                    {
                        transform.position = newPosition;
                    }

                    // Set the animator parameters for animation
                    animator.SetBool("IsMoving", true);
                    animator.SetFloat("MoveX", direction.x);
                    animator.SetFloat("MoveY", direction.y);

                    // Flip the sprite renderer based on the direction
                    FlipSprite(direction.x < 0);
                }
                else
                {
                    animator.SetBool("IsMoving", false);
                }
            }
        }
    }

    void FlipSprite(bool shouldFlip)
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.flipX = shouldFlip;
    }

    public void Defeated()
    {
        animator.SetBool("IsMoving", false);
        animator.SetTrigger("Defeated");
    }

    public void RemoveEnemy()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, separationDistance);
    }
}
