using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MermaidBulletControl : MonoBehaviour
{
    private Vector2 movingDirection;
    // Start is called before the first frame update
    void Start()
    {
        //Destroy the object in 3 seconds
        Destroy(gameObject, 3);
    }

    private void Update()
    {
        //Move the object towards desired direction
        if (movingDirection != null)
        {
            transform.Translate(movingDirection * 5f * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Get object desired direction
        if(collision.CompareTag("Mermaid"))
        {
            movingDirection = collision.GetComponent<MermaidMovement>().bulletDirection;
        }

        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerEvents>().TakeDamage();
            Destroy(gameObject);
        }
    }
}
