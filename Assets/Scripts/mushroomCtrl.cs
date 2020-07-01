using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mushroomCtrl : MonoBehaviour
{
    private float moveSpeed = 1f;

    private int health = 10;

    private Vector2 direction;
    private SpriteRenderer renderer;
    private Animator animator;
    private PlayerEvents playerScript;
    private Transform playerPos;

    private bool damageDone;
    private bool damageTaken;
    private bool attackMode;
    public bool withSabre;

    public bool hurtMode;

    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        GetDirection();
    }

    private void Update()
    {
        if (health > 0)
        {
            if (attackMode)
            {
                if (!damageDone)
                {
                    DoDamage(playerScript);
                    StartCoroutine(WaitForNextAttack(1));
                }
            }
            else if (hurtMode)
            {
                if (!damageTaken)
                {
                    TakeDamage();
                    StartCoroutine(WaitForNextBullet(1));
                }
            }
            else
            {
                Move(moveSpeed, direction);
            }
        }
        else
        {
            Die();
        }
    }

    void Move(float speed, Vector2 direction)
    {
        transform.Translate(direction * speed * Time.deltaTime);
        animator.SetFloat("Speed", moveSpeed);
    }

    void DoDamage(PlayerEvents playerScript)
    {
        if (playerScript.playerHealth > 0)
        {
            FaceToPlayer();
            GetDirection();

            playerScript.TakeDamage();
            animator.SetBool("IsAttacking", true);
            damageDone = true;
        }
        else
        {
            animator.SetBool("IsAttacking", false);
            attackMode = false;
        }
    }

    void TakeDamage()
    {
        animator.SetBool("IsHurt", true);
        hurtMode = false;

        if (withSabre)
        {
            health = 0;
        }
        else
        {
            health = health - 3;
        }
        Debug.Log(health);
        withSabre = false;
    }

    void Die()
    {
        animator.SetBool("IsDead", true);

        StartCoroutine(Blinker());
        StartCoroutine(Destroy());
    }

    void GetDirection()
    {
        if (renderer.flipX == true)
        {
            direction = Vector2.right;
        }
        else
        {
            direction = Vector2.left;
        }
    }

    void FaceToPlayer()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        if(transform.position.x > playerPos.position.x)
        {
            renderer.flipX = false;
        }
        else
        {
            renderer.flipX = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("RangePoint"))
        {
            if (direction == Vector2.right)
            {
                direction = Vector2.left;
                renderer.flipX = false;
            }
            else
            {
                direction = Vector2.right;
                renderer.flipX = true;
            }
        }

        if (collision.CompareTag("Player"))
        {
            playerScript = collision.GetComponent<PlayerEvents>();
            attackMode = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            attackMode = false;
            animator.SetBool("IsAttacking", false);
        }
    }

    private IEnumerator WaitForNextAttack(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        damageDone = false;
    }

    private IEnumerator WaitForNextBullet(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        animator.SetBool("IsHurt", false);
        damageTaken = false;
    }

    private IEnumerator Blinker()
    {
        Color transperancy;

        transperancy = GetComponent<SpriteRenderer>().color;

        for (float i = 0.1f; i < 10; i = +i)
        {
            yield return new WaitForSeconds(0.2f);
            transperancy.a -= i;
            GetComponent<SpriteRenderer>().color = transperancy;
        }

    }

    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
