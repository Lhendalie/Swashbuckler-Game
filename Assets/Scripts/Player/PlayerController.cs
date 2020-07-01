using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float m_JumpForce = 400f;                          // Amount of force added when the player jumps.
    [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;          // Amount of maxSpeed applied to crouching movement. 1 = 100%
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
    [Range(0, .3f)] [SerializeField] private float m_ClimbingSmoothing = .05f;  // How much to smooth out the climbing
    [SerializeField] private bool m_AirControl = false;                         // Whether or not a player can steer while jumping;
    [SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
    [SerializeField] private LayerMask m_WhatIsWater;                          // A mask determining what is water to the character
    [SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
    [SerializeField] private Transform m_SwimCheck;                           // A position marking where to check if the player is swimming.
    [SerializeField] private Transform m_CeilingCheck;                          // A position marking where to check for ceilings
    [SerializeField] private Collider2D m_CrouchDisableCollider;                // A collider that will be disabled when crouching

    const float k_WaterdRadius = 0.0001f; // Radius of the overlap circle to determine if swimming
    const float k_GroundedRadius = 0.0001f; // Radius of the overlap circle to determine if grounded
    public bool m_Swimming;            // Whether or not the player is swimming.
    public bool m_Climbing;            // Whether or not the player is climbing.
    private bool m_Grounded;            // Whether or not the player is grounded.
    private bool m_Jumping;            // Whether or not the player is jumping.
    const float k_CeilingRadius = 0.0001f; // Radius of the overlap circle to determine if the player can stand up
    private Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true;  // For determining which way the player is currently facing.
    private Vector3 m_Velocity = Vector3.zero;

    public PlayerMovement movementScript;

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;
    public UnityEvent OnSwimEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    public BoolEvent OnCrouchEvent;
    private bool m_wasCrouching = false;

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();

        if (OnCrouchEvent == null)
            OnCrouchEvent = new BoolEvent();

        if (OnSwimEvent == null)
            OnSwimEvent = new UnityEvent();
    }

    private void FixedUpdate()
    {
        if (!m_Climbing)
        {
            bool wasGrounded = m_Grounded;
            m_Grounded = false;

            // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
            // This can be done using layers instead but Sample Assets will not overwrite your project settings.
            Collider2D[] collidersGround = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
            for (int i = 0; i < collidersGround.Length; i++)
            {
                if (collidersGround[i].gameObject != gameObject)
                {
                    m_Grounded = true;
                    if (!wasGrounded)
                    {
                        OnLandEvent.Invoke();
                    }
                }
            }

            bool wasSwimming = m_Swimming;
            m_Swimming = false;

            // The player is swimming if a circlecast to the groundcheck position hits anything designated as water
            // This can be done using layers instead but Sample Assets will not overwrite your project settings.
            Collider2D[] collidersWater = Physics2D.OverlapCircleAll(m_SwimCheck.position, k_WaterdRadius, m_WhatIsWater);
            for (int i = 0; i < collidersWater.Length; i++)
            {
                if (collidersWater[i].gameObject != gameObject)
                {
                    m_Swimming = true;
                    if (!wasSwimming)
                    {
                        OnSwimEvent.Invoke();
                    }
                }
            }
        }
    }

    public void Climb(float move)
    {
        m_Climbing = true;
        m_Swimming = false;

        // Base speed for character climbing
        float speed = 2f;

        // Move the character by finding the target velocity
        Vector3 targetVelocity = new Vector2(m_Rigidbody2D.velocity.x, move * speed);

        // And then smoothing it out and applying it to the character
        m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_ClimbingSmoothing);
    }

    public void Move(float move, bool crouch, bool jump)
    {
        m_Climbing = false;

        // If not crouching, check to see if the character can stand up
        if (!crouch)
        {
            // If the character has a ceiling preventing them from standing up, keep them crouching
            if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
            {
                crouch = true;
            }
        }

        //only control the player if grounded or airControl is turned on
        if (m_Grounded || m_Swimming || m_AirControl)
        {

            // If crouching
            if (crouch)
            {
                if (!m_wasCrouching)
                {
                    m_wasCrouching = true;
                    OnCrouchEvent.Invoke(true);
                }

                // Reduce the speed by the crouchSpeed multiplier
                move *= m_CrouchSpeed;

                // Disable one of the colliders when crouching
                if (m_CrouchDisableCollider != null)
                    m_CrouchDisableCollider.enabled = false;
            }
            else
            {
                // Enable the collider when not crouching
                if (m_CrouchDisableCollider != null)
                    m_CrouchDisableCollider.enabled = true;

                if (m_wasCrouching)
                {
                    m_wasCrouching = false;
                    OnCrouchEvent.Invoke(false);
                }
            }

            // Base speed for character moving
            float speed = 10f;

            // If swimming move slower
            if (m_Swimming)
            {
                speed = 7f;
            }

            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(move * speed, m_Rigidbody2D.velocity.y);
            // And then smoothing it out and applying it to the character
            m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

            // If the input is moving the player right and the player is facing left...
            if (move > 0 && !m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (move < 0 && m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
        }

        // If the player should jump...
        if (m_Grounded && jump)
        {
            // Add a vertical force to the player.
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
        }
    }


    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        transform.position = new Vector3(transform.position.x + (m_FacingRight ? 0.5f : -0.5f), transform.position.y, transform.position.z);
    }
}
