using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletControl : MonoBehaviour
{
    private Vector2 movingDirection;
    // Start is called before the first frame update
    void Start()
    {
        //Destroy the object in 1 second
        Destroy(gameObject, 0.9f);

        movingDirection = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEvents>().bulletDirection;
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
        if (collision.CompareTag("Mermaid"))
        {
            collision.GetComponent<MermaidMovement>().hurtingMode = true;
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Mushroom"))
        {
            collision.GetComponent<mushroomCtrl>().hurtMode = true;
            Destroy(gameObject);
        }        
    }
}
