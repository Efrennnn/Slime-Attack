using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Collider2D attackCollider;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }

    void Attack()
    {
        attackCollider.enabled = true;
        StartCoroutine(DisableAttackCollider());
    }

    IEnumerator DisableAttackCollider()
    {
        yield return new WaitForSeconds(0.1f); // Attack duration
        attackCollider.enabled = false;
    }
}
