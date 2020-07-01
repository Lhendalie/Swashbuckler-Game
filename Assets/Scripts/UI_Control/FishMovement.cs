using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour
{
    public float speed = 2;

    private Vector2 direction = Vector2.left;

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("DirectionLeft"))
        {
            direction = Vector2.left;
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (other.CompareTag("DirectionRight"))
        {
            direction = Vector2.right;
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }
}
