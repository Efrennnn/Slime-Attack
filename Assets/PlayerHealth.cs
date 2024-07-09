using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private HealthManager healthManager;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool isInvincible = false;

    void Start()
    {
        healthManager = HealthManager.instance;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !isInvincible)
        {
            TakeDamage(1);
        }
    }

    public void TakeDamage(int damage)
    {
        healthManager.TakeDamage(damage);
        if (healthManager.health > 0)
        {
            StartCoroutine(GetHurt());
        }
    }

    IEnumerator GetHurt()
    {
        isInvincible = true;
        Physics2D.IgnoreLayerCollision(6, 7, true);
        animator.SetBool("IsInvincible", true);
        
        for (int i = 0; i < 10; i++)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }

        animator.SetBool("IsInvincible", false);
        Physics2D.IgnoreLayerCollision(6, 7, false);
        isInvincible = false;
    }
}
