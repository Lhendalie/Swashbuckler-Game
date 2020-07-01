using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerController controller;
    public Animator animator;

    public AudioSource bgAudio;
    public AudioSource DiveInWaterSplash;
    public AudioSource UnderWaterIntro;
    public AudioSource UnderWaterLoop;
    public AudioSource sabreAttack;

    private AudioSource jumpSound;

    public float runSpeed = 40f;
    public float climbSpeed = 40f;

    float horizontalMove = 0f;
    float verticalMove = 0f;

    bool jump = false;
    bool crouch = false;
    bool isClimbing = false;

    private bool trigger = false;
    private string colliderObjectName;

    private void Start()
    {
        bgAudio = GameObject.Find("BGAudio").GetComponent<AudioSource>();
        DiveInWaterSplash = GameObject.Find("DiveInWaterSplash").GetComponent<AudioSource>();
        UnderWaterIntro = GameObject.Find("UnderWaterIntro").GetComponent<AudioSource>();
        UnderWaterLoop = GameObject.Find("UnderWaterLoop").GetComponent<AudioSource>();
        sabreAttack = GameObject.Find("SabreAttack").GetComponent<AudioSource>();
        jumpSound = GameObject.Find("JumpSound").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isClimbing)
        {
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        }

        if (isClimbing)
        {
            horizontalMove = 0f;
            verticalMove = Input.GetAxisRaw("Vertical") * climbSpeed;

            animator.SetFloat("Speed", Mathf.Abs(verticalMove));
        }

        if (Input.GetButtonDown("Aim"))
        {
            animator.SetBool("IsAiming", true);
        }
        else if (Input.GetButtonUp("Aim"))
        {
            animator.SetBool("IsAiming", false);
        }

        if (Input.GetButtonDown("Attack"))
        {
            animator.SetBool("IsAttacking", true);
            sabreAttack.Play();
        }
        else if (Input.GetButtonUp("Attack"))
        {
            animator.SetBool("IsAttacking", false);
        }

        if (Input.GetButtonDown("Throw"))
        {
            animator.SetBool("IsThrowing", true);
        }
        else if (Input.GetButtonUp("Throw"))
        {
            animator.SetBool("IsThrowing", false);
        }

        if (Input.GetButtonDown("Shoot"))
        {
            animator.SetBool("IsShooting", true);
        }
        else if (Input.GetButtonUp("Shoot"))
        {
            animator.SetBool("IsShooting", false);
        }

        if (!controller.m_Swimming && !isClimbing && Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("IsJumping", true);
            jumpSound.Play();
        }

        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }

        if (trigger)
        {
            if (Input.GetButtonDown("Climb"))
            {
                Debug.Log($"Needed Name: {colliderObjectName}Point");
                transform.position = GameObject.Find($"{colliderObjectName}Point").transform.position;

                if (colliderObjectName == "CheckIfLadder1" || colliderObjectName == "CheckIfLadder2" || colliderObjectName == "CheckIfLadder5" || colliderObjectName == "CheckIfLadder6" || colliderObjectName == "CheckIfLadder9" || colliderObjectName == "CheckIfLadder10" || colliderObjectName == "CheckIfLadder13" || colliderObjectName == "CheckIfLadder14")
                {
                    horizontalMove = 0;
                    animator.SetBool("IsSwimming", false);
                    animator.SetBool("IsClimbing", true);
                    GetComponent<Rigidbody2D>().gravityScale = 0;
                    isClimbing = true;
                }
                else
                {
                    verticalMove = 0;
                    animator.SetBool("IsClimbing", false);
                    GetComponent<Rigidbody2D>().gravityScale = 3;
                    isClimbing = false;
                }
            }
        }
    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
        animator.SetBool("IsClimbing", false);
        animator.SetBool("IsSwimming", false);

        bgAudio.volume = 0.08f;
        UnderWaterLoop.Stop();
    }

    public void OnSwimming()
    {
        animator.SetBool("IsJumping", false);
        animator.SetBool("IsSwimming", true);

        bgAudio.volume = 0.04f;
        DiveInWaterSplash.Play();
        UnderWaterIntro.Play();
        UnderWaterLoop.Play();


        jump = false;
    }

    public void OnCrouching(bool isCrouching)
    {
        animator.SetBool("IsCrouching", isCrouching);
    }

    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);

        jump = false;

        if (isClimbing)
        {
            controller.Climb(verticalMove * Time.fixedDeltaTime);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            trigger = true;
            colliderObjectName = collision.name;
        }

        if (Input.GetButtonDown("Attack"))
        {
            if (collision.CompareTag("Mushroom"))
            {
                animator.SetBool("IsAttacking", true);
                sabreAttack.Play();
                collision.GetComponent<mushroomCtrl>().withSabre = true;
                collision.GetComponent<mushroomCtrl>().hurtMode = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Ladder"))
        {
            trigger = false;
        }
    }

}
