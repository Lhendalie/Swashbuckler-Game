using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBombCtrl : MonoBehaviour
{
    public GameObject explosion;
    private Vector2 movingDirection;
    // Start is called before the first frame update
    void Start()
    {
        //Destroy the object in 1 second
        StartCoroutine(StartExplosion(0.8f));
        Destroy(gameObject, 0.9f);

        movingDirection = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEvents>().bombDirection;
    }

    private void Update()
    {
        //Move the object towards desired direction
        if (movingDirection != null)
        {
            transform.Translate(movingDirection * 7f * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Mushroom"))
        {
            collision.GetComponent<mushroomCtrl>().hurtMode = true;
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    IEnumerator StartExplosion(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        Instantiate(explosion, transform.position, Quaternion.identity);
    }
}
