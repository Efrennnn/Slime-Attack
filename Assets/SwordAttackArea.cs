using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttackArea : MonoBehaviour
{
    public float damage = 3;
    public Collider2D swordCollider;
    Vector2 rightAttackOffset;

    // Start is called before the first frame update
    private void Start()
    {
        rightAttackOffset = transform.position;
    }


    public void AttackRight() {
        swordCollider.enabled = true;
        transform.localPosition = new Vector3 (rightAttackOffset.x * -1, rightAttackOffset.y);
        print("Right");
    }

    public void AttackLeft() {
        swordCollider.enabled = true;
        transform.localPosition = rightAttackOffset;
        print("Left");
    }

    public void StopAttack() {
        swordCollider.enabled = false;

    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Enemy") {

            Enemy enemy = other.GetComponent<Enemy>();

            if (enemy != null) {
                enemy.Health -= damage;
            }
        }
    }

}
