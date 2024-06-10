using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoveController : MonoBehaviour{
    public float moveSpeed = 1f;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;
    public SwordAttackArea swordAttack;

    private Vector2 movementInput;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Animator animator;
    private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    private bool canMove = true;

    void Start(){
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate(){
        if (canMove){
            if (movementInput != Vector2.zero){
                bool success = TryMove(movementInput);

                if (!success)
                {
                    success = TryMove(new Vector2(movementInput.x, 0));
                }

                if (!success)
                {
                    success = TryMove(new Vector2(0, movementInput.y));
                }

                animator.SetBool("IsMoving", success);
                animator.SetFloat("MoveX", movementInput.x);
                animator.SetFloat("MoveY", movementInput.y);
            } else {
                animator.SetBool("IsMoving", false);
            }

            if (movementInput.x < 0){
                spriteRenderer.flipX = true;
            } else if (movementInput.x > 0){
                spriteRenderer.flipX = false;
            }
        }
    }

    private bool TryMove(Vector2 direction){
        if (direction != Vector2.zero){
            int count = rb.Cast(
                direction, movementFilter, castCollisions, moveSpeed * Time.fixedDeltaTime + collisionOffset
            );

            if (count == 0){
                rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
                return true;
            } else {
                return false;
            }
        } else {
            return false;
        }
    }

    void OnMove(InputValue movementValue){
        movementInput = movementValue.Get<Vector2>();
    }

    void OnFire(){
        animator.SetTrigger("Attack");
    }

    public void SwordAttackArea(){
        LockMovement();
        if (Mathf.Abs(movementInput.x) > Mathf.Abs(movementInput.y)){
            if (movementInput.x < 0){
                swordAttack.AttackLeft();
            } else {
                swordAttack.AttackRight();
            }
        } else {
            if (movementInput.y < 0){
                swordAttack.AttackDown();
            } else {
                swordAttack.AttackUp();
            }
        }
    }

    public void EndSwordAttack(){
        UnlockMovement();
        swordAttack.StopAttack();
    }

    public void LockMovement(){
        canMove = false;
    }

    public void UnlockMovement(){
        canMove = true;
    }
}
