using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MermaidMovement : MonoBehaviour
{
    public float moveSpeed = 2f;

    public bool hurtingMode;
    public bool chasingMode;
    public bool attackingMode;

    public GameObject bulletPrefab;
    public GameObject bulletSpawnLocation;

    private Animator animator;
    private SpriteRenderer sprRenderer;
    private Vector2 movingDirection;
    public Vector2 bulletDirection;
    private Transform playerPosition;

    private int mermaidHealth = 100;
    private int dieAnimationIndex;

    private bool isDirectionChanged = true;
    private bool isNextBulletReady = true;

    private bool isDead;
    private bool positionTaken;

    private int shootAnimationIndex;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        sprRenderer = GetComponent<SpriteRenderer>();

        if (sprRenderer.flipX)
        {
            movingDirection = Vector2.left;
        }
        else
        {
            movingDirection = Vector2.right;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("SpawnPoint").GetComponent<PlayerSpawn>().playerSpawned && !positionTaken)
        {
            playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            positionTaken = true;
        }

        if (mermaidHealth <= 0)
        {
            isDead = true;
        }

        if (!isDead)
        {
            if (hurtingMode)
            {
                TakeDamage();
            }
            else if (attackingMode && chasingMode)
            {
                DoDamage();
            }
            else if (chasingMode)
            {
                ChasePlayer();
            }
            else
            {
                Move(moveSpeed, movingDirection);
            }
        }
        else
        {
            Die();
        }
    }

    void Move(float speed, Vector2 direction)
    {
        moveSpeed = 1f;

        transform.Translate(direction * speed * Time.deltaTime);

        if (direction == Vector2.left)
        {
            sprRenderer.flipX = true;
        }
        else
        {
            sprRenderer.flipX = false;
        }

        animator.SetFloat("Speed", speed);
        animator.SetBool("IsAttacking", false);
        animator.SetBool("IsHurt", false);
        animator.SetBool("IsAngry", false);

        attackingMode = false;

        if (isDirectionChanged)
        {
            StartCoroutine(ChangeDirection(6));
        }
    }

    void DoDamage()
    {
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEvents>().isDead)
        {
            attackingMode = false;
            chasingMode = false;
            hurtingMode = false;
        }

        if (!hurtingMode)
        {
            LookAtTarget(playerPosition);

            //StopMoving
            moveSpeed = 0;
            animator.SetFloat("Speed", moveSpeed);

            //Start attacking animation
            animator.SetBool("IsAttacking", true);
            animator.SetBool("IsHurt", false);
            animator.SetBool("IsAngry", false);

            if (!animator.GetBool("IsHurt"))
            {
                //Spawn a bullet prefab
                shootAnimationIndex = ((int)(animator.GetCurrentAnimatorStateInfo(0).normalizedTime * (10))) % 10;
                if (shootAnimationIndex == 5 && isNextBulletReady)
                {
                    Vector3 spawnLocation = new Vector3();

                    //Get bullet direction and spawn bullet position
                    if (sprRenderer.flipX)
                    {
                        spawnLocation = new Vector3(bulletSpawnLocation.transform.position.x - (0.396f * 2), bulletSpawnLocation.transform.position.y, bulletSpawnLocation.transform.position.z);
                        bulletDirection = Vector2.left;
                    }
                    else
                    {
                        spawnLocation = new Vector3(bulletSpawnLocation.transform.position.x, bulletSpawnLocation.transform.position.y, bulletSpawnLocation.transform.position.z);
                        bulletDirection = Vector2.right;
                    }

                    Instantiate(bulletPrefab, spawnLocation, Quaternion.identity);
                    isNextBulletReady = false;
                    StartCoroutine(GetBulletReady(0.9f));
                }
            }
        }
    }

    void TakeDamage()
    {
        moveSpeed = 0;

        animator.SetBool("IsHurt", true);
        animator.SetBool("IsAttacking", false);
        animator.SetBool("IsAngry", false);
        hurtingMode = false;


        mermaidHealth -= 20;

        StartCoroutine(StopHurtAnimation(0.5f));
    }

    void ChasePlayer()
    {
        moveSpeed = 2f;
        animator.SetFloat("Speed", moveSpeed);
        animator.SetBool("IsHurt", false);
        animator.SetBool("IsAttacking", false);

        LookAtTarget(playerPosition);

        //Go to player is too far away
        if (Vector2.Distance(transform.position, playerPosition.position) > 4)
        {
            moveSpeed = 3f;
            animator.SetBool("IsAngry", true);
            transform.position = Vector2.MoveTowards(transform.position, playerPosition.position, moveSpeed * Time.deltaTime);
        }
        //Start attacking the player
        else
        {
            attackingMode = true;
        }
    }

    void Die()
    {
        animator.SetBool("IsAttacking", false);
        animator.SetBool("IsHurt", false);
        animator.SetBool("IsDead", true);

        dieAnimationIndex = ((int)(animator.GetCurrentAnimatorStateInfo(0).normalizedTime * (10))) % 10;
        if (dieAnimationIndex == 1)
        {
            StartCoroutine(Blinker());
        }

        Destroy(gameObject, 3);
    }

    void LookAtTarget(Transform target)
    {
        if (transform.position.x > target.transform.position.x)
        {
            sprRenderer.flipX = true;
        }
        else
        {
            sprRenderer.flipX = false;
        }
    }

    private IEnumerator ChangeDirection(float seconds)
    {
        isDirectionChanged = false;

        yield return new WaitForSeconds(seconds);

        if (movingDirection == Vector2.right)
        {
            movingDirection = Vector2.left;
            sprRenderer.flipX = true;
        }
        else
        {
            movingDirection = Vector2.right;
            sprRenderer.flipX = false;
        }

        isDirectionChanged = true;
    }

    private IEnumerator StopHurtAnimation(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        animator.SetBool("IsHurt", false);
    }

    IEnumerator GetBulletReady(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        isNextBulletReady = true;
    }

    private IEnumerator Blinker()
    {
        Color transperancy;

        transperancy = GetComponent<SpriteRenderer>().color;

        for (float i = 0.1f; i < 10; i = +i)
        {
            yield return new WaitForSeconds(0.3f);
            transperancy.a -= i;
            GetComponent<SpriteRenderer>().color = transperancy;
        }

    }
}
