using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChase : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public float distanceBetween;

    private float distance;
    private Vector2 direction;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        direction = (player.transform.position - transform.position).normalized;

        if (distance < distanceBetween)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }

        if(direction.x != 0 || direction.y != 0) {
        animator.SetFloat("MoveX", direction.x);
        animator.SetFloat("MoveY", direction.y);
	
	animator.SetBool("IsChasing", true);
	} else {
	animator.SetBool("IsChasing", false);
	}
    }
}
