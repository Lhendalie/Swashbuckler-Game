using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMove : MonoBehaviour
{
    private float moveSpeed = 1f;
    private Vector2 direction;
    private SpriteRenderer renderer;
    private Animator animator;

    private bool isDirectionChanged = true;
    private bool isAttacking;
    private bool attackingModeChanged = true;

    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();       
    }

    private void Update()
    {
        Move(moveSpeed, direction);
    }

    void Move(float speed, Vector2 direction)
    {
        GetDirection();

        transform.Translate(direction * speed * Time.deltaTime);

        if (isDirectionChanged)
        {
            StartCoroutine(ChangeDirection(5));
        }

        if (attackingModeChanged)
        {
            StartCoroutine(ChangeAttacking(4));
        }
    }

    void GetDirection()
    {
        if (renderer.flipX)
        {
            direction = Vector2.right;
        }
        else
        {
            direction = Vector2.left;
        }
    }

    private IEnumerator ChangeDirection(float seconds)
    {
        isDirectionChanged = false;

        yield return new WaitForSeconds(seconds);

        if (direction == Vector2.right)
        {
            renderer.flipX = false;
        }
        else
        {
            renderer.flipX = true;
        }

        isDirectionChanged = true;
    }

    private IEnumerator ChangeAttacking(float seconds)
    {
        attackingModeChanged = false;

        yield return new WaitForSeconds(seconds);

        if (!isAttacking)
        {
            animator.SetBool("IsAttacking", true);
            isAttacking = true;
        }
        else
        {
            animator.SetBool("IsAttacking", false);
            isAttacking = false;
        }

        attackingModeChanged = true; ;
    }
}
