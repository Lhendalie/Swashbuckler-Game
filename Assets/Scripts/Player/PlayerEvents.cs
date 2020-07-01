using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEvents : MonoBehaviour
{
    public AudioSource playerHurt;
    public bool isDead = false;
    public bool GameOver;
    public int playerScore = 0;
    public int playerHealth = 100;

    public Animator animator;
    public Vector2 bulletDirection;
    public Vector2 bombDirection;

    public GameObject bullet;
    public GameObject bomb;
    public GameObject bulletLocation;
    public GameObject bombLocation;

    private Slider healthBarSlider;
    private Text healthText;

    private int animationIndex;

    private bool isNextBulletReady = true;
    private bool isNextBombReady = true;
    public bool chestFound = true;
    private Animator chestAnim;

    private void Start()
    {
        healthBarSlider = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<Slider>();
        healthText = GameObject.FindGameObjectWithTag("HealthText").GetComponent<Text>();
        playerHurt = GetComponent<AudioSource>();
    }
    private void FixedUpdate()
    {
        if (!GameOver)
        {
            healthBarSlider.value = playerHealth;
            healthText.text = $"{playerHealth}";

            if (!isDead)
            {                
                if (animator.GetBool("IsShooting"))
                {
                    ShootBullet();
                }

                if (animator.GetBool("IsThrowing"))
                {
                    ThrowBomb();
                }

                animator.SetBool("IsHurt", false);

                if (chestFound)
                {
                    if (Input.GetKeyDown(KeyCode.O))
                    {
                        chestAnim.SetBool("ChestOpen", true);
                        GameOver = true;
                        GameObject.Find("GameEnd").GetComponent<AudioSource>().Play();
                        GameObject.Find("UnderWaterLoop").GetComponent<AudioSource>().Stop();
                        GameObject.Find("BGAudio").GetComponent<AudioSource>().Stop();
                    }
                }

            }
            else
            {
                Die();
            }
        }
        else
        {
            StartCoroutine(FinishGame(4));
        }
    }
    void ShootBullet()
    {
        GetBulletDirection();

        animationIndex = ((int)(animator.GetCurrentAnimatorStateInfo(0).normalizedTime * (10))) % 10;
        if (animationIndex == 0 && isNextBulletReady)
        {
            Instantiate(bullet, bulletLocation.transform.position, Quaternion.identity);
            isNextBulletReady = false;
            StartCoroutine(GetBulletReady(0.1f));
        }
    }

    void ThrowBomb()
    {
        GetBombDirection();

        animationIndex = ((int)(animator.GetCurrentAnimatorStateInfo(0).normalizedTime * (10))) % 10;
        if (animationIndex == 0 && isNextBombReady)
        {
            Instantiate(bomb, bombLocation.transform.position, Quaternion.identity);
            isNextBombReady = false;
            StartCoroutine(GetBombReady(0.1f));
        }

    }

    public void TakeDamage()
    {
        playerHurt.Play();
        GetComponent<PlayerMovement>().runSpeed = 0;
        animator.SetBool("IsHurt", true);
        playerHealth -= 10;

        StartCoroutine(SetSpeed(0.7f, 20));

        if (playerHealth <= 0)
        {
            isDead = true;
            playerHealth = 0;
        }
    }

    void Die()
    {
        GetComponent<PlayerMovement>().runSpeed = 0;
        animator.SetBool("IsDead", true);

        StartCoroutine(Blinker());
        StartCoroutine(WaitAfterDead(3));
    }

    void GetBulletDirection()
    {
        if(bulletLocation.transform.position.x < transform.position.x)
        {
            bulletDirection = Vector2.left;
        }
        else
        {
            bulletDirection = Vector2.right;
        }
    }

    void GetBombDirection()
    {
        if(bombLocation.transform.position.x < transform.position.x)
        {
            bombDirection = Vector2.left;
        }
        else
        {
            bombDirection = Vector2.right;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Chest"))
        {
            chestFound = true;
            chestAnim = collision.GetComponent<Animator>();
        }
    }

    IEnumerator GetBulletReady(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        isNextBulletReady = true;
    }

    IEnumerator GetBombReady(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        isNextBombReady = true;
    }

    IEnumerator WaitAfterDead(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        GameObject.Find("PanelCtrl").GetComponent<PanelControl>().OpenGOPanel();
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

    private IEnumerator FinishGame(float seconds)
    {
        GetComponent<PlayerMovement>().runSpeed = 0;
        
        yield return new WaitForSeconds(seconds);

        GameObject.Find("PanelCtrl").GetComponent<PanelControl>().OpenLCPanel();
    }

    private IEnumerator SetSpeed(float seconds, float speed)
    {
        yield return new WaitForSeconds(seconds);

        GetComponent<PlayerMovement>().runSpeed = speed;
    }
}
